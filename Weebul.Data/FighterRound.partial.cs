using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Data
{
    public partial class FighterRound
    {
        private bool _cutsLoaded = false;
        private List<Cut> _previousCuts = new List<Cut>(); 
        public List<Cut> _cuts = null; 
		public List<Cut> Cuts
        {
            get
            {
                if (_cuts == null)
                {
                    _cuts = this.FighterRoundCuts.Select(f => f.Cut).ToList();
                }
                return _cuts;
            }
        }

        public List<Cut> PreviousCuts { get; set; }

		public IEnumerable<Cut> CutsAtStart()
        {            
            FighterRound prevRound = Previous();
            if (prevRound == null) return new List<Cut>();
            return prevRound.Cuts; 
        }
		public FighterRound Previous()
        {
            Round prev = this.Round.Fight.Rounds.FirstOrDefault(r => r.RoundNumber == this.Round.RoundNumber - 1);

            if (prev == null) return null;
            FighterRound ret = prev.FighterRounds.FirstOrDefault(fr => fr.IsFighter1 == this.IsFighter1);
            return ret;
        }
		public FighterRound Next()
        {
            Round next = this.Round.Fight.Rounds.FirstOrDefault(r => r.RoundNumber == this.Round.RoundNumber + 1);

            if (next == null) return null;
			FighterRound ret = next.FighterRounds.FirstOrDefault(fr => fr.IsFighter1 == this.IsFighter1);
            return ret;
        }
    }
}
