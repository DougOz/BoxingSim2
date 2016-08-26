using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class FightResultSet
    {
        public FightResultSet()
        { }
        
        public FightResultSet(IEnumerable<FightResultBase> results)
        {
            this.Results = results.GroupBy(r => r).ToDictionary(r => r.Key, r => r.Count());
        }

        public int Wins
        {
            get
            {
                return this.Results.Where(r => r.Key.Outcome == FightOutcome.Win).Sum(r => r.Value);
            }
        }

        public int Draws
        {
            get
            {
                return this.Results.Where(r => r.Key.Outcome == FightOutcome.Draw).Sum(r => r.Value);
            }
        }
        
        public int Losses
        {
            get
            {
                return this.Results.Where(r => r.Key.Outcome == FightOutcome.Loss).Sum(r => r.Value);
            }
        }

        public int KOWins
        {
            get
            {
                return this.Results.Where(r => r.Key.Outcome == FightOutcome.Win && 
                (r.Key.ResultType == FightResultType.Knockout || r.Key.ResultType == FightResultType.TKO)).Sum(r => r.Value);
            }
        }
        public int KOLosses
        {
            get
            {
                return this.Results.Where(r => r.Key.Outcome == FightOutcome.Loss &&
                (r.Key.ResultType == FightResultType.Knockout || r.Key.ResultType == FightResultType.TKO)).Sum(r => r.Value);
            }
        }
        public int Count
        {
            get
            {
                return this.Results.Sum(r => r.Value);
            }
        }

   
        public double WinPercentage
        {
            get
            {
                double wPct = (Wins + 0.5 * Draws) / Count;

                return wPct;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} Wins ({1} by KO), {2} Draws, {3} Losses ({4} by KO) {5:0.0%}", Wins, KOWins, Draws, Losses, KOLosses, WinPercentage);
        }
        public Dictionary<FightResultBase, int> Results { get; set; }
    }
}
