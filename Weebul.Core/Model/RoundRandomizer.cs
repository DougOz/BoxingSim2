using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Helpers;

namespace Weebul.Core.Model
{
    public class RoundRandomizer
    {
        public int OrigPunchesAttempted { get; set; }
        public int OrigPunchesLanded { get; set; }
        public double OrigDamage { get; set; }
        
        public int NewPunchesAttempted { get; set; }
        public int NewPunchesLanded { get; set; }
        public double NewDamage { get; set; }
        public void RandomizePerPunch()
        {
            Random r = new Random();
            double percentLanded = (double) OrigPunchesLanded / OrigPunchesAttempted;
            double damagePerPunch = OrigDamage / OrigPunchesAttempted; 
            int landed = 0;
            double damage = 0; 
            for(int i = 1; i<= OrigPunchesAttempted; i++) 
                //attempts could be randomized too similar to how landed is, we just need an opportunities variable                                                 
            {
                if (r.NextDouble() < percentLanded)
                {
                    landed++;
                    damage += damagePerPunch; //could randomize damage too
                }
            }
            this.NewDamage = damage;
            this.NewPunchesAttempted = OrigPunchesAttempted;
            this.NewPunchesLanded = landed; 
        }

        public void RandomizeWeBL()
        {
            double luck = RandomGen.GetRandomNormal(mean: 1, stdDev: 0.05);
            NewDamage = OrigDamage * luck;
            this.NewPunchesAttempted = OrigPunchesAttempted;
            this.NewPunchesLanded = (int) (OrigPunchesLanded * luck);
        }
    }

}
