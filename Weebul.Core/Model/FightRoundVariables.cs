using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Scripting;

namespace Weebul.Core.Model
{
    public class FightRoundVariables
    {
        public int Round { get; set; }

        public double EndurancePercent { get; set; }
        public int MyStuns { get; set; }
        public int HisStuns { get; set; }
        public int MyKnockdowns { get; set; }
        public int HisKnockdowns { get; set; }
        public int MyCuts { get; set; }
        public int HisCuts { get; set; }
        public int RoundsWon { get; set;}
        public int RoundsLost { get; set; }
        public OpponentStrength Opponent { get; set; }
        public int Endurance { get; set; }
        public int Score { get; set; }
        public int Warnings { get; set; }

        public bool CanTowel { get; set; }
        public static FightRoundVariables GetVariables(int round, int score, FighterFight fighter, FighterFight other)
        {
            FightRoundVariables ret = new FightRoundVariables()
            {
                Round = round,
                Score = score,
                EndurancePercent = fighter.EndurancePercent,
                MyStuns = fighter.Stuns,
                MyKnockdowns = fighter.Knockdowns,
                MyCuts = fighter.Cuts.Sum(s => (int) s.Level),
                HisCuts = other.Cuts.Sum(s => (int) s.Level),
                HisStuns = other.Stuns,
                HisKnockdowns = other.Knockdowns,
                RoundsWon = fighter.RoundsWon,
                RoundsLost = other.RoundsWon,
                Endurance = Convert.ToInt32(fighter.EndurancePoints),
                Warnings = fighter.Warnings,
                CanTowel = fighter.CanTowel()
            };

            double otherEndurancePre = (round == 1 || other.EndurancePreRecover == 0) ? other.EndurancePoints : other.EndurancePreRecover;
            double otherEndurance = otherEndurancePre / (other.RoundStats.AdjustedStats.Conditioning * 10);
            if (otherEndurance < 1d / 3)
            {
                ret.Opponent = OpponentStrength.Exhausted;
            }
            else if (otherEndurance <= 2d / 3)
            {
                ret.Opponent = OpponentStrength.Tired;
            }
            else
            {
                ret.Opponent = OpponentStrength.Strong;
            };
            return ret;
        }

        public enum OpponentStrength
        {
            Strong,
            Tired,
            Exhausted
        }

        public ScriptVariables ToScriptVariables()
        {
            ScriptVariables ret = new ScriptVariables()
            {
                DecisionLost = (RoundsLost > 7),
                DecisionWon = (RoundsWon > 7),
                Endurance = Endurance,
                EndurancePercent = Convert.ToInt32(EndurancePercent * 100),
                HisCuts = HisCuts,
                HisKnockdowns = HisKnockdowns,
                HisStuns = HisStuns,
                MyCuts = MyCuts,
                MyKnockdowns = MyKnockdowns,
                MyStuns = MyStuns,
                Round = Round,
                RoundsLost = RoundsLost,
                RoundsWon = RoundsWon,
                Score = Score,
                Warnings = Warnings,
                CanTowel = CanTowel
            };
            if(Opponent == OpponentStrength.Strong)
            {
                ret.Opponent = ScriptVariables.Strong;
            }
            else if (Opponent == OpponentStrength.Tired)
            {
                ret.Opponent = ScriptVariables.Tired;
            }
            else
            {
                ret.Opponent = ScriptVariables.Weak;
            }
            return ret; 
        }
    }
}
