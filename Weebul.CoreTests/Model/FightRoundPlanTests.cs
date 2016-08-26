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
    public class FightRoundPlanTests
    {
        [Test]
        public void ParseTest()
        {
            string toParse = "4H/10!/5 (allout)";
            FighterRoundPlan expected = new FighterRoundPlan(4, 10, 5, FightingStyle.AllOut, TargetArea.Head, true);

            FighterRoundPlan actual = FighterRoundPlan.Parse(toParse);

            Assert.AreEqual(expected, actual);
        }
    }
}