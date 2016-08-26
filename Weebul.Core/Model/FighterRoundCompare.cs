using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class FighterRoundCompare
    {

        public FighterRoundCompare(FighterRound round, RoundDamage computed)
        {
            this.FighterRound = round;
            this.Computed = computed;
            this.Diff = RoundDamage.Diff(Computed, round.DamageReceived);
        }
        
        public FighterRound FighterRound { get; set; }
        
        public RoundDamage Computed { get; set; }
        
        public string CutsStart
        {
            get
            {
                if (this.FighterRound.Cuts_StartRound == null) return "";

                return this.FighterRound.Cuts_StartRound.ToString();
            }
        }
        public RoundDamage Diff { get; set; }
    }
}
