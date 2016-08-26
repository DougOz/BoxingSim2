using Microsoft.VisualStudio.TestTools.UnitTesting;
using Weebul.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Weebul.Scripting.Tests
{
    [TestClass()]
    public class WeblScriptTests
    {
        [TestMethod()]
        public void ParseFightPlanTest()
        {
            string thing = @"#Tire him by working the body
 4B/8/8 (clinch)
#Move and jab to keep the score close
4) if score < 1 then  7/5/8 (ring)
#If you`ve won enough rounds, then just protect yourself
if decision_won = true then  4H/8/8 (clinch)

8)  5/10/5 (allout)
if score > 8 then  3/6/11";
            string actual = WeblScript.ParseFightPlan(thing);
            string expected = @"
if round >= 1 : fightTactic = '4B/8/8 (clinch)'
if round >= 4 and score < 1 : fightTactic = '7/5/8 (ring)'
if round >= 4 and decision_won == true : fight=' 4H/8/8 (clinch)'

if round >=8 : fightTactic = ' 5/10/5 (allout)'
if round >= 8 and score > 8 : fightTactic = ' 3/6/11'";

            Debug.Print(actual);

        }

        [TestMethod()]
        public void ParseAndEvaluateTest()
        {
            ScriptVariables var = new ScriptVariables()
            {
                Round = 5
            };
            string script = @"1)4/6/10
4)4/8/8";
            WeblScript wScript = new WeblScript();
            string expected = "4/8/8";
            string actual = wScript.ParseAndEvaluate(script, var).Text;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCheat()
        {
            ScriptVariables var = new ScriptVariables()
            {
                Round = 5
            };
            string script = @"1)4/6/10
4)4/8/8
if warnings < 1 then cheat
if warnings < 1 then 5/7/8";
            
            
            WeblScript wScript = new WeblScript();
            string expected = "4/8/8";

            ParseResult ret = wScript.ParseAndEvaluate(script, var);
            Assert.IsTrue(ret.Cheat);

            Assert.AreEqual("5/7/8", ret.Text);

           
        }
    }
}