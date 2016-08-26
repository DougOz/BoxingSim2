using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Weebul.Data;
namespace Weebul.Core.Model
{
    public class Fight
    {

        public Fight()
        {
            this.RoundResults = new List<Model.Round>();
            this.Result = new Model.FightResult();
        }
        public Fight(List<Round> roundResults)
        {
            this.Result = new FightResult();
            this.RoundResults = roundResults;
            Round last = roundResults.OrderByDescending(r => r.RoundNumber).First();
            this.Result.ResultType = last.ResultType;
            this.Result.Rounds = last.RoundNumber;
            this.Fighter1Score = roundResults.Sum(s => s.Fighter1Score);
            this.Fighter2Score = roundResults.Sum(s => s.Fighter2Score);

            SetJudgeScores(); 
            if (last.IsEndOfBout)
            {
                this.Result.Outcome = last.Fighter1Win ? FightOutcome.Win : FightOutcome.Loss;
                this.Result.TkoCut = last.TKOCut;
            }
            else
            {
                this.Result.Outcome = GetDecisionResult(); 

                if (this.Fighter1Score > this.Fighter2Score)
                {
                    this.Result.Outcome = FightOutcome.Win;
                }
                else if (this.Fighter1Score == this.Fighter2Score)
                {
                    this.Result.Outcome = FightOutcome.Draw;
                }
                else
                {
                    this.Result.Outcome = FightOutcome.Loss;
                }
            }
        }

        public FightOutcome GetDecisionResult()
        {
            int wins = JudgeScores.Count(j => j.Fighter1Score > j.Fighter2Score);
            int losses = JudgeScores.Count(j => j.Fighter2Score > j.Fighter1Score);

            if(wins > losses)
            {
                return FightOutcome.Win;
            }
            else if (losses > wins)
            {
                return FightOutcome.Loss; 
            }
            return FightOutcome.Draw;             
        }
        public void SetJudgeScores()
        {
            this.JudgeScores = new List<Model.JudgeFightScore>();

            if(!this.RoundResults.Any(r=>r.JudgeRounds != null && r.JudgeRounds.Count > 0))
            {
                int f1Score = this.RoundResults.Sum(r => r.Fighter1Score);
                int f2Score = this.RoundResults.Sum(r => r.Fighter2Score);
                this.JudgeScores.Add(new JudgeFightScore(1, f1Score, f2Score));
            }
            else
            {
                foreach(int judgeNumber in this.RoundResults.Where(r=>r.JudgeRounds != null).SelectMany(r => r.JudgeRounds).Select(r => r.JudgeNumber).Distinct())
                {
                    AddJudgeScore(judgeNumber); 
                }
            }
        }

        private void AddJudgeScore(int judgeNumber)
        {
            var judgeRounds = this.RoundResults.Where(r=>r.JudgeRounds != null && r.JudgeRounds.Count > 0).Select(r => r.JudgeRounds.First(j => j.JudgeNumber == judgeNumber));

            int f1Score = judgeRounds.Sum(j => j.Score.Fighter1Score);
            int f2Score = judgeRounds.Sum(j => j.Score.Fighter2Score);
            this.JudgeScores.Add(new Model.JudgeFightScore(judgeNumber, f1Score, f2Score));

        }
        public Fight(Data.Fight fight)
        {
            this.Fighter1Score = fight.Fighter1Score;
            this.Fighter2Score = fight.Fighter2Score;
            this.Fighter1 = new Model.FighterStats(fight.Fighter1);
            this.Fighter2 = new Model.FighterStats(fight.Fighter2);
            FightOutcome outcome = FightOutcome.Draw;
            if (fight.Fighter1Win == true)
            {
                outcome = FightOutcome.Win;
            }
            else if (fight.Fighter1Win == false)
            {
                outcome = FightOutcome.Loss;
            }
            SetJudgeScores(); 
            this.Result = new FightResult()
            {
                Outcome = outcome,
                Rounds = fight.NumberOfRounds,
                ResultType = (FightResultType) fight.ResultType,
                TkoCut = fight.TkoCut 
            };
            this.RoundResults = new List<Model.Round>();

            foreach (Data.Round r in fight.Rounds)
            {
                Round rr = new Model.Round(r)
                { Fight = this };
                rr.Fighter1Round.SetPercentages(fight.Fighter1.Conditioning * 10);
                rr.Fighter2Round.SetPercentages(fight.Fighter2.Conditioning * 10);
                this.RoundResults.Add(rr);
            }
        }

        public int Fighter1Score { get; set; }
        public int Fighter2Score { get; set; }

        public List<JudgeFightScore> JudgeScores { get; set; }
        public FighterStats Fighter1 { get; set; }       
        public FighterStats Fighter2 { get; set; }
        public FightResult Result { get; set; }
        public List<Round> RoundResults { get; set; }
        public Data.Fight SaveToDatabase(FightOptions options, int fighter1Id, int fighter2Id, int plan1Id, int plan2Id, string parseText)
        {
            {
                WeebulEntities entities = Data.DataHelpers.Entities;
                entities.Fights.Load();
                FightOption o = entities.FightOptions.Create();
                o.CutMultiplier = options.CutFactor;
                o.Luck = options.LuckAmount;
                o.NumberOfRounds = options.TotalRounds;
                o.WeightClass = (int) options.WeightClass;
                o = entities.FightOptions.Add(o);
                int upd = entities.SaveChanges();
                Data.Fight f = entities.Fights.Create();
                f.Fighter1Id = fighter1Id;
                f.Fighter2Id = fighter2Id;
                f.Fighter1PlanId = plan1Id;
                f.Fighter2PlanId = plan2Id;

                f.Fighter1Score = this.Fighter1Score;
                f.Fighter2Score = this.Fighter2Score;

                f.NumberOfRounds = this.Result.Rounds;
                f.OptionsId = o.OptionsId;

                bool? fighter1Win = null;

                if (this.Result.Outcome == FightOutcome.Win)
                {
                    fighter1Win = true;
                }
                else if (this.Result.Outcome == FightOutcome.Loss)
                {
                    fighter1Win = false;
                }
                f.Fighter1Win = fighter1Win;
                f.IsParsed = true;
                f.ResultType = (int) this.Result.ResultType;

                f = entities.Fights.Add(f);
                f.Date = DateTime.Now;
                f.ParseText = parseText;
                f.TkoCut = this.Result.TkoCut;
                f.CutLevelF1 = this.RoundResults.Last().Fighter1Round.CutLevel;
                f.CutLevelF2 = this.RoundResults.Last().Fighter2Round.CutLevel;
                entities.SaveChanges();
                foreach (Round rr in this.RoundResults)
                {
                    rr.CreateToDatabase(f.FightId);
                }
                entities.SaveChanges();
                return f;
            }
        }
        public List<int> LineHits_Fighter1 { get; set; }

        public List<int> LineHits_Fighter2 { get; set; }
        public void SetCutsStartRound()
        {
            Round prevRound = null;
            
            foreach(Round r in this.RoundResults.OrderBy(r=>r.RoundNumber))
            {
                if(prevRound != null)
                {
                    r.Fighter1Round.Cuts_StartRound = prevRound.Fighter1Round.Cuts;
                    r.Fighter2Round.Cuts_StartRound = prevRound.Fighter2Round.Cuts; 
                }
                else
                {
                    r.Fighter1Round.Cuts_StartRound = new CutList();
                    r.Fighter2Round.Cuts_StartRound = new CutList();
                }                
                prevRound = r; 
            }
            
        }
        public override string ToString()
        {

            if (this.Result.Outcome == FightOutcome.Draw)
            {
                return String.Format("Draw: {0}", ScoreString());
            }
            else
            {
                string winner = (this.Result.Outcome == FightOutcome.Win) ? "Fighter 1" : "Fighter 2";
                return String.Format("{0} won in {1} rounds by {2}, {3}", winner, this.Result.Rounds,
                    Enum.GetName(typeof(FightResultType), this.Result.ResultType), ScoreString());
            }

        }
        public string ScoreString()
        {
            if(this.JudgeScores == null || this.JudgeScores.Count == 0)
            {
                return String.Format("{0}-{1}", this.Fighter1Score, this.Fighter2Score);
            }
            else
            {
                return String.Join(", ", this.JudgeScores.Select(j => j.ToString()));
            }
        }
        public void SetFightOutcomeDecision()
        {
            FightOutcome outcome = GetDecisionResult();
            this.Result = new Model.FightResult(outcome, this.RoundResults.Count, FightResultType.Decision);            
        }
        public List<FighterRoundCompare> CompareToCalculated()
        {
            SetCutsStartRound();
            foreach(Round round in this.RoundResults)
            {
                round.Fight = this; 
            }
            List<FighterRoundCompare> ret = new List<Model.FighterRoundCompare>();

            foreach(Round r in this.RoundResults.OrderBy(r=>r.RoundNumber))
            {
                ret.AddRange(r.CompareDamageToCalculated());
            }
            return ret; 
        }
    }
}
