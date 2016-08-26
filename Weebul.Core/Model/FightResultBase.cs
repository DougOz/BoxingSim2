using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class FightResultBase : ModelBase, IEquatable<FightResultBase>
    {
        public FightOutcome Outcome { get; set; }

        

        public FightResultType ResultType { get; set; }

        public bool Equals(FightResultBase other)
        {
            return other != null && other.Outcome == this.Outcome && other.ResultType == this.ResultType;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 11;
                int mult = 19;
                hash = hash * mult + Outcome.GetHashCode();
                hash = hash * mult + ResultType.GetHashCode();
                return hash; 
            }
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as FightResultBase);
        }
    }
}
