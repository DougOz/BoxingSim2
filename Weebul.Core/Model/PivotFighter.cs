using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class PivotFighter
    {
        public PivotFighter(FighterStats stats, FightPlan fightPlan, string name, TrainingStat trainingStat)
        {
            this.Stats = stats;
            this.FightPlan = fightPlan;
            this.Name = name;
            this.StatAdjustments = GetStatAdjustment(trainingStat);
        }


        public static FighterStats GetStatAdjustment(TrainingStat stat)

        {
            FighterStats ret = new FighterStats(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            switch(stat)
            {
                case TrainingStat.Agility:
                    ret.Agility = 1;
                    break;
                case TrainingStat.Chin:
                    ret.Chin = 1;
                    break;
                case TrainingStat.Conditioning:
                    ret.Conditioning = 1;
                    break;
                case TrainingStat.Speed:
                    ret.Speed = 1; 
                    break;
                case TrainingStat.Strength:
                    ret.Strength = 1; 
                    break;
                case TrainingStat.KOPunch:
                    ret.KnockoutPunch = 1;
                    break;
            }
            return ret; 
        }
        public FighterStats Stats { get; set; }

        public FightPlan FightPlan { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public int Number { get; set; }

        public FighterStats StatAdjustments { get; set; }

        public FighterStats GetStats()
        {
            FighterStats ret = Stats.Copy();
                
            if(StatAdjustments != null)
            {
                ret.Agility += StatAdjustments.Agility;
                ret.Chin += StatAdjustments.Chin;
                ret.Conditioning += StatAdjustments.Conditioning;
                ret.Height += StatAdjustments.Height;
                ret.Speed += StatAdjustments.Speed;
                ret.Strength += StatAdjustments.Strength;
                ret.KnockoutPunch += StatAdjustments.KnockoutPunch;                
            }
            return ret; 

        }
    }
}
