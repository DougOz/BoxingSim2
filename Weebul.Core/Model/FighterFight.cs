using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class FighterFight
    {
        public FighterFight(Fighter fighter, FightPlan fp)
        {         
            this.Fighter = fighter;
            this.FightPlan = fp; 
            Reset();       
        }
        public FighterFight(FighterStats stats, FightPlan fp)
        {
            this.Fighter = new Fighter() { Stats = stats };
            this.FightPlan = fp;
            Reset(); 
        }
        public string PivotName { get; set; }
        public void Reset()
        {
            this.Cuts = new CutList();         
            this.RoundStats = new FighterRoundStats(this.Fighter.Stats.Copy(), new FighterRoundPlan(4, 8, 8));
            this.Warnings = 0;
            this.RoundsWon = 0;
            this.Stuns = 0;
            this.Knockdowns = 0;
            this.FatigueLossPerRound = 0;
            this.TotalBaseDamage = 0;           
            this.EndurancePoints = this.Fighter.Stats.Conditioning * 10;
            this.EndurancePreRecover = this.EndurancePoints;
            this.FatigueLossStartRound = 0;
            this.PlanLineHits = new List<int>();

        }
        public Fighter Fighter { get; set; }
        public int RoundsWon { get; set; }
       
        public double EndurancePoints { get; set; }
        public double EndurancePreRecover { get; set; }
        public double FatigueLossStartRound { get; set; }
        public double StartFightEndurancePoints { get; set; }
        public int Warnings { get; set; }

        public int Stuns { get; set; }

        public int Knockdowns { get; set; }

        public double FatigueLossPerRound { get; set; }

        public bool CanTowel()
        {
            return TotalBaseDamage - this.Fighter.Stats.Chin * 5 >= 25;
        }
        public CutList Cuts { get; set; }

        public double TotalBaseDamage { get; set; }
        public FighterRoundStats RoundStats { get; set; }
        public double EndurancePercent { get
            {
                return this.EndurancePoints / (this.Fighter.Stats.Conditioning * 10);
            }
        }

        public FightPlan FightPlan { get; set; }
        public List<int> PlanLineHits { get; set; }
        public void SetStartRoundStatsAndGetPlan(FightRoundVariables variables)
        {
            this.RoundStats.AdjustedStats = this.RoundStats.OriginalStats.Copy();
            this.RoundStats.LuckFactor = 1;
            this.RoundStats.DamageAdjustment = 1;
            this.RoundStats.AdditionalFatigue = FatigueLossPerRound;
            this.RoundStats.AdditionalEnduranceDamage = 0; 
           // this.RoundStats.StunDefense = this.RoundStats.OriginalStats.Chin + this.RoundStats.OriginalStats.Agility - 10;
            FighterRoundPlan plan = this.FightPlan.GetPlan(variables);
            this.PlanLineHits.Add(plan.HitLineNumber);
            this.RoundStats.Plan = plan;            
            this.RoundStats.AdjustedTactics = plan.Copy();
        }
      
        public void AdjustStartEndurance(WeightClass weightClass)
        {
            double mult = WeightLossEnduranceMultiplier(weightClass, this.Fighter.Stats.MinWeight);
            this.EndurancePoints *= mult;
            this.StartFightEndurancePoints = this.EndurancePoints;
            this.EndurancePreRecover = this.EndurancePoints;

            if(this.Fighter.Stats.MinWeight > 200)
            {
                this.FatigueLossPerRound = (this.Fighter.Stats.MinWeight - 200) / 10; 
            }
        }
        public void MakeAdjustments(FighterFight other, FightOptions options)
        {
            FightingStyle style = this.RoundStats.Plan.Style;
            this.RoundStats.AdjustStyle(style, other.RoundStats);
            this.RoundStats.AdjustHeight(other.RoundStats);
            AdjustFatigueEndurancePreRound();
            this.RoundStats.AdjustCuts(this.Cuts);
            this.RoundStats.AdjustEndurance(this.EndurancePoints);
      
            if (options.LuckAmount > 0)
            {
                this.RoundStats.SetLuckFactor(options.LuckAmount);
            }
            else
            {
                this.RoundStats.LuckFactor = 1; 
            }

            this.RoundStats.FixAdjustments();
            this.RoundStats.SetCutPercent(other.RoundStats);         
        }

        /// <summary>
        /// Fatigue if super aggressive I guess 
        /// </summary>
        public void AdjustFatigueEndurancePreRound()
        {
            double fatigue = this.RoundStats.AdjustedTactics.Aggressiveness + this.RoundStats.AdjustedTactics.Power * 0.5; 

            if(fatigue > this.RoundStats.AdjustedStats.Conditioning)
            {
                this.FatigueLossStartRound = fatigue - this.RoundStats.AdjustedStats.Conditioning;

                this.EndurancePoints -= this.FatigueLossStartRound;
            }
        }
        public FighterRound BeginFighting(FighterFight other, Round round)
        {
            FighterRound ret = new FighterRound(round);
            
            ret.Tactics = FighterRoundPlan.Adjusted(this.RoundStats.Plan, this.RoundStats.AdjustedTactics);
            ret.StartEndurance = this.EndurancePoints;
            ret.StartEndurancePercent = this.EndurancePercent;        
            ret.CheckFighterWarning(this);
            this.RoundStats.AdjustDirty(ret.IsWarned);
            if(ret.IsWarned)
            {
                this.Warnings++;
                if (this.Warnings > 1)
                    ret.DeductPointWarning = true; 
            }
            ret.DamageDealt = RoundDamage.CalculateRoundDamage(this.RoundStats.AdjustedStats, this.RoundStats.AdjustedTactics,
                other.RoundStats.AdjustedStats, other.RoundStats.AdjustedTactics, this.RoundStats.Plan.TargetArea, this.RoundStats.DamageAdjustment);
            this.RoundStats.SetPunchAccuracy(other.RoundStats);  
            return ret;
        }

        public void RecoverEndurance()
        {
            this.EndurancePreRecover = this.EndurancePoints;
            double recovery = 0.1 + 0.02 * this.RoundStats.AdjustedTactics.Rest;
            double startEndurance = (this.StartFightEndurancePoints == 0) ? this.RoundStats.OriginalStats.Conditioning * 10 : this.StartFightEndurancePoints;
            double recovered = (startEndurance - EndurancePoints) * recovery;
            this.EndurancePoints += recovered;
            
        }
      
        public void FatigueRound(double enduranceDamage)
        {
            this.EndurancePoints -= enduranceDamage;
            double fatigue = GetEndOfRoundFatigue();
            this.EndurancePoints -= this.Cuts.DamageForRound();
            this.EndurancePoints -= fatigue; 
            this.EndurancePoints -= this.RoundStats.AdditionalFatigue;       
        }

        public double GetEndOfRoundFatigue()
        {
            double fatigue = this.RoundStats.AdjustedTactics.Aggressiveness + this.RoundStats.AdjustedTactics.Power * 0.5;
            if (fatigue > this.RoundStats.AdjustedStats.Conditioning)
            {
                fatigue = this.RoundStats.AdjustedStats.Conditioning;
            }
            if (fatigue > (this.RoundStats.AdjustedStats.Conditioning / 2))
            {
                fatigue -= this.RoundStats.AdjustedStats.Conditioning / 2;                
            }
            else
            {
                fatigue = 0;
            }
            fatigue += this.RoundStats.AdditionalFatigue;
            return fatigue; 
        }
        public void SetCutDamageAndFatigue(FighterRound round)
        {
            round.DamageReceived.CutDamage = this.Cuts.DamageForRound();
        }
        
        public static double WeightLossEnduranceMultiplier(WeightClass weightClass, double minWeight)
        {
            if (weightClass == WeightClass.Heavy) return 1;
         
            if (minWeight <= (int) weightClass)
                return 1;

            double mult = (int) weightClass / minWeight;
            return Math.Pow(mult, 2); 
        }
        public static double MinWeight(double weight, double cond)
        {
            return weight - 0.25 * (cond + 2);
        }
    }
}
