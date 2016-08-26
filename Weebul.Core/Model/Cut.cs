using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Helpers;

namespace Weebul.Core.Model
{
    public class Cut : CutBase 
    {
        public Cut(CutType type, CutSeverity level) : base(type, level)
        { }

        public Cut(Data.Cut dataCut) : base(dataCut)
        {
            

        }
        public int TotalDamage { get; set; }

        public int RoundDamage { get; set; }

        public bool CausedTko { get; set; }

        public int TotalAgg { get; set; }
        private bool CheckTotalDamage()
        {

            if ((TotalAgg >= 7 && (int) this.Level >= 3) || (TotalAgg >= 6 && (int) this.Level == 4))
            {
                this.CausedTko = true;
                return true; 
            }
            return false; 
        }
        public void Aggravate()
        {
            
            TotalAgg++;
            if(IsBleeding || (this.Type == CutType.InjuredNose && (int)this.Level >= 2))
            {
                RoundDamage += (int) this.Level;
                TotalDamage += (int) this.Level;
                if (CheckTotalDamage()) return; 
            }

       //     if (this.Level == CutSeverity.Critical) return; 
            bool promote = IsSwelling || RandomGen.CheckRandom(0.5);
            if (promote)
            {
                if (IsSwelling)
                {
                
                    if (this.Level != CutSeverity.Critical)
                    {
                        this.Level++;
                    }
                    else
                    {
                        this.AggravatedTimes++; 
                    }
                }
                else
                {
                    this.Level = PromoteNonSwell(this.Level);
                }
                
                if(IsBleeding || (this.Type == CutType.InjuredNose))
                {
                    RoundDamage += (int) this.Level;
                    TotalDamage += (int) this.Level;
                    CheckTotalDamage(); 
                }
                if(this.Type == CutType.InjuredJaw && this.Level == CutSeverity.Critical)
                {
                    this.RoundDamage += 10;
                    this.TotalDamage += 10; 
                }
            }
        }
        public static CutSeverity PromoteNonSwell(CutSeverity start)
        {
            
            if(start == CutSeverity.High || start == CutSeverity.Critical)
            {
                return CutSeverity.Critical;
            }
            if(start == CutSeverity.Medium)
            {
                bool isCritical = RandomGen.CheckRandom(1d / 3);
                return (isCritical) ? CutSeverity.Critical : CutSeverity.High;
            }
 
                if(RandomGen.CheckRandom(2d/3))
                {
                    return CutSeverity.Medium;
                }
                else if (RandomGen.CheckRandom(2d/3))
                {
                    return CutSeverity.High; 
                }
                return CutSeverity.Critical; 
        }

        public void SetNewCutRoundDamage()
        {
            if (IsBleeding || (this.Type == CutType.InjuredNose && (int) this.Level >= 2))
            {
                RoundDamage += (int) this.Level;
                TotalDamage += (int) this.Level;
                TotalAgg = 1; 
            }
            else if (this.Type == CutType.InjuredJaw && this.Level == CutSeverity.Critical)
            {
                this.RoundDamage += 10;
                this.TotalDamage += 10; 
            }
        }

        public static Cut RandomCut()
        {
            double r = RandomGen.RANDOM_GEN.NextDouble();

            CutType cType = CutType.SwellRight;
            if (r < 0.1)
            {
                cType = CutType.BleedAboveLeft;
            }
            else if (r < 0.2)
            {
                cType = CutType.BleedAboveRight;
            }
            else if (r < 0.3)
            {
                cType = CutType.BleedBelowLeft;
            }
            else if (r < 0.4)
            {
                cType = CutType.BleedBelowRight;
            }
            else if (r < 0.5)
            {
                cType = CutType.InjuredJaw;
            }
            else if (r < 0.6)
            {
                cType = CutType.InjuredNose;
            }
            else if (r < 0.8)
            {
                cType = CutType.SwellLeft;
            }
            CutSeverity severity = CutSeverity.Low;
            if (cType != CutType.SwellLeft && cType != CutType.SwellRight)
            {
                if (RandomGen.CheckRandom(2d / 3))
                {
                    severity = CutSeverity.Low;
                }
                else if (RandomGen.CheckRandom(2d / 3))
                {
                    severity = CutSeverity.Medium;
                }
                else if (RandomGen.CheckRandom(2d / 3))
                {
                    severity = CutSeverity.High;
                }
                else
                {
                    severity = CutSeverity.Critical;
                }
            }
            return new Cut(cType, severity);
        }

        public Cut Copy()
        {
            return new Model.Cut(this.Type, this.Level)
            {
                TotalDamage = this.TotalDamage,
                RoundDamage = this.RoundDamage,
                CausedTko = this.CausedTko,
                TotalAgg = this.TotalAgg,
                AggravatedTimes = this.AggravatedTimes 
            };
        }

        public override string ToString()
        {
            string ret = null;
            string rightLeft = (Type == CutType.BleedAboveLeft || Type == CutType.BleedBelowLeft || Type == CutType.SwellLeft) ? "left" : "right";
            if(IsBleeding)
            {
                string ab = (Type == CutType.BleedAboveLeft || Type == CutType.BleedAboveRight) ? "above" : "below";
                string cut = "bleeding";

                if(Level == CutSeverity.Medium )
                {
                    cut = "cut";
                }
                else if (Level == CutSeverity.High)
                {
                    cut = "serious cut";
                }
                else if (Level == CutSeverity.Critical)
                {
                    cut = "gash";
                }
                ret = String.Format("{0} {1} the {2} eye", cut, ab, rightLeft);
             
            }
            else if (IsSwelling)
            {
                string swell = "swollen";

                if (Level == CutSeverity.Medium)
                {
                    swell = "badly swollen";
                }
                else if (Level == CutSeverity.High)
                {
                    swell = "nearly swollen shut";
                }
                else if (Level == CutSeverity.Critical)
                {
                    swell = "swollen shut";
                }
                ret = String.Format("{0} eye {1}", rightLeft, swell);
            }
            else if (Type == CutType.InjuredNose )
            {
                if (Level == CutSeverity.Low)
                {
                    ret = "bloody nose";
                }
                else if (Level == CutSeverity.Medium)
                {
                    ret = "fractured nose";
                }
                else if (Level == CutSeverity.High)
                {
                    ret = "badly fractured nose";
                }
                else if (Level == CutSeverity.Critical)
                {
                    ret = "broken nose";
                }
            }
           else
            {
                if (Level == CutSeverity.Low)
                {
                    ret = "bloody lip";
                }
                else if (Level == CutSeverity.Medium)
                {
                    ret = "bloody mouth";
                }
                else if (Level == CutSeverity.High)
                {
                    ret = "broken tooth";
                }
                else if (Level == CutSeverity.Critical)
                {
                    ret = "broken jaw";
                }
            }
            return ret;
        }

        public static Cut Parse(string text)
        {
            string t = text.Trim().TrimEnd('.');
            Cut ret = RegexConstants.CutParseDictionary[t];
            return ret.Copy();
        }

        public int DataId
        {
            get
            {
                return CutBase.IdDictionary[this];
            }
        }

    }
  
}
