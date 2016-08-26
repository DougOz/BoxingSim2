using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class JudgeFightScore : FightScore 
    {

        public JudgeFightScore(int judgeNumber, int fighter1Score, int fighter2Score) : base(fighter1Score, fighter2Score)
        {
            this.JudgeNumber = judgeNumber; 
        }  
        protected JudgeFightScore(int fighter1Score, int fighter2Score)
            : base(fighter1Score, fighter2Score)
        {
            
        }
        public int JudgeNumber { get; set; }

        public override string ToString()
        {
            return base.ToString(); 
        }

    }
}
