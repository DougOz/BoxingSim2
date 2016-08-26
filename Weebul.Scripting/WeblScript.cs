using IronPython.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System.Diagnostics;
using Weebul.Util;

namespace Weebul.Scripting
{
    public class WeblScript
    {
        public const string CHEAT = "CHEAT@@@";
        private ScriptEngine _engine = Python.CreateEngine();
        private ScriptScope _scope = null;
        private string _fightPlan;
        const string PAD = "     ";
        public WeblScript()
        {
            _scope = _engine.CreateScope();
        }


        #region parsing

        public static string ParseFightPlan(string orig)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("def getFightPlan() : ");
            builder.AppendLine();
            builder.AppendLine();
            builder.Append(PAD + "if round >= 0 :");
            builder.AppendLine();
            builder.Append(PAD + PAD + "fightTactic='1/1/18|0'");
            builder.AppendLine();
            builder.Append(PAD + PAD + "cheat=false");
            builder.AppendLine();
            int curRound = 1;
            int lineNumber = 0; 
            foreach (string line in orig.ToLower().Split(new string[] { Environment.NewLine, ";" }, StringSplitOptions.None).Select(f => f.Trim()))
            {
                lineNumber++; 
                if (!line.StartsWith("#") && !String.IsNullOrWhiteSpace(line))
                {
                    curRound = ProcessLine(builder, curRound, line.Trim(), lineNumber);
                }
            }
            builder.AppendLine();
            builder.AppendLine(PAD + "if cheat == true :");
            builder.AppendLine(String.Format(PAD + PAD + "fightTactic = fightTactic + '{0}'", CHEAT));
            builder.Append(PAD + "return fightTactic");
            return builder.ToString();
        }

        private static int ProcessLine(StringBuilder builder, int curRound, string line, int lineNumber)
        {
            int index = line.IndexOf(')');

            if (index == 1 || index == 2)
            {
                int round = StringFunctions.LeftVal(line);
                curRound = round;
                builder.Append(PAD + "if round >= " + curRound + " :");
                builder.AppendLine();
                builder.AppendLine(PAD + PAD + "dummyvar = 3");
                if (line.Length > index + 1 && !String.IsNullOrWhiteSpace(line.Substring(index + 1)))
                {
                    string subs = line.Substring(index + 1).Trim();
                    AppendLine(subs, builder, lineNumber);
                }
            }
            else
            {
                AppendLine(line, builder, lineNumber);
            }

            return curRound;
        }
        private static void AppendLine(string line, StringBuilder builder, int lineNumber)
        {

            string pad = PAD + PAD;

            if(line.ToLower().EndsWith("towel"))
            {
                builder.Append(PAD + PAD + "if cantowel == true :");
                builder.AppendLine();
                pad = pad + PAD;
            }
            if (line.StartsWith("if"))
            {
                string temp = ReplaceIf(line, lineNumber);
                builder.AppendFormat(pad + temp);
                builder.AppendLine();
            }
            else
            {
                builder.AppendFormat(pad + "fightTactic = '{0}|{1}'", line, lineNumber);
                //builder.AppendFormat(PAD + PAD + line);
                builder.AppendLine();
            }
        }
        private static string ReplaceIf(string line, int lineNumber)
        {

            string temp = line.Trim();
            temp = temp.Replace("<=", "|@@@|");
            temp = temp.Replace(">=", "^þ^");
            temp = temp.Replace("=", "==");
            temp = temp.Replace("|@@@|", "<=");
            temp = temp.Replace("^þ^", ">=");
            temp = temp.Replace(" is ", " == ");
            if (line.Trim().ToLower().EndsWith("cheat"))
            {
                temp = temp.Replace(" then ", " : ");
                temp = temp.Replace("cheat", "cheat = true");
            }
            else
            {

                temp = string.Format("{0}|{1}'", temp.Replace(" then ", " : fightTactic ='"), lineNumber);
            }
            return temp;
        }

        #endregion


        public void SetScopeVariables(ScriptVariables variables)
        {
            _scope.SetVariable("round", variables.Round);
            _scope.SetVariable("strong", ScriptVariables.Strong);
            _scope.SetVariable("tired", ScriptVariables.Tired);
            _scope.SetVariable("weak", ScriptVariables.Weak);
            _scope.SetVariable("exhausted", ScriptVariables.Weak);
            _scope.SetVariable("opponent", variables.Opponent);
            _scope.SetVariable("roundswon", variables.RoundsWon);
            _scope.SetVariable("roundslost", variables.RoundsLost);
            _scope.SetVariable("mycuts", variables.MyCuts);
            _scope.SetVariable("hiscuts", variables.HisCuts);
            _scope.SetVariable("mystuns", variables.MyStuns);
            _scope.SetVariable("hisstuns", variables.HisStuns);
            _scope.SetVariable("myknockdowns", variables.MyKnockdowns);
            _scope.SetVariable("hisknockdowns", variables.HisKnockdowns);
            _scope.SetVariable("hurt", ScriptVariables.Weak);
            _scope.SetVariable("endurance", variables.Endurance);
            _scope.SetVariable("end", variables.Endurance);
            _scope.SetVariable("endurance_percent", variables.EndurancePercent);
            _scope.SetVariable("decision_won", variables.DecisionWon);
            _scope.SetVariable("decision_lost", variables.DecisionLost);
            _scope.SetVariable("warnings", variables.Warnings);
            _scope.SetVariable("score", variables.Score);
            _scope.SetVariable("opp", variables.Opponent);
            _scope.SetVariable("cantowel", variables.CanTowel);
            _scope.SetVariable("cheat", false);
        }


        public ParseResult ParseAndEvaluate(string fightPlan, ScriptVariables variables)
        {

            SetScopeVariables(variables);
            if (_fightPlan != fightPlan)
            {
                _fightPlan = fightPlan;
                string script = ParseFightPlan(fightPlan);
                InitializeScript(script);
          

            }
            return EvaluateFunc();
        }

        private void InitializeScript(string script)
        {            
            ScriptSource ss = _engine.CreateScriptSourceFromString(script);
            _scope.SetVariable("true", true);
            _scope.SetVariable("false", false);
            ss.Execute(_scope);
        }

        private ParseResult EvaluateFunc()
        {
            string func = "getFightPlan()";
            ScriptSource ss = _engine.CreateScriptSourceFromString(func);
            string text =  ss.Execute(_scope);
            bool cheat = false;
            if(text.EndsWith(CHEAT))
            {
                cheat = true;
                text = text.Replace(CHEAT, "");
            }
            string[] arr = text.Split('|');

            return new Scripting.ParseResult()
            {
                Text = arr[0],
                Cheat = cheat,
                LineNumber = int.Parse(arr[1])                
            };
        }
    

        public bool ValidateLine(string line, out string errMessage)
        {
            return ValidateLine(line, new ScriptVariables(), out errMessage);
        }
        public bool ValidateLine(string line, ScriptVariables variables, out string errMessage)
        {
            List<ScriptVariables> varList = new List<Scripting.ScriptVariables>() { variables };
            return ValidateLine(line, varList, out errMessage);
        }

        public bool ValidateLine(string line, IEnumerable<ScriptVariables> variableList, out string errMessage)
        {
            errMessage = null;
            try
            {
                string script = ParseFightPlan(line);
                foreach (ScriptVariables variables in variableList)
                {
                    SetScopeVariables(variables);
                    InitializeScript(script);
                    EvaluateFunc();
                }
            }
            catch (Exception ex)
            {
                errMessage = ex.ToString();
                return false;
            }
            return true;
        }

        public bool ValidatePlan(string plan, IEnumerable<ScriptVariables> variableList, out string modPlan, string errPrefix)
        {
            bool ret = true;
            string[] lines = plan.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            StringBuilder sb = new StringBuilder();

            foreach (String line in lines)
            {
                string newLine = line;
                string err;
                if (!ValidateLine(line, variableList, out err))
                {
                    ret = false;
                    newLine = errPrefix + line;
                }
                sb.AppendLine(newLine);
            }
            modPlan = sb.ToString();
            return ret;
        }
        public bool ValidatePlan(string plan, out string modPlan, string errPrefix = "#!")
        {
            IEnumerable<ScriptVariables> varList = new List<ScriptVariables>() { new Scripting.ScriptVariables() };
            return ValidatePlan(plan, varList, out modPlan, errPrefix);
        }

        public bool ValidatePlan(string plan)
        {
            string modPlan;
            return ValidatePlan(plan, out modPlan);
        }
    }
}
