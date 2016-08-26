using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Data;
using System.Data.Entity;
using Weebul.Core.Helpers;

namespace Weebul.Core.Model
{
    public class CutBase : IEquatable<CutBase>
    {
        public CutBase(CutType type, CutSeverity level)
        {
            this.Type = type;
            this.Level = level;
        }
        public CutBase(Data.Cut dataCut)
        {
            this.Level = (CutSeverity) dataCut.Severity;
            this.Type = (CutType) dataCut.CutType;
        }
        public CutType Type { get; set; }


        public CutSeverity Level { get; set; }

        public CutGroup CutGroup
        {
            get
            {
                return Resources.CutTypeGroupDictionary[this.Type];
            }
        }
        public bool IsBleeding
        {
            get
            {
                return this.Type == CutType.BleedAboveLeft ||
                    this.Type == CutType.BleedAboveRight ||
                    this.Type == CutType.BleedBelowLeft ||
                    this.Type == CutType.BleedBelowRight;
            }
        }
        public bool IsSwelling
        {
            get
            {
                return this.Type == CutType.SwellLeft ||
                    this.Type == CutType.SwellRight;
            }
        }

        public bool IsRight { get
            {
                return this.Type == CutType.BleedAboveRight || this.Type == CutType.BleedBelowRight || this.Type == CutType.SwellRight;
            }
            }

        public bool IsLeft
        {
            get
            {
                return this.Type == CutType.BleedAboveLeft || this.Type == CutType.BleedBelowLeft || this.Type == CutType.SwellLeft;
            }
        }

        public bool IsSwollenShut
        {
            get
            {
                return this.IsSwelling && this.Level == CutSeverity.Critical;
            }
        }
        public bool Equals(CutBase other)
        {
            return other != null && other.Type == this.Type && other.Level == this.Level;
        }
        public int AggravatedTimes { get; set; }
        public override bool Equals(object obj)
        {
            return Equals(obj as CutBase);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 37;
                int mult = 23;
                hash = hash * mult + Type.GetHashCode();
                hash = hash * mult + Level.GetHashCode();
                return hash; 
            }
        }

        private static Dictionary<CutBase, int> _idDictionary = null;

        public static Dictionary<CutBase, int> IdDictionary
        {
            get
            {
                if(_idDictionary == null)
                {
                    _idDictionary = LoadIDDictionary();
                }
                return _idDictionary;
            }
        }

        public static Dictionary<CutBase, int> LoadIDDictionary()
        {
            Dictionary<CutBase, int> ret = new Dictionary<Model.CutBase, int>();

            WeebulEntities entities = DataHelpers.Entities;

            entities.Cuts.Load();
            foreach(Data.Cut cut in entities.Cuts)
            {
                CutBase cb = new Model.CutBase((CutType) cut.CutType, (CutSeverity) cut.Severity);
                ret.Add(cb, cut.CutId);
            }
            return ret; 
        }

        public CutPenalty GetPenalty()
        {
            CutPenalty cp = Resources.CutGroupPenaltyDictionary[this.CutGroup][this.Level];
            if (IsSwollenShut && AggravatedTimes >= 1)
            {
                cp = cp.Copy();
                CutPenalty cp3 = Resources.CutGroupPenaltyDictionary[this.CutGroup][CutSeverity.High];
                for(int i = 1; i<= AggravatedTimes; i++)
                {
                    cp.Add(cp3);
                }
            }
            return cp; 
            //return Resources.CutGroupPenaltyDictionary[this.CutGroup][this.Level];
        }

        public static CutPenalty CutGroupPenalty(CutGroup cutGroup, CutSeverity level)
        {
            CutPenalty cp = new CutPenalty();
            if (cutGroup == CutGroup.BleedAbove)
            {
                cp.SpeedPenalty += 0.5 * (int) level; 
                if (level == CutSeverity.High)
                {
                    cp.AgilityPenalty += 0.5;
                }
                else if (level == CutSeverity.Critical)
                {
                    cp.AgilityPenalty += 1;
                }
            }
            //else if (cutGroup == CutGroup.BleedBelow)
            //{
            //    double penalty = ((int) level - 1) * 0.5;
            //    cp.SpeedPenalty += penalty;
            //    cp.AgilityPenalty += penalty;
            //}
            else if (cutGroup == CutGroup.Swell && (int) level >= 2)
            {

                double penalty = (level == CutSeverity.Medium) ? 0.5 : level == CutSeverity.High ? 1.5 : 3.0; 
                cp.AgilityPenalty += penalty;
                cp.SpeedPenalty += penalty;
            }
            else if (cutGroup == CutGroup.Nose)
            {
                cp.FatiguePenalty = 1; 
                
            }
            return cp; 
        }

        
        public static CutPenalty CalculatePenaltyForCuts(IEnumerable<CutBase> cuts)
        {
            CutPenalty ret = new Model.CutPenalty();
            var cpList = cuts.Select(c => c.GetPenalty()).ToList();
            ret.AgilityPenalty = cpList.Sum(c => c.AgilityPenalty);
            ret.SpeedPenalty = cpList.Sum(c => c.SpeedPenalty);
            return ret; 
        }

        public static Dictionary<CutGroup, CutGroupPenalty> GetPenaltyDictionary()
        {
            var ret = new Dictionary<CutGroup, CutGroupPenalty>();
            foreach(CutGroup value in Enum.GetValues(typeof(CutGroup)))
           {
                CutGroupPenalty pen = new CutGroupPenalty()
                {
                    CutGroup = value,
                    Low = CutGroupPenalty(value, CutSeverity.Low),
                    Medium = CutGroupPenalty(value, CutSeverity.Medium),
                    High = CutGroupPenalty(value, CutSeverity.High),
                    Critical = CutGroupPenalty(value, CutSeverity.Critical)

                };
                ret.Add(value, pen);
            }
            return ret; 
        }
        public override string ToString()
        {
            return String.Format("{0} - {1}", this.Type.ToString(), this.Level.ToString());
        }
    }
}
