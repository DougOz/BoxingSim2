using NUnit.Framework;
using Weebul.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model.Tests
{
    [TestFixture]
    public class FighterRoundStatsTests
    {
   
        [Test]
        public void FighterRoundStatsTest()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void AdjustStyleTest()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void AdjustAlloutTest()
        {
            FighterStats fst = GetFighterStats();
            FighterStats other = GetFighterStats();

            FighterRoundStats frs = new FighterRoundStats(fst, new FighterRoundPlan(4, 8, 8));
            FighterRoundStats otherFrs = new FighterRoundStats(other, new FighterRoundPlan(4, 8, 8));

            frs.AdjustAllout(otherFrs);

            Assert.AreEqual(2.0, frs.DamageAdjustment);
            Assert.AreEqual(4.0, otherFrs.DamageAdjustment);
        }

        [Test]
        public void AdjustClinchTest()
        {
            FighterStats fst = GetFighterStats();
            fst.Strength = 18;
            FighterStats other = GetFighterStats();
            
            FighterRoundStats frs = new FighterRoundStats(fst, 
                new FighterRoundPlan(4, 8, 8));
            FighterRoundStats otherRs = 
                new FighterRoundStats(other, 
                new FighterRoundPlan(4, 8, 8));

            frs.AdjustClinch(otherRs);

            FighterStats expected = fst.Copy();

   
            expected.Agility = 16;
            Assert.AreEqual(expected, frs.AdjustedStats);
            FighterTactics tactics = frs.Plan.Copy();
            tactics.Aggressiveness = 3.4;
            tactics.Rest = 0.6;
            Assert.AreEqual(tactics, frs.AdjustedTactics);

        }

        [Test]
        public void AdjustCounterTest()
        {
            FighterStats f1 = GetFighterStats();
            f1.Speed = 15;
            f1.Height = 15;
            FighterStats f2 = GetFighterStats();

            FighterRoundStats frs1 = GetFighterRoundStats(f1, FightingStyle.Counter);
            FighterRoundStats frs2 = GetFighterRoundStats(f2, FightingStyle.Counter);

            FighterStats expected = f1.Copy();
            expected.Strength++;
            expected.Agility += 2;
            frs1.AdjustCounter(frs2);
            Assert.AreEqual(expected, frs1.AdjustedStats);

            FighterTactics tactics = frs1.Plan.Copy();
            tactics.Aggressiveness = 3.4;
            Assert.AreEqual(tactics, frs1.AdjustedTactics);
            expected = f2.Copy();
            expected.Agility = 10;
            Assert.AreEqual(expected, frs2.AdjustedStats);
        }

        [Test] 
        public void AdjustCounter_NegativeTest()
        {
            FighterStats f1 = GetFighterStats();
            
            FighterStats f2 = GetFighterStats();
            f2.Height = 15;
            f2.Speed = 15; 
            FighterRoundStats frs1 = GetFighterRoundStats(f1, FightingStyle.Counter);
            FighterRoundStats frs2 = GetFighterRoundStats(f2, FightingStyle.Counter);

            FighterStats expected = f1.Copy();
            expected.Strength++;
            expected.Agility -= 2; 
            frs1.AdjustCounter(frs2);
            Assert.AreEqual(expected, frs1.AdjustedStats);

            FighterTactics tactics = frs1.Plan.Copy();
            tactics.Aggressiveness = 3.4;
            Assert.AreEqual(tactics, frs1.AdjustedTactics);
            expected = f2.Copy();
            expected.Agility = 14; 
            Assert.AreEqual(expected, frs2.AdjustedStats);
        }
        [Test]
        public void AdjustFeintTest()
        {
            FighterStats f1 = GetFighterStats();
            f1.Speed = 20;
            FighterStats f2 = GetFighterStats();
            FighterRoundStats frs1 = GetFighterRoundStats(f1, FightingStyle.Feint);
            FighterRoundStats frs2 = GetFighterRoundStats(f2, FightingStyle.None);
            frs1.AdjustFeint(frs2);


            FighterStats expected = f1.Copy();
            expected.Speed = 25;
            Assert.AreEqual(expected, frs1.AdjustedStats);
            Assert.AreEqual(1, frs1.AdditionalFatigue);
        }

        [Test]
        public void AdjustInsideTest()
        {
            FighterStats f1 = GetFighterStats();
            f1.Strength = 20;
            FighterStats f2 = GetFighterStats();
            FighterRoundStats frs1 = GetFighterRoundStats(f1, FightingStyle.Inside);
            FighterRoundStats frs2 = GetFighterRoundStats(f2, FightingStyle.None);
            frs1.AdjustInside(frs2);


            FighterStats expected = f1.Copy();
            expected.Strength = 25;
            Assert.AreEqual(expected, frs1.AdjustedStats);
            Assert.AreEqual(1.1, frs2.DamageAdjustment);
        }

        [Test]
        public void AdjustOutsideTest()
        {
            FighterStats f1 = GetFighterStats();
            f1.Height = 18;
            FighterStats f2 = GetFighterStats();
            FighterRoundStats frs1 = GetFighterRoundStats(f1, FightingStyle.Outside);
            FighterRoundStats frs2 = GetFighterRoundStats(f2, FightingStyle.None);
            frs1.AdjustOutside(frs2);


            FighterStats expected = f1.Copy();
            expected.Agility = 12.5;
            expected.Speed = 12.5;
            expected.Height = 21; 
            Assert.AreEqual(expected, frs1.AdjustedStats);
            FighterTactics tactics = frs1.Plan.Copy();
            tactics.Power = 6.8;

            Assert.AreEqual(tactics, frs1.AdjustedTactics);
        }

        [Test]
        public void AdjustRingTest()
        {
            FighterStats f1 = GetFighterStats();
            f1.Agility = 18;
            FighterStats f2 = GetFighterStats();
            FighterRoundStats frs1 = GetFighterRoundStats(f1, FightingStyle.Ring);
            FighterRoundStats frs2 = GetFighterRoundStats(f2, FightingStyle.None);
            frs1.AdjustRing(frs2);


            FighterStats expected = f1.Copy();
            expected.Agility = 22; 
            Assert.AreEqual(expected, frs1.AdjustedStats);
            Assert.AreEqual(1, frs1.AdditionalFatigue);
        }

        [Test]
        public void AdjustRopesTest()
        {
            FighterStats f1 = GetFighterStats();
            f1.Agility = 18;
            
            FighterStats f2 = GetFighterStats();

            FighterRoundStats frs1 = GetFighterRoundStats(f1, FightingStyle.Ropes);
            FighterRoundStats frs2 = GetFighterRoundStats(f2, FightingStyle.Ropes);

            FighterStats expected = f1.Copy();
            expected.Agility = 17;
            frs1.AdjustRopes(frs2);
            Assert.AreEqual(expected, frs1.AdjustedStats);

            expected = f2.Copy();
            expected.Agility = 8; 
            Assert.AreEqual(expected, frs2.AdjustedStats);
        }


        [Test]
        public void AdjustRopesBelow8Test()
        {
            FighterStats f1 = GetFighterStats();
            f1.Agility = 24;

            FighterStats f2 = GetFighterStats();

            FighterRoundStats frs1 = GetFighterRoundStats(f1, FightingStyle.Ropes);
            FighterRoundStats frs2 = GetFighterRoundStats(f2, FightingStyle.Ropes);

            FighterStats expected = f1.Copy();
            expected.Agility = 23;
            frs1.AdjustRopes(frs2);
            Assert.AreEqual(expected, frs1.AdjustedStats);

            expected = f2.Copy();
            expected.Agility = 8;
            Assert.AreEqual(expected, frs2.AdjustedStats);
        }
        [Test]
        public void FixAdjustmentsTest()
        {
            Assert.IsTrue(true);
        }

        public static FighterStats GetFighterStats()
        {
            FighterStats fst = new FighterStats(
              height: 12,
              speed: 12,
              agility: 12,
              strength: 12,
              knockoutPunch: 0,
              conditioning: 12,
              chin: 12,
              cutResistance: 1,
              weight:150, numberOfFights:1, rating:0);
            return fst; 
        }

        private static FighterRoundStats GetFighterRoundStats(FighterStats fighter, FightingStyle style)
        {
            return new FighterRoundStats(fighter, new FighterRoundPlan(4, 8, 8, style, TargetArea.Opportunistic, false));
        }
    }
}