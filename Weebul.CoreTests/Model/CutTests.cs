
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
    public class CutTests
    {
        [Test]
        [TestCase(1, 100000, 2d / 3)]
        [TestCase(2, 100000, 2d / 9)]
        [TestCase(3, 100000, 2d / 27)]
        [TestCase(4, 100000, 1d / 27)]
        public void RandomCutTest(int level, int trials, double expected)
        {
            int numLevel = 0;
            for (int i = 0; i <= trials; i++)
            {
                Cut c = Cut.RandomCut();
                if ((int) c.Level == level)
                {
                    numLevel++;
                }
            }
            double percent = (double) numLevel / trials;

            Assert.AreEqual(expected, percent, 0.005);
        }

        [Test]
        public void IdDictionaryTest()
        {
            Cut c = new Cut(CutType.BleedBelowRight, CutSeverity.Medium);

            int id = CutBase.IdDictionary[c];

            Assert.IsTrue(id > 0);
        }
    }
}