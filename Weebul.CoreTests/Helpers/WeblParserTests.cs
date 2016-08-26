
using NUnit.Framework;
using Weebul.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Model;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Weebul.Core.Helpers.Tests
{
    [TestFixture]
    public partial class WeblParserTests
    {
        [Test]
        public void ParseWeblRoundTest()
        {
            Round rr = WeblParser.ParseWeblRound(_roundText);

            Debug.Print("Hey");
            

        }
        [Test]
        public void TestKORound()
        {
            Round rr = WeblParser.ParseWeblRound(_koRound);

            Assert.AreEqual(FightResultType.Knockout, rr.ResultType);
        }

        [Test]
        public void TestTKORound()
        {
            Round rr = WeblParser.ParseWeblRound(_tkoRound);

            Assert.AreEqual(FightResultType.TKO, rr.ResultType);
        }

        [Test]
        public void TestDQRound()
        {
            Round rr = WeblParser.ParseWeblRound(_dqRound);

            Assert.AreEqual(FightResultType.DQ, rr.ResultType);
        }
        [Test]
        public void ParseFightTest()
        {
            Fight fpr = WeblParser.ParseFight(_fullThing);

            
            Assert.AreEqual(FightOutcome.Win, fpr.Result.Outcome);
            Assert.AreEqual(FightResultType.Knockout, fpr.Result.ResultType);
        }

        [Test]
        public void GetCutListTest()
        {
            string text = "Here's a line before it" + Environment.NewLine +
                "Test B 0001 remains standing while the trainer wipes him down. He has his left eye badly swollen. He has a fractured nose." +
                Environment.NewLine + "Here's a line after";
            CutList c = WeblParser.GetCutList("Test B 0001", text);
            Assert.AreEqual(2, c.Count);
            Assert.IsTrue(c.Count(d => d.Type == CutType.SwellLeft && d.Level == CutSeverity.Medium) == 1);
            Assert.IsTrue(c.Count(d => d.Type == CutType.InjuredNose && d.Level == CutSeverity.Medium) == 1);            

        }
        [Test]
        public void GetCutList_EmptyTest()
        {
            string text = "Test B 0001 remains standing while the trainer wipes him down.";
            CutList c = WeblParser.GetCutList("Test B 0001", text);
            Assert.AreEqual(0, c.Count);



        }


        [Test]
        public void GetFightingStyleTest()
        {
            string text = "SOme guy is being bad man (clinching).";

            FightingStyle expected = FightingStyle.Clinch;
            FightingStyle actual = WeblParser.GetFightingStyle(text,"SOme guy");
            Assert.AreEqual(expected, actual);

            text = _clinchRound;

            Round rr = WeblParser.ParseWeblRound(text);

            Assert.AreEqual(FightingStyle.Clinch, rr.Fighter1Round.Tactics.Style);
        }

        [Test]
        public void GetTacticsTest()
        {
            string text = "Your tactics: aggressiveness = 1.0, power = 1.0, defense = 8.0, resting 10.0";

            FighterTactics expected = new FighterTactics(1.0, 1.0, 8.0, 10.0);
            FighterTactics actual = WeblParser.GetTactics(text);
            Assert.AreEqual(expected, actual);
        }

        [Test]

        public void ParseDamageTest()
        {

            Round rr = new Round(1, new FighterRound(null), new FighterRound(null));

            string text = @"
Test B 0001:

Your fighter lost 14.1 points of endurance this round due to damage, and 4.0 points due to fatigue. He took 12.1 points of stun damage this round. He has accumulated 67 points of damage in the fight.

ZZTest B 0001:

Your fighter lost 11.4 points of endurance this round due to damage, and 4.0 points due to fatigue. He took 11.4 points of stun damage this round. He has accumulated 69 points of damage in the fight. ";


            WeblParser.ParseDamage(rr, text);
            Assert.AreEqual(14.1, rr.Fighter1Round.DamageReceived.EnduranceDamage);
            Assert.AreEqual(12.1, rr.Fighter1Round.DamageReceived.StunDamage);
            Assert.AreEqual(11.4, rr.Fighter2Round.DamageReceived.EnduranceDamage);
            Assert.AreEqual(11.4, rr.Fighter2Round.DamageReceived.StunDamage);
            Assert.AreEqual(14.1, rr.Fighter2Round.DamageDealt.EnduranceDamage);
            Assert.AreEqual(12.1, rr.Fighter2Round.DamageDealt.StunDamage);

            Assert.AreEqual(4.0, rr.Fighter1Round.FatigueEndRound);
            Assert.AreEqual(4.0, rr.Fighter2Round.FatigueEndRound);
        }

        [Test]
        public void TestJudgeRound()
        {
            List<JudgeRound> ret = WeblParser.ParseJudges(_judgeString, "Test B 0001", "ZZTest B 0001");
            List<JudgeRound> rev = WeblParser.ParseJudges(_judgeString, "ZZTest B 0001", "Test B 0001");
            Debug.Print("Got it");
        }


    }
}
