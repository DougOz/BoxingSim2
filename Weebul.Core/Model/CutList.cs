using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class CutList : List<Cut>
    {
        public CutList()
        {
        }

        public CutList(IEnumerable<Cut> collection) : base(collection)
        {
        }

        public void ResetRound()
        {
            this.ForEach((c) => c.RoundDamage = 0);
        }
        public double DamageForRound()
        {
            double ret = this.Sum(s => s.RoundDamage);
            //Cut swell = this.FirstOrDefault(c => c.IsSwelling && c.Level == CutSeverity.Critical);
            //if(swell != null && this.Count(c=>c.IsSwelling) == 1)
            //{
            //    ret -= swell.RoundDamage;
            //}
            return ret;
        }
        public int SumLevelForPlans()
        {
            int ret = this.Sum(s => (int) s.Level);
            Cut swell = this.FirstOrDefault(c => c.IsSwelling && c.Level == CutSeverity.Critical);
            if (swell != null && this.Count(c => c.IsSwelling) == 1)
            {
                ret -= (int) swell.Level;
            }
            return ret;
        }
        public void AddCut(Cut cut)
        {
            Cut c = this.FirstOrDefault(a => a.Type == cut.Type);
            if (c != null)
            {
                //if (c.IsSwelling && c.Level == CutSeverity.Critical)
                //{
                //    // can't aggravate level 4 swelling. 
                //}
                //else
                //{           
                    c.Aggravate();
            //    }
            }
            else
            {
                cut.SetNewCutRoundDamage();
                this.Add(cut);
            }
        }
        public CutList Copy()
        {
            CutList ret = new CutList();
            foreach (Cut cut in this)
            {
                ret.Add(cut.Copy());
            }

            return ret;
        }
        public bool CantSee()
        {
            return this.Any(c => c.Level == CutSeverity.Critical && c.Type == CutType.SwellLeft) && this.Any(c => c.Level == CutSeverity.Critical && c.Type == CutType.SwellRight);
        }


        //public static CutPenalty GetAdditionalSwellPenalty(IEnumerable<CutBase> cuts)
        //{
        //    CutBase cLeft = cuts.FirstOrDefault(c => c.IsSwelling && c.IsLeft && c.Level == CutSeverity.Critical);
        //    CutPenalty cp = new CutPenalty();
        //    if (cLeft != null)
        //    {
        //        foreach (var cb in cuts.Where(c => !c.IsSwelling && c.IsLeft))
        //        {
        //            cp.Add(cLeft.GetPenalty());
        //        }
        //    }
        //    CutBase cRight = cuts.FirstOrDefault(c => c.IsSwelling && c.IsRight && c.Level == CutSeverity.Critical);

        //    if (cRight != null)
        //    {
        //        foreach (var cb in cuts.Where(c => !c.IsSwelling && c.IsRight))
        //        {
        //            cp.Add(cRight.GetPenalty());
        //        }
        //    }
        //    return cp;
        //}
        public override string ToString()
        {
            return String.Join(", ", this);
        }
    }
}
