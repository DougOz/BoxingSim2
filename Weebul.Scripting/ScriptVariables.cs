using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Scripting
{
    public class ScriptVariables
    {
        public const int Strong = 2;
        public const int Tired = 1;
        public const int Weak = 0; 

        public int Round { get; set; }

        public int EndurancePercent { get; set; }

        public int Endurance { get; set; }
        
        public int Score { get; set; }

        public int RoundsWon { get; set; }
        public int RoundsLost { get; set; }
        public bool DecisionWon { get; set; }
        public bool DecisionLost { get; set; }
        public int MyCuts { get; set; }
        public int HisCuts { get; set; }
        public int HisStuns { get; set; }
        public int MyStuns { get; set; }
        public int Warnings { get; set; }
        public int MyKnockdowns { get; set; }
        public int HisKnockdowns { get; set; }

        public int Opponent { get; set; }

        public bool CanTowel { get; set; }

    }
}
