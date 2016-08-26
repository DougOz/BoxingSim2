
using NUnit.Framework;
using Weebul.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Weebul.Core.Model.FighterRound;

namespace Weebul.Core.Model.Tests
{
    [TestFixture()]
    public class FighterRoundTests
    {
        public const int NumTimes = 50000;
        [TestCase(true, 8, 0, NumTimes, 0.5, 0.01)]
        [TestCase(false, 8, 0, NumTimes, 0.01, 0.005)]
        [TestCase(false, 15, 0, NumTimes, 0.25, 0.01)]
        [TestCase(true, 15, 0, NumTimes, 0.625, 0.01)]
        public void CheckWarningWarningPercent(bool dirty, int defense, int warnings, int numTimes, double expected, double err = 0.01)
        {
            int warningTimes =
            CheckWarnings(dirty, defense, warnings, numTimes, false);
            double actual = (double) warningTimes / numTimes;

            Assert.AreEqual(expected, actual, err);

        }
        [Test]
        [TestCase(true, 8, 0, NumTimes, 0.025, 0.01)]
        [TestCase(false, 8, 0, NumTimes, 0,0)]
        [TestCase(true, 8, 1, NumTimes, 0.05, 0.01)]
        [TestCase(true, 15,2,NumTimes,0.125,0.01)]
        public void CheckWarningDqPercent(bool dirty, int defense, int warnings, int numTimes, double expected, double err = 0.01)
        {
            int dqTimes =
            CheckWarnings(dirty, defense, warnings, numTimes, true);
            double actual = (double) dqTimes / numTimes;

            Assert.AreEqual(expected, actual, err);
        }

        private static int CheckWarnings(bool dirty, int defense, int warnings, int numTimes, bool checkDq)
        {
            int times = 0;

            for (int i = 0; i < numTimes; i++)
            {
                WarningResult res = FighterRound.CheckWarning(dirty, warnings, FightingStyle.Clinch, defense);
                if (checkDq && res.IsDisqualified) times++;
                if (!checkDq && res.IsWarned) times++;
            }
            return times;
        }

        [Test]
        [TestCase(0, ExpectedResult =0.05)]
        [TestCase(1, ExpectedResult = 0.10)]
        [TestCase(2, ExpectedResult = 0.20)]
        public double DqPercentTest(int warnings)
        {
            double ret = FighterRound.DqPercent(warnings);
            return ret;
        }
    }
}