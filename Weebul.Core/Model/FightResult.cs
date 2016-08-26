using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class FightResult : FightResultBase , IEquatable<FightResult>
    {

        public FightResult()
        { }

        public FightResult(FightOutcome outcome, int rounds, FightResultType resultType)
        {
            this.Outcome = outcome;
            this.Rounds = rounds;
            this.ResultType = resultType;
        }

        public int Rounds { get; set; }


        public bool Equals(FightResult other)
        {
            return other != null && this.Outcome == other.Outcome && this.Rounds == other.Rounds && this.ResultType == other.ResultType;
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as FightResult);
        }
        public string TkoCut { get; set; }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                int mult = 13;
                hash = hash * mult + Outcome.GetHashCode();
                hash = hash * mult + Rounds.GetHashCode();
                hash = hash * mult + ResultType.GetHashCode();
                return hash; 
            }
        }
    }
}
