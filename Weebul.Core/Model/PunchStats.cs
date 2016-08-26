using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class PunchStats
    {



        public PunchStats(int jabsAttempted, int rightsAttempted, int powerPunchesAttempted, int jabsLanded, int rightsLanded, int powerPunchesLanded)
        {
            this.JabsAttempted = jabsAttempted;
            this.RightsAttempted = rightsAttempted;
            this.PowerPunchesAttempted = powerPunchesAttempted;
            this.JabsLanded = jabsLanded;
            this.RightsLanded = rightsLanded;
            this.PowerPunchesLanded = powerPunchesLanded;
        }
        public PunchStats(FighterTactics tactics)
        {
            int totalPunches  = Convert.ToInt32(Math.Round(tactics.Aggressiveness * 9, 0));
            this.PowerPunchesAttempted = Convert.ToInt32(Math.Round(tactics.Power * 3, 0));
            this.RightsAttempted = PowerPunchesAttempted;
            if (PowerPunchesAttempted > totalPunches)
            {
                PowerPunchesAttempted = totalPunches;
                RightsAttempted = 0;
            }
            else if (RightsAttempted+PowerPunchesAttempted > totalPunches)
            {


                RightsAttempted = totalPunches - PowerPunchesAttempted;
                
            }
            JabsAttempted = totalPunches - RightsAttempted - PowerPunchesAttempted;

        }
        public static PunchStats GetPunchStats(FighterRoundStats roundStats)
        {
            roundStats.AdjustPunchLuck();
            PunchStats ret = new PunchStats(roundStats.AdjustedTactics);
         
            ret.PowerPunchesLanded = GetLanded(ret.PowerPunchesAttempted, roundStats.PunchLandPercent);
            ret.RightsLanded = GetLanded(ret.RightsAttempted, roundStats.PunchLandPercent);
            ret.JabsLanded = GetLanded(ret.JabsAttempted, roundStats.JabLandPercent);
            return ret; 

        }

        public static int GetLanded(int attempted, double percent)
        {
            return Convert.ToInt32(Math.Round(attempted * percent, 0));
        }
        public int JabsAttempted { get; set; }

        public int RightsAttempted { get; set; }

        public int PowerPunchesAttempted { get; set; }
        public int TotalPunchesAttempted
        {
            get
            {
                return this.PowerPunchesAttempted + this.RightsAttempted + this.JabsAttempted;
            }

        }

        public int TotalPunchesLanded
        {
            get
            {
                return this.PowerPunchesLanded + this.JabsLanded + this.RightsLanded;
            }
        }
        public int JabsLanded { get; set; }
        public int RightsLanded { get; set; }

        public int PowerPunchesLanded { get; set; }

        public double GetScore(double stunDamage, double enduranceDamage)
        {
            double adjust = (stunDamage + enduranceDamage) / 100;
            double ret = Points;
            ret *= (1 + adjust);
            return ret;
        }

        public double Points
        {
            get
            {
                double ret = JabsLanded * 2 + RightsLanded * 3 + PowerPunchesLanded * 4;
                return ret; 
            }
        }
        public static double GetPercentageNoLuck(double speed, int rating, double oppAgility, bool isJab)
        {
            if (isJab)
            {
                speed *= 1.5;
            }
            if (oppAgility < 1) oppAgility = 1; 
            double k = 12 + (1d / 3) * rating;
            double ret = speed * speed / (speed * speed + k * Math.Sqrt(k * oppAgility));

            return ret;
        }
        public static double GetPercentageLuck(double percentage, double luck)
        {
            double ret = percentage;

            if (luck > 1)
            {
                ret += (1 - ret) * (luck - 1);
            }
            else
            {
                ret *= luck;
            }
            return ret;
        }

        public override string ToString()
        {

            return string.Format("{0} of {1} landed ({2} Power Punches, {3} Jabs, {4} Rights)",
                this.TotalPunchesLanded, this.TotalPunchesAttempted, this.PowerPunchesLanded, this.JabsLanded, this.RightsLanded);
        }

        public string ToShortString()
        {
            return String.Format("{0} of {1} - ({2} PP, {3} J, {4}, R)", 
                this.TotalPunchesLanded, 
                this.TotalPunchesAttempted, 
                this.PowerPunchesLanded, 
                this.JabsLanded, 
                this.RightsLanded);
        }
    }
}
