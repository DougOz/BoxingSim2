using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class RoundDamage
    {
        public const double DAMAGE_FOR_STUN = 1.5;
        public const double STUN_MULTIPLIER = 0.75;
        public double StunDamage { get; set; }
        public double EnduranceDamage { get; set; }
        public double BaseDamage { get; set; }
        public double StunValue { get; set; }
        public double CutDamage { get; set; }


        public static void AdjustForStun(RoundDamage first, RoundDamage second)
        {
            if(first.StunValue > DAMAGE_FOR_STUN && first.StunValue > second.StunValue)
            {
                second.MultiplyDamage(STUN_MULTIPLIER);
            }
            else if (second.StunValue > DAMAGE_FOR_STUN)
            {
                first.MultiplyDamage(STUN_MULTIPLIER);
            }
        }
        public static RoundDamage CalculateRoundDamage(FighterStats stats, FighterTactics tactics, FighterStats oppStats, FighterTactics oppTactics, TargetArea targetArea, double multiplier = 1)
        {

            RoundDamage ret = new RoundDamage();
            ret.BaseDamage = GetBaseDamage(stats, tactics, oppStats, oppTactics);
            ret.BaseDamage *= multiplier;
            ret.EnduranceDamage = ret.BaseDamage;
            if (targetArea == TargetArea.Body)
            {
                ret.EnduranceDamage *= 1.2;
            }
            else if (targetArea == TargetArea.Head)
            {
                ret.EnduranceDamage *= 0.8;
            }
            else if (targetArea == TargetArea.Cut)
            {
                ret.EnduranceDamage *= 0.9;
            }
            ret.StunDamage = GetStunDamage(stats, tactics, oppStats, oppTactics, targetArea);
            ret.StunDamage *= multiplier;
            ret.StunValue = ret.StunDamage / oppStats.Chin;
            return ret;
        }

        public static double GetBaseDamage(FighterStats stats, FighterTactics tactics, FighterStats oppStats, FighterTactics oppTactics)
        {
            double ret = tactics.Power* stats.Strength*
            Math.Sqrt(stats.Speed * tactics.Aggressiveness) /
            (oppTactics.Defense * oppStats.Agility);
            return ret; 
        }
        public static double  GetStunDamage(FighterStats stats, FighterTactics tactics, FighterStats oppStats, FighterTactics oppTactics, TargetArea targetArea)
        {
            double ret = (stats.Strength + (stats.KnockoutPunch * 3)) *
          tactics.Power * Math.Sqrt(stats.Speed * tactics.Aggressiveness) /
          (oppTactics.Defense * oppStats.StunDefense);
            if (targetArea == TargetArea.Body)
            {
                ret *= 0.8;
            }
            else if (targetArea == TargetArea.Head)
            {
                ret *= 1.2;
            }
            else if (targetArea == TargetArea.Cut)
            {
                ret *= 0.9;
            }
            return ret; 
        }

        public static RoundDamage Diff(RoundDamage first, RoundDamage second)
        {
            RoundDamage ret = new Model.RoundDamage()
            {
                BaseDamage = first.BaseDamage - second.BaseDamage,
                CutDamage = first.CutDamage - second.CutDamage,
                EnduranceDamage = first.EnduranceDamage - second.EnduranceDamage,
                StunDamage = first.StunDamage - second.StunDamage,
                StunValue = first.StunValue - second.StunValue

            };
            return ret; 
        }

        public static RoundDamage Sum(IEnumerable<RoundDamage> items)
        {
            RoundDamage ret = new RoundDamage();

            foreach(RoundDamage item in items)
            {
                ret.AddDamage(item);
            }
            return ret; 
        }

        public void AddDamage(RoundDamage other)
        {
            this.StunDamage += other.StunDamage;
            this.EnduranceDamage += other.EnduranceDamage;
            this.BaseDamage += other.BaseDamage;
            this.StunValue += other.StunValue;
            this.CutDamage += other.CutDamage;
        }

        public void MultiplyDamage(double multiplier)
        {
            this.StunDamage *= multiplier;
            this.EnduranceDamage *= multiplier;
            this.BaseDamage *= multiplier;
            this.StunValue *= multiplier;

        }
    }
}
