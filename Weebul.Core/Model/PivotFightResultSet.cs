using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class PivotFightResultSet : FightResultSet
    {
        public PivotFightResultSet(IEnumerable<FightResultBase> results) : base(results)
        {
        }
        public PivotFightResultSet(FightResultSet baseObj) : this(baseObj.Results)
        {            
        }
        public PivotFightResultSet(Dictionary<FightResultBase, int> results) : base ()
        {
            this.Results = results; 
        }
        
        public bool IsBestInColumn { get; set; }
        public bool IsBestInRow { get; set; }

        public bool IsWorstInRow { get; set; }
        public bool IsWorstInColumn { get; set; }

        public static PivotFightResultSet Combine(PivotFightResultSet first, PivotFightResultSet second)
        {
            Dictionary<FightResultBase, int> dict = first.Results.ToDictionary(r => r.Key, r => r.Value);

            foreach (var v in second.Results)
            {
                if (dict.ContainsKey(v.Key))
                {
                    dict[v.Key] += v.Value;
                }
                else
                {
                    dict[v.Key] = v.Value;
                }
            }
            return new PivotFightResultSet(dict);
        }
        public static PivotFightResultSet CombineMany(IEnumerable<PivotFightResultSet> results)
        {
            var dict = results.SelectMany(r=>r.Results).GroupBy(r=>r.Key).ToDictionary(r=>r.Key, r=> r.Sum(s=>s.Value));
            return new PivotFightResultSet(dict);
        }
    }
}
