using NUnit.Framework;
using Weebul.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.Entity;
using Weebul.Core.Model;
using Weebul.Core.Helpers;

namespace Weebul.Data.Tests
{
    [TestFixture]
    public class DataHelpersTests
    {
        [Test]
        public void LoadFightTest()
        {
            int fightId = 19;

            var something = DataHelpers.Entities.Fights.Where(f => f.FightId >= 19 && !f.BadResult).ToList();
            something.ForEach(s => s.SetCuts());
            var rounds = something.SelectMany(f => f.Rounds.Where(r => r.IsSingleCut())).ToList();

            Debug.Print(rounds.Count.ToString());
        }
        [Test]

        public void TestFighters()
        {
            DataHelpers.Entities.Fights.Load();

            var something = DataHelpers.Entities.Fights.Where(f => f.FightId >= 19 && !f.BadResult);
            foreach (Fight f in something)
            {
                f.SetCuts();
            }



        }
        [Test]
        public void TestAgain()
        {
            DataHelpers.Entities.Fights.Load();
            HashSet<CutBase> doNothingCuts = new HashSet<CutBase>();
            var something = DataHelpers.Entities.Fights.Where(f => f.FightId >= 19 && !f.BadResult);
            foreach (Fight fight in something)
            {                
                fight.SetCuts();
                FighterStats stats1 = new FighterStats(fight.Fighter1);
                FighterStats stats2 = new FighterStats(fight.Fighter2);
                foreach (Data.Round round in fight.Rounds)
                {
                    List<CutBase> cuts1 = FightHelpers.GetCutsForDataRound(round.First);
                    List<CutBase> cuts2 = FightHelpers.GetCutsForDataRound(round.Second);
                    CutPenalty cp1 = CutBase.CalculatePenaltyForCuts(FightHelpers.GetCutsForDataRound(round.First));
                    CutPenalty cp2 = CutBase.CalculatePenaltyForCuts(FightHelpers.GetCutsForDataRound(round.Second));
                    if (round.First.Tactic.Aggressiveness == 1) continue;
                    if (FightHelpers.RoundMatchesExpectations(round, stats1, stats2, true, 0))
                    {
                        if (!cp1.HasNone || !cp2.HasNone)
                        {
                            Debug.Print("HMMM");
                        }
                        if (cuts1.Count > 0 || cuts2.Count >0)
                        {
                            doNothingCuts.UnionWith(cuts1);
                            doNothingCuts.UnionWith(cuts2);
                        }
                            
                        
                    }
                    else
                    {
                        if(cp1.HasNone && cp2.HasNone)
                        {
                            Debug.Print("uh oh?");
                            if (FightHelpers.RoundMatchesExpectations(round, stats1, stats2, true, 0.1))

                            {
                                Debug.Print("Looks like, just a rounding thing");
                            }
                            else
                            {
                                Debug.Print("No it's worse");
                            }
                        }
                    }
                }
            }
            foreach(CutBase cb in doNothingCuts)
            {
                Debug.Print(cb.ToString() + " does nothing!");
            }
        }

    }
}