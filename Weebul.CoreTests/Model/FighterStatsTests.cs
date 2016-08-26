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
    public class FighterStatsTests
    {
        [Test]
        public void FatigueStatsTest()
        {
            FighterStats fStats = new FighterStats(height: 10, speed: 10, agility: 15, strength: 10, knockoutPunch: 1, 
                conditioning: 10, chin: 15, cutResistance: 1, weight: 150, numberOfFights: 0, rating: 0);

            FighterStats fatigued = FighterStats.FatigueStats(fStats, 90);
            FighterStats expected = new FighterStats(height: 10, speed: 9, agility: 13.5, strength: 9, knockoutPunch: 1,
                conditioning: 10, chin: 15, cutResistance: 1, weight: 150, numberOfFights: 0, rating: 0)
            {
                FatiguePercent = 0.9
            };

            
            Assert.AreEqual(expected, fatigued);

        

        }
        [Test]
        public void FatigueStats_StunDefenseTest()
        {
            FighterStats fStats = new FighterStats(height: 10, speed: 10, agility: 15, strength: 10, knockoutPunch: 1,
                conditioning: 10, chin: 15, cutResistance: 1, weight: 150, numberOfFights: 0, rating: 0);

            FighterStats fatigued = FighterStats.FatigueStats(fStats, 90);
            const double expected = 18;
            double actual = fatigued.StunDefense; 

            Assert.AreEqual(expected, actual);



        }

    }
}