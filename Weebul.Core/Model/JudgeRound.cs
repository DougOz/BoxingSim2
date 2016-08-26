using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Data;

namespace Weebul.Core.Model
{
    public class JudgeRound
    {
        public JudgeRound(int judgeNumber, int fighter1Score, int fighter2Score, int roundNumber)
        {
            this.JudgeNumber = judgeNumber;

            this.Score = new Model.FightScore(fighter1Score, fighter2Score);

            this.RoundNumber = roundNumber;
        }
        public JudgeRound(Data.JudgeRound dataRound) : this(dataRound.JudgeNumber, dataRound.Fighter1Score, dataRound.Fighter2Score, dataRound.Round.RoundNumber)
        {

        }
        public int JudgeNumber { get; set; }

        public FightScore Score { get; set; }

        public int RoundNumber { get; set; }


        public Data.JudgeRound CreateInDatabase(int roundId)
        {
            WeebulEntities entities = DataHelpers.Entities;
            Data.JudgeRound dataRound = entities.JudgeRounds.Create();

            dataRound.RoundId = roundId;
            dataRound.JudgeNumber = JudgeNumber;
            dataRound.Fighter1Score = Score.Fighter1Score;
            dataRound.Fighter2Score = Score.Fighter2Score;
            dataRound = entities.JudgeRounds.Add(dataRound);
            return dataRound;
        }
        public override string ToString()
        {

            return String.Format("Round: {0} Score: {1}-{2}", RoundNumber, Score.Fighter1Score, Score.Fighter2Score);
        }
    }
}
