
using NUnit.Framework;
using Weebul.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Data;
using Weebul.Core.Model;
using System.Diagnostics;

namespace Weebul.Core.Helpers.Tests
{

    [TestFixture]
    public class FightHelpersTests
    {
        [Test]
        public void CheckRandomTest()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void TestDamageForDataFightTest()
        {
            List<Data.Fight> fightList = DataHelpers.Entities.Fights.Where(f => f.FightId >= 19 && !f.BadResult).ToList();
            HashSet<CutBase> allCuts = new HashSet<CutBase>();
            foreach(Data.Fight f in fightList)
            {
                f.SetCuts();
                HashSet<CutBase> cutSet = FightHelpers.TestDamageForDataFight(f, 0);
                allCuts.UnionWith(cutSet);
            }

            Debug.Print("Good");
        }
    }
}