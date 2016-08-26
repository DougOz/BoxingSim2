using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Helpers;
using static Weebul.Core.Model.FighterRound;

namespace Weebul.Core.Model
{
    public class FightTracker : ModelBase 
    {

        public FightTracker()
        {
            this.Fight = new Model.Fight();
            this.Options = new FightOptions();
        }
        public FightTracker(Fighter fighter1, Fighter fighter2, FightPlan fighter1Plan, FightPlan fighter2Plan, FightOptions options) : this() 
        {
            this.Fighter1 = new FighterFight(fighter1, fighter1Plan);


            this.Fighter2 = new FighterFight(fighter2, fighter2Plan);
            
            this.Options = options; 
            
            
        }
        public FightTracker(FighterFight fighter1, FighterFight fighter2, FightOptions options)
        {
            this.Fighter1 = fighter1;
            this.Fighter2 = fighter2;
            this.Options = options; 
        }

        public FightOptions Options { get; set; }
        public FighterFight Fighter1 { get; set; }
        public FighterFight Fighter2 { get; set; }
        public int Round { get; set; }

        public Fight Fight { get; set; }
        //public List<RoundResult> AllRounds { get; set; }
        //public int ScoreFighter1 { get; set; }
        //public int ScoreFighter2 { get; set; }
        //public FightOutcome Result { get; set; }
        //public FightResultType ResultType { get; set; }
        public bool IsOver { get; set; }

        public void AdjustStrength()
        {
            double f1weight = this.Fighter1.Fighter.Stats.GetWeight(this.Options.WeightClass);
            double f2weight = this.Fighter2.Fighter.Stats.GetWeight(this.Options.WeightClass);
            if (f1weight > f2weight)
            {
                double rat = (f1weight / f2weight) - 1;
                double strplus = rat / 0.05;
                this.Fighter1.RoundStats.OriginalStats.Strength += strplus;
            }
            else if (f2weight > f1weight)
            {
                double rat = (f2weight / f1weight) - 1;
                double strplus = rat / 0.05;
                this.Fighter2.RoundStats.OriginalStats.Strength += strplus;
            }
        }     

        private void InitializeStuff()
        {
            this.Fighter1.Reset();
            this.Fighter2.Reset();
            AdjustStrength();
            this.Fight = new Model.Fight()
            {
                Fighter1 = this.Fighter1.Fighter.Stats,
                Fighter2 = this.Fighter2.Fighter.Stats
            };
            this.Round = 0;
            this.IsOver = false; 
            this.Fighter1.AdjustStartEndurance(this.Options.WeightClass);
            this.Fighter2.AdjustStartEndurance(this.Options.WeightClass);

            //To do move this somewhere 
            Resources.CutPercentMultiplier = this.Options.CutFactor;

        }

        public List<Fight> PlayFights(int numberOfTimes)
        {
            List<Fight> ret = new List<Fight>();
            for(int i = 0; i< numberOfTimes; i++)
            {
                Fight res = PlayFight();
                if(i %100 == 0)
                {
                   // Debug.Print("processing " + i);
                }
                ret.Add(res);
            }
            return ret; 
        }

        public static List<Fight> PlayMultiple(int numberOfTimes, Fighter f1, Fighter f2, FightPlan fp1, FightPlan fp2, FightOptions options)
        {
            int perEach = numberOfTimes / 20;

            var results = new ConcurrentBag<List<Fight>>();

            Parallel.For(0, 20, (i) =>
              {
                  
                  FightTracker tracker = new FightTracker(f1, f2, fp1.Copy(), fp2.Copy(), options);
                  List<Fight> resTemp = tracker.PlayFights(perEach);
                  results.Add(resTemp);
              });
            List<Fight> ret = results.SelectMany(fr => fr).ToList();
            if(ret.Count < numberOfTimes)
            {
                int numSim = ret.Count - numberOfTimes;
                FightTracker tracker = new FightTracker(f1, f2, fp1.Copy(), fp2.Copy(), options);
                var v = tracker.PlayFights(numSim);
                foreach(var res in v)
                {
                    ret.Add(res);
                }
            }
            return ret;
        }
        public static FightResultSet PlayMultiple_ResultSet(int numberOfTimes, FighterFight f1, FighterFight f2, FightOptions options)
        {
            List<Fight> fights = PlayMultiple(numberOfTimes, f1.Fighter, f2.Fighter, f1.FightPlan, f2.FightPlan, options);
            return new Model.FightResultSet(fights.Select(f => f.Result));

        }
        public static List<Fight> PlayMultiple(int numberOfTimes, FightTracker tracker)
        {
            return PlayMultiple(numberOfTimes, tracker.Fighter1.Fighter, tracker.Fighter2.Fighter, tracker.Fighter1.FightPlan, tracker.Fighter2.FightPlan, tracker.Options);
        }
        public Fight PlayFight()
        {
            InitializeStuff();             
            this.Fight.Result.ResultType = FightResultType.Decision;
            this.Fight.RoundResults = new List<Round>();
            while (true)
            {
                this.Round++;
                Round roundResult = FightRound();
           //     Debug.Print(roundResult.PrintString());
                this.Fight.RoundResults.Add(roundResult);
                if (roundResult.IsEndOfBout || this.Round == this.Options.TotalRounds)
                    break;
            }
            this.Fight.SetJudgeScores(); 
            if (!this.IsOver)
            {
                this.Fight.SetFightOutcomeDecision();
                this.IsOver = true; 
            }
            this.Fight.LineHits_Fighter1 = this.Fighter1.PlanLineHits;
            this.Fight.LineHits_Fighter2 = this.Fighter2.PlanLineHits; 
            return this.Fight;
        }

        public Round FightRound()
        {

            FightRoundVariables var1 = FightRoundVariables.GetVariables(this.Round, this.Fight.Fighter1Score - this.Fight.Fighter2Score, this.Fighter1, this.Fighter2);
            this.Fighter1.SetStartRoundStatsAndGetPlan(var1);
            FightRoundVariables var2 = FightRoundVariables.GetVariables(this.Round, this.Fight.Fighter2Score - this.Fight.Fighter1Score, this.Fighter2, this.Fighter1);
            this.Fighter2.SetStartRoundStatsAndGetPlan(var2);
            Round ret = new Round() { RoundNumber = this.Round, Fight = this.Fight  };

            if (this.Fighter1.RoundStats.Plan.IsTowel || this.Fighter2.RoundStats.Plan.IsTowel)
            {

                ret.SetTowel(!this.Fighter1.RoundStats.Plan.IsTowel);
            }
            else
            {
                SimRound(ret);
                ret.SetRoundResult(this.Options);
            }
            
            if (ret.IsEndOfBout)
            {
                this.IsOver = true;
                this.Fight.Result.Outcome = ret.Fighter1Win ? FightOutcome.Win : FightOutcome.Loss;
                this.Fight.Result.ResultType = ret.ResultType;
                this.Fight.Result.Rounds = this.Round;
            }
            else
            {
                if (ret.Fighter1Score > ret.Fighter2Score)
                {
                    this.Fighter1.RoundsWon++;
                }
                else if (ret.Fighter2Score > ret.Fighter1Score)
                {
                    this.Fighter2.RoundsWon++;
                }
                this.Fighter1.Knockdowns += ret.Fighter1Round.StunsCaused / 2;
                this.Fighter1.Stuns += ret.Fighter1Round.StunsCaused;
                this.Fighter2.Stuns += ret.Fighter2Round.StunsCaused;
                this.Fighter2.Knockdowns += ret.Fighter2Round.StunsCaused / 2;
                this.Fight.Fighter1Score += ret.Fighter1Score;
                this.Fight.Fighter2Score += ret.Fighter2Score;

                this.Fighter1.RecoverEndurance();
                ret.Fighter1Round.EndurancePostRecover = this.Fighter1.EndurancePoints;
                this.Fighter2.RecoverEndurance();
                ret.Fighter2Round.EndurancePostRecover = this.Fighter2.EndurancePoints;
            }
            return ret;
        }
        
        private void SimRound(Round round)
        {
            this.Fighter1.MakeAdjustments(this.Fighter2, this.Options);
            this.Fighter2.MakeAdjustments(this.Fighter1, this.Options);

           
            round.Fighter1Round = Fighter1.BeginFighting(this.Fighter2, round);
            round.Fighter2Round = Fighter2.BeginFighting(this.Fighter1, round);

            round.Fighter1Round.MultiplyDamageLuckFactor(Fighter1.RoundStats.LuckFactor);
            round.Fighter2Round.MultiplyDamageLuckFactor(Fighter2.RoundStats.LuckFactor);
            if (round.Fighter1Round.DamageDealt.StunValue > 1.5 && round.Fighter1Round.DamageDealt.StunValue > round.Fighter2Round.DamageDealt.StunValue)
            {
                Fighter2.RoundStats.LuckFactor *= 0.75;
                round.Fighter2Round.MultiplyDamageLuckFactor(0.75);
            }
            else if (round.Fighter2Round.DamageDealt.StunValue > 1.5 && round.Fighter2Round.DamageDealt.StunValue > round.Fighter1Round.DamageDealt.StunValue)
            {
                Fighter1.RoundStats.LuckFactor *= 0.75;
                round.Fighter1Round.MultiplyDamageLuckFactor(0.75);
            }

            round.Fighter1Round.DamageReceived = round.Fighter2Round.DamageDealt;
            round.Fighter2Round.DamageReceived = round.Fighter1Round.DamageDealt;
            round.Fighter1Round.SetCuts(Fighter1, round.Fighter2Round);
            round.Fighter2Round.SetCuts(Fighter2, round.Fighter1Round);

            round.Fighter1Round.SetPunches(Fighter1.RoundStats);
            round.Fighter2Round.SetPunches(Fighter2.RoundStats);
            Fighter1.TotalBaseDamage += round.Fighter1Round.DamageReceived.BaseDamage;
            Fighter2.TotalBaseDamage += round.Fighter2Round.DamageReceived.BaseDamage;
            Fighter1.FatigueRound(round.Fighter2Round.DamageDealt.EnduranceDamage);
            round.Fighter1Round.FatigueStartRound = Fighter1.FatigueLossStartRound;
            round.Fighter1Round.FatigueEndRound = Fighter1.GetEndOfRoundFatigue();

            Fighter2.FatigueRound(round.Fighter1Round.DamageDealt.EnduranceDamage);
            round.Fighter2Round.FatigueStartRound = Fighter2.FatigueLossStartRound;
            round.Fighter2Round.FatigueEndRound = Fighter2.GetEndOfRoundFatigue();

            round.Fighter1Round.IsTKOedByEndurance = Fighter1.EndurancePercent < 0;
            round.Fighter2Round.IsTKOedByEndurance = Fighter2.EndurancePercent < 0;


            round.Fighter1Round.Cuts = Fighter1.Cuts.Copy();
            round.Fighter2Round.Cuts = Fighter2.Cuts.Copy();

            round.Fighter1Round.EndEndurancePercent = Fighter1.EndurancePercent;
            round.Fighter2Round.EndEndurancePercent = Fighter2.EndurancePercent;
            round.Fighter1Round.EndEndurance = Fighter1.EndurancePoints;
            round.Fighter2Round.EndEndurance = Fighter2.EndurancePoints;

            round.Fighter1TotalScore = this.Fight.Fighter1Score;
            round.Fighter2TotalScore = this.Fight.Fighter2Score;

        }

        public override string ToString()
        {
            if (this.Fight == null) return base.ToString(); 
            if(!this.IsOver)
            {
                return String.Format("Round: {0}, Score: {1} - {2}", this.Round, this.Fight.Fighter1Score, this.Fight.Fighter2Score);
            }
            else
            {
                return this.Fight.ToString();
            }
        }
    }

}
