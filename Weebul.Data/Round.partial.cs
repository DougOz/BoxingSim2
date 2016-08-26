using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Data
{
    public partial class Round
    {

        public bool IsSingleCut()
        {

            IEnumerable<Cut> cut1 = First.PreviousCuts;
            IEnumerable<Cut> cut2 = Second.PreviousCuts; 
            bool ret = false; 
            if(cut1?.Count() == 1)
            {
                ret = !cut2.Any();
            }
            else if (cut2?.Count() == 1)
            {
                ret = !cut1.Any();
            }
            return ret; 
        }
        
        public FighterRound First
        {
            get
            {
                return this.FighterRounds.FirstOrDefault(f => f.IsFighter1);
            }
        }
        public FighterRound Second
        {
            get
            {
                return this.FighterRounds.FirstOrDefault(f => !f.IsFighter1);
            }
        }
    }
}
