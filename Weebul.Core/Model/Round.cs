using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Helpers;
using Weebul.Data;

namespace Weebul.Core.Model
{
    public class Round
    {

        public Round()
        {

        }
        public Round(int round, FighterRound fighter1Round, FighterRound fighter2Round)
        {
            this.Fighter1Round = fighter1Round;
            this.Fighter2Round = fighter2Round;
            this.RoundNumber = round;     
        }
        public Round(Data.Round dataRound)
        {
            this.RoundNumber = dataRound.RoundNumber;
            this.Fighter1Score = dataRound.Fighter1Score;
            this.Fighter2Score = dataRound.Fighter2Score;
            this.ResultType = (FightResultType) dataRound.ResultType;
            if (this.ResultType != FightResultType.Decision)
            {
                this.Fighter1Win = (bool) dataRound.Fight.Fighter1Win;
            }
            this.Fighter1Round = new Model.FighterRound(dataRound.First, this);
            this.Fighter2Round = new Model.FighterRound(dataRound.Second, this);
            this.JudgeRounds = dataRound.JudgeRounds.Select(j => new Model.JudgeRound(j)).ToList();            
        }
        public Data.Round CreateToDatabase(int fightId)
        {
            WeebulEntities entities = DataHelpers.Entities;
            Data.Round r = entities.Rounds.Create();

            r.FightId = fightId;
            r.Fighter1Score = this.Fighter1Score;
            r.Fighter2Score = this.Fighter2Score;
            r.ResultType = (int) this.ResultType;
            r.RoundNumber = this.RoundNumber;
            r = entities.Rounds.Add(r);
            entities.SaveChanges();
            this.Fighter1Round.CreateInDatabase(r.RoundId, true);
            this.Fighter2Round.CreateInDatabase(r.RoundId, false);
            if (JudgeRounds != null && JudgeRounds.Count > 0)
            {
                foreach (JudgeRound jr in this.JudgeRounds)
                {
                    jr.CreateInDatabase(r.RoundId);
                }
            }
            return r;
        }

        public void SetTowel(bool fighter1Win)
        {
            IsEndOfBout = true;
            ResultType = FightResultType.TKO;
            Fighter1Win = fighter1Win; 

        }
        public void SetRoundResult(FightOptions options)
        {
            if (Fighter1Round.IsTowel)
            {
                IsEndOfBout = true;
                ResultType = FightResultType.TKO;
                Fighter1Win = false;
                return;
            }
            if(Fighter2Round.IsTowel)
            {
                IsEndOfBout = true;
                ResultType = FightResultType.TKO;
                Fighter1Win = true;
                return;
            }
            if (Fighter1Round.IsDisqualified)
            {
                IsEndOfBout = true;
                ResultType = FightResultType.DQ;
                Fighter1Win = false;
                return;
            }
            if(Fighter2Round.IsDisqualified)
            {
                IsEndOfBout = true;
                ResultType = FightResultType.DQ;
                Fighter1Win = true;
                return;
            }
          
            if(Fighter1Round.DamageDealt.StunValue > 2.5 && Fighter1Round.DamageDealt.StunValue > Fighter2Round.DamageDealt.StunValue)
            {
                IsEndOfBout = true;
                Fighter1Win = true;
                ResultType = FightResultType.Knockout;
                return; 
            }
            if (Fighter2Round.DamageDealt.StunValue > 2.5)
            {
                IsEndOfBout = true;
                Fighter1Win = false;
                ResultType = FightResultType.Knockout;
                return;
            }
            ScoreRound(options.JudgeLuck, options.Judges);

            if(Fighter1Round.IsTKOedByEndurance || Fighter1Round.IsTKOedByCut)
            {
                IsEndOfBout = true;
                Fighter1Win = false;
                ResultType = FightResultType.TKO;
                return; 
            }
            if(Fighter2Round.IsTKOedByEndurance || Fighter2Round.IsTKOedByCut)
            {
                IsEndOfBout = true;
                Fighter1Win = true;
                ResultType = FightResultType.TKO;
                return;
            }
            IsEndOfBout = false; 
            ResultType = FightResultType.Decision;
        }

        private void ScoreRound(double judgeLuck, int numberOfJudges)
        {
            FightScore fScore = GetRoundScore(Fighter1Round, Fighter2Round, Fighter1Round.GetScore(), Fighter2Round.GetScore());
            this.Fighter1Score = fScore.Fighter1Score;
            this.Fighter2Score = fScore.Fighter2Score;

            this.JudgeRounds = new List<JudgeRound>();

            for(int i = 1; i<= numberOfJudges; i++)
            {
                double score1 = Fighter1Round.GetScore() * RandomGen.GetRandomNormal(1, judgeLuck);
                double score2 = Fighter2Round.GetScore() * RandomGen.GetRandomNormal(1, judgeLuck);
                FightScore fs = GetRoundScore(this.Fighter1Round, this.Fighter2Round, score1, score2);
                JudgeRound jr = new Model.JudgeRound(i, fs.Fighter1Score, fs.Fighter2Score, this.RoundNumber);
                this.JudgeRounds.Add(jr);
            }

        }
     
        public static FightScore GetRoundScoreNoStun(double score1, double score2, double judgeLuck)
        {
            int fighter1Score = 10;
            int fighter2Score = 10;
            score1 = score1 * RandomGen.GetRandomNormal(1, judgeLuck);
            score2 = score2 * RandomGen.GetRandomNormal(1, judgeLuck);
            if (score1 > score2 && (score1 - score2) > Resources.RoundTieThreshold)
            {
                fighter2Score = 9;
            }
            else if (score2 > score1 && (score2 - score1) > Resources.RoundTieThreshold)
            {
                fighter1Score = 9;
            }
            return new Model.FightScore(fighter1Score, fighter2Score);
        }
        public static FightScore GetRoundScore(FighterRound fighter1Round, FighterRound fighter2Round, double score1, double score2)
        {
            int fighter1Score = 10;
            int fighter2Score = 10;       
            int stun1 = fighter1Round.StunsCaused;
            int stun2 = fighter2Round.StunsCaused;
            if (score1 / 4 > score2) stun1++;
            if (score2 / 4 > score1) stun2++; 
            if (stun1 > stun2)
            {
                fighter1Score = 10;
                fighter2Score = 9;
                int stunDiff = stun1 - stun2; 
                if (stunDiff >= 4)
                {
                    fighter2Score = 7;
                }
                else if (stunDiff >= 2)
                {
                   fighter2Score = 8;
                }
            }
            else if (stun2 > stun1)
            {
                fighter2Score = 10;
                fighter1Score = 9;
                int stunDiff = stun2 - stun1; 
                if (stunDiff >= 4)
                {
                    fighter1Score = 7;
                }
                else if (stunDiff >= 2)
                {
                    fighter1Score = 8;
                }
            }
            else
            {
                if (score1 > score2 && (score1 - score2) > Resources.RoundTieThreshold)
                {
                    fighter2Score = 9;
                }
                else if (score2 > score1 && (score2 - score1) > Resources.RoundTieThreshold)
                {
                    fighter1Score = 9;
                }
            }
            if (fighter1Round.DeductPointWarning)
            {
                fighter1Score--;
            }
            if (fighter2Round.DeductPointWarning)
            {
                fighter2Score--;
            }
            return new FightScore(fighter1Score, fighter2Score);
        }

        public FighterRound Fighter1Round { get; set; }

        public FighterRound Fighter2Round { get; set; }

        public bool IsEndOfBout { get; set; }
        
        public bool Fighter1Win { get; set; }
        
        public int Fighter1Score { get; set; }
        
        public int Fighter2Score { get; set; }


        public string TKOCut { get; set; }

        //TODO get rid of these 

        public int Fighter1TotalScore { get; set; }
        public int Fighter2TotalScore { get; set; }
        public string TotalScore
        {
            get
            {
                return String.Format("Score: {0} - {1}", Fighter1TotalScore, Fighter2TotalScore);
            }
        }
        public int RoundNumber { get; set; }
        public FightResultType ResultType { get; set; }
        public List<JudgeRound> JudgeRounds { get; set; }
        public string PrintString(bool detailed = true)
        {
            var sb = new StringBuilder();
            sb.Append(String.Format("Round: {0}, {1}", this.RoundNumber, this.ToString()));
            sb.AppendLine();
            if(this.Fighter1Round.Tactics != null)
            {
                sb.AppendLine(String.Format("Tactics: {0} vs. {1}", Fighter1Round.Tactics.ToString(), Fighter2Round.Tactics.ToString()));
            }
            sb.AppendFormat("Fighter 1 took {0:0.0} endurance damage and {1:0.0} stun damage",Fighter2Round.DamageDealt.EnduranceDamage, Fighter2Round.DamageDealt.StunDamage);
            sb.AppendLine();
            sb.AppendFormat("Fighter 2 took {0:0.0} endurance damage and {1:0.0} stun damage", Fighter1Round.DamageDealt.EnduranceDamage, Fighter1Round.DamageDealt.StunDamage);
            sb.AppendLine();
            sb.Append("Fighter 1 " + this.Fighter1Round.PunchStats.ToString());
            sb.AppendLine();
            sb.Append("Fighter 2 " + this.Fighter2Round.PunchStats.ToString());
            
            if (detailed)
            {
                sb.AppendLine();
                sb.AppendFormat("Endurance: {0:0.0%} - {1:0.0%}", Fighter1Round.EndEndurancePercent, Fighter2Round.EndEndurancePercent);
                sb.AppendLine();
                sb.AppendFormat("Fighter 1 Cuts: " + this.Fighter1Round.Cuts.ToString());
                sb.AppendLine();
                sb.AppendFormat("Fighter 2 Cuts: " + this.Fighter2Round.Cuts.ToString());
                sb.AppendLine();
                sb.AppendFormat(TotalScore);
                sb.AppendLine();
            }
            return sb.ToString(); 
        }


        public List<FighterRoundCompare> CompareDamageToCalculated()
        {

            FighterStats stats1 = Fight.Fighter1.AdjustAll(Fighter1Round.StartEndurance, Fighter1Round.Cuts_StartRound, Resources.FatigueBeforeCut);
            FighterStats stats2 = Fight.Fighter2.AdjustAll(Fighter2Round.StartEndurance, Fighter2Round.Cuts_StartRound, Resources.FatigueBeforeCut);            
            RoundDamage d1 = RoundDamage.CalculateRoundDamage(stats1, 
                Fighter1Round.Tactics,
                stats2, 
                Fighter2Round.Tactics, 
                Fighter1Round.Tactics.TargetArea);

            RoundDamage d2 = RoundDamage.CalculateRoundDamage(stats2, 
                Fighter2Round.Tactics, 
                stats1, 
                Fighter1Round.Tactics, 
                Fighter2Round.Tactics.TargetArea);

            RoundDamage.AdjustForStun(d1, d2);
            List<FighterRoundCompare> ret = new List<Model.FighterRoundCompare>()
            {
                new Model.FighterRoundCompare(Fighter1Round, d2),
                new Model.FighterRoundCompare(Fighter2Round, d1)
            };
            return ret; 
        }
           
        public override string ToString()
        {

            if (ResultType == FightResultType.DQ)
            {
                return String.Format("Fighter {0} won by disqualification", Fighter1Win ? "1" : "2");

            }
            else if (ResultType == FightResultType.Knockout)
            {
                return String.Format("Fighter {0} won by knockout", Fighter1Win ? "1" : "2");
            }
            else if (ResultType == FightResultType.TKO)
            {
                return String.Format("Fighter {0} won by TKO", Fighter1Win ? "1" : "2");
            }
            else
            {
                if (Fighter1Score == Fighter2Score)
                {
                    return String.Format("The round was a {0}-{0} tie", this.Fighter1Score);
                }
                else if (Fighter1Score > Fighter2Score)
                {
                    return String.Format("Fighter 1 won the round {0}-{1}", Fighter1Score, Fighter2Score);
                }
                else
                {
                    return String.Format("Fighter 2 won the round {0}-{1}", Fighter2Score, Fighter1Score);
                }
            }
        }



        public Fight Fight { get; set; }


    }
}
