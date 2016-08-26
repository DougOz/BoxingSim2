using NUnit.Framework;
using Weebul.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Data;
using System.Diagnostics;

namespace Weebul.Core.Model.Tests
{
    [TestFixture]
    public class PivotFightSimulatorTests
    {
        [Test]
        public void FightAllTest()
        {
            List<int> fpList = new List<int>()
            {
                8, 9, 14, 15, 16
            };
            var listOne = new List<PivotFighter>();

            var listTwo = new List<PivotFighter>();

            int fighter1Id = 5;
            int fighter2Id = 5;

            FightOptions options = new FightOptions()
            {
                JudgeLuck = 0.05,
                LuckAmount = 0.05
            };

            Data.Fighter f1 = DataHelpers.Entities.Fighters.First(f => f.FighterId == fighter1Id);
            FighterStats f1Stats = new FighterStats(f1);
            Data.Fighter f2 = DataHelpers.Entities.Fighters.First(f => f.FighterId == fighter2Id);
            FighterStats f2Stats = new FighterStats(f2);
            Fighter f1Fighter = new Fighter()
            {
                Stats = f1Stats
            };
            Fighter f2Fighter = new Fighter()
            {
                Stats = f2Stats
            };
            foreach (int i in fpList)
            {

                Data.FightPlan dfp = DataHelpers.Entities.FightPlans.First(fp => fp.PlanId == i);
                Model.FightPlan mFp = new FightPlan() { FightPlanText = dfp.PlanData };                
                string name = String.Format("{0}, {1}", f1.Name, dfp.PlanName);
                listOne.Add(new PivotFighter(f1Stats, mFp, name, TrainingStat.None));

                
                name  = String.Format("{0}, {1}", f2.Name, dfp.PlanName);
                listTwo.Add(new PivotFighter(f2Stats, mFp, name, TrainingStat.None));
            }

            PivotFightSimulator simmer = new PivotFightSimulator(listOne, listTwo, 100, options);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            simmer.FightAll();

            Debug.Print("Done in " + sw.ElapsedMilliseconds);

            simmer.PrintAll();


        }
    }
}