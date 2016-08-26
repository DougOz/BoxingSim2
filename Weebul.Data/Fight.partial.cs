using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Data
{
    public partial class Fight
    {

        public void SetCuts()
        {

            List<Cut> prevCuts = new List<Data.Cut>();
            Round prev = null;
            foreach (Round r in this.Rounds.OrderBy(r => r.RoundNumber))
            {
                if(prev != null)
                {
                    r.First.PreviousCuts = prev.First.Cuts;
                    r.Second.PreviousCuts = prev.Second.Cuts;
                }
                prev = r; 
            }
            
        }
    }
}
