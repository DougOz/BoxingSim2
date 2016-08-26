using NUnit.Framework;
using Weebul.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data = Weebul.Data;
namespace Weebul.Core.Model.Tests
{
    [TestFixture]
    public class FightTests
    {
        [Test]
        public void FightFromDataFightTest()
        {
            Data.Fight dataFight = Data.DataHelpers.Entities.Fights.Where(f => !f.BadResult).OrderByDescending(f => f.FightId).First();
            Fight coreFight = new Fight(dataFight);

            Assert.IsTrue(true);
        }

        [Test]

        public void TestAgainstWeblSim(Fighter fighter1, Fighter fighter2, FightPlan fightPlan1, FightPlan fightPlan2)
        {

            
        }
    }
}