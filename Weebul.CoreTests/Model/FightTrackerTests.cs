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
    public class FightTrackerTests
    {
        [Test]
        public void FightRoundTest()
        {

            for (int i = 1; i <= 50; i++)
            {
                Fighter f = new Fighter()
                {
                    Stats = FighterRoundStatsTests.GetFighterStats()
                };
                f.Stats.Weight = 135;
                FighterFight fFight = new FighterFight(f, new FightPlan()
                {
                    Default = new FighterRoundPlan(4, 8, 8)
                });

                Fighter f2 = new Fighter()
                {
                    Stats = FighterRoundStatsTests.GetFighterStats()
                };
                FighterFight fFight2 = new FighterFight(f2, new FightPlan()
                {
                    Default = new FighterRoundPlan(4, 8, 8)
                });
           

                FightPlan defPlan = new FightPlan()
                {
                    Default = new FighterRoundPlan(5, 9, 6)
                };
                FightOptions options = new FightOptions()
                {
                    WeightClass = WeightClass.Light
                };
                FightTracker tracker = new FightTracker(f, f2, defPlan, defPlan, options);
                tracker.PlayFight();
            }
        }
    }
}