using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class FightScore
    {
        public FightScore(int fighter1Score, int fighter2Score)
        {
            this.Fighter1Score = fighter1Score;
            this.Fighter2Score = fighter2Score;
        }

        public int Fighter1Score { get; set; }
        public int Fighter2Score { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - {1}", this.Fighter1Score, this.Fighter2Score);
        }

    }
}
