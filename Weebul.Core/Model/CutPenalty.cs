using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class CutPenalty
    {

        public CutPenalty()
        {

        }
        public CutPenalty(double agilityPenalty, double speedPenalty, double fatiguePenalty)
        {
            this.AgilityPenalty = agilityPenalty;
            this.SpeedPenalty = speedPenalty;
            this.FatiguePenalty = fatiguePenalty;
        }

        public bool HasNone
        { 
            get
            {
                return AgilityPenalty == 0 && SpeedPenalty == 0;
            }
        }
        
        public double AgilityPenalty { get; set; }

        public double SpeedPenalty { get; set; }

        public double FatiguePenalty { get; set; }

        public CutPenalty Copy()
        {
            return new Model.CutPenalty(this.AgilityPenalty, this.SpeedPenalty, this.FatiguePenalty);
        }
        public void Add(CutPenalty other)
        {
            this.AgilityPenalty += other.AgilityPenalty;
            this.SpeedPenalty += other.SpeedPenalty;
            this.FatiguePenalty += other.FatiguePenalty; 
        }
        
    }
}
