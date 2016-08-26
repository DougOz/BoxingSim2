using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Weebul.Core.Helpers
{
    public static class RegexHelpers
    {

        public static double GroupAsDouble(this Match match, string key)
        {
            return double.Parse(match.Groups[key].Value);
        }

        public static string GetFirstLine(this string s)
        {
            if (String.IsNullOrEmpty(s)) return s; 
            if (s.IndexOf(Environment.NewLine) == -1) return s;
            if (s.IndexOf(Environment.NewLine) == 0) return String.Empty;
            return s.Substring(0, s.IndexOf(Environment.NewLine));            
        }

        public static string SkipLines(this string s, int numLines)
        {

            string ret = s;
            for (int i = 0; i < numLines; i++)
            {
                ret = ret.GetStringAfter(Environment.NewLine);
            }
            return ret; 
        }

        public static String GetStringAfter(this string s, string text)
        {
            return s.Substring(s.IndexOf(text) + text.Length);
        }


        public static string RemoveMatchAndLine(Match match, string orig)
        {
            if (orig.Length == match.Index + match.Length + 1)
            {
                return null;
            }
            string ret = orig.Substring(match.Index + match.Length);
            ret = RemoveNewLineFromStartOfString(ret);
            return ret;

        }

        public static string RemoveNewLineFromStartOfString(string s)
        {
            if (String.IsNullOrEmpty(s)) return s;

            if (!s.StartsWith(Environment.NewLine)) return s;

            if (s.Length == Environment.NewLine.Length) return null;
            return s.Substring(Environment.NewLine.Length);


        }
        public static string GetEnduranceDescriptorRegex(string fighterName)
        {

            string descriptorJoin = String.Join("|", RegexConstants.EnduranceDescriptors.Select(r => r.Key));
            string expression = string.Format
                ("^{0} (?<endurance>{1})\\.",
                Regex.Escape(fighterName),
                descriptorJoin);
            return expression;
        }

        public static MatchCollection GetTacticMatches(string fightPlan)
        {
            return Regex.Matches(fightPlan, RegexConstants.FightPlanTactics);
        }

    }
}
