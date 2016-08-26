using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class CutGroupPenalty
    {
        public CutGroup CutGroup { get; set; }
        public CutPenalty this[CutSeverity level]
        {
            get
            {
                switch (level)
                {
                    case CutSeverity.Low:
                        return Low;
                    case CutSeverity.Medium:
                        return Medium;
                    case CutSeverity.High:
                        return High;
                    case CutSeverity.Critical:
                        return Critical;
                    default:
                        return Low;
                }
            }   
            set
            {
                switch (level)
                {
                    case CutSeverity.Low:
                        this.Low = value;
                        break;
                    case CutSeverity.Medium:
                        this.Medium = value;
                        break;
                    case CutSeverity.High:
                        this.High = value;
                        break;
                    case CutSeverity.Critical:
                        this.Critical = value;
                        break;
                }
            }
        }
        
        public CutPenalty Low { get; set; }
        public CutPenalty Medium { get; set; }

        public CutPenalty High { get; set; }
        public CutPenalty Critical { get; set; }
    }
}
