using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace Weebul.Data
{
    public static class DataHelpers
    {

        private static Lazy<WeebulEntities> _entities = new Lazy<WeebulEntities>();
        public static WeebulEntities Entities
        {
            get
            {
                return _entities.Value;
            }
        }

        public static IEnumerable<Fight> LoadFights()
        {
            return Entities.Fights
                 .Include(a => a.Fighter1)
                 .Include(a => a.Fighter2)
                 .Include(a => a.FightOption)
                 .Include(a => a.Rounds)
                 .Include(a => a.Rounds.Select(r => r.JudgeRounds))
                 .Include(a => a.Rounds.Select(r => r.FighterRounds))

                 .Include(a => a.Rounds.Select(r => r.FighterRounds.Select(fr => fr.FighterRoundCuts)))
                 .Include(a => a.Rounds.Select(r => r.FighterRounds.Select(fr => fr.FighterRoundCuts.Select(c => c.Cut))))
                 .Include(a => a.Rounds.Select(r => r.FighterRounds.Select(fr => fr.Tactic)));
        }
        public static Fight LoadFight(int fightId)
        {

            var v = LoadFights().Where(r=>r.FightId == fightId).FirstOrDefault();
                 //.Include(a => a.Fighter1)
                 //.Include(a => a.Fighter2)
                 //.Include(a => a.FightOption)
                 //.Include(a => a.Rounds)
                 //.Include(a => a.Rounds.Select(r => r.JudgeRounds))
                 //.Include(a => a.Rounds.Select(r => r.FighterRounds))

                 //.Include(a => a.Rounds.Select(r => r.FighterRounds.Select(fr => fr.FighterRoundCuts)))
                 //.Include(a => a.Rounds.Select(r => r.FighterRounds.Select(fr => fr.FighterRoundCuts.Select(c=>c.Cut))))
                 //.Include(a => a.Rounds.Select(r => r.FighterRounds.Select(fr => fr.Tactic))).FirstOrDefault();
            
            return v; 
            
        }
    }
}
