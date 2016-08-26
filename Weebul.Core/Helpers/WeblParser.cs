using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Model;
using System.Text.RegularExpressions;
namespace Weebul.Core.Helpers
{
    public class WeblParser
    {

        public static Round ParseWeblRound(string roundText)
        {

            string f1Name;
            string f2Name;
            return ParseWeblRound(roundText, out f1Name, out f2Name);
        }

        public static Round ParseWeblRound(string roundText, out string f1Name, out string f2Name)
        {
     
   
            string[] lines = roundText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            Match match = Regex.Match(roundText, RegexConstants.Round, RegexOptions.Multiline);
            int round = int.Parse(match.Groups["round"].Value);
            Round ret = new Round() { RoundNumber = round };
            FighterRound r1 = new FighterRound(ret);
            FighterRound r2 = new FighterRound(ret);
            ret.Fighter1Round = r1;
            ret.Fighter2Round = r2;
            
            string text = RegexHelpers.RemoveMatchAndLine(match, roundText);

            GetEnduranceAndTactics(r1, ref text, out f1Name);
            GetEnduranceAndTactics(r2, ref text, out f2Name);
            if (f1Name == f2Name)
            {
                throw new Exception("Fighters need different names because I'm lazy");
            }
            r1.Tactics.Style = GetFightingStyle(text, f1Name);            
            r2.Tactics.Style = GetFightingStyle(text, f2Name);
            r1.Cuts = GetCutList(f1Name, text);
            r2.Cuts = GetCutList(f2Name, text);
            ParseDamage(ret, text);

            GetRoundEndResult(ret, f1Name, f2Name, text);
            return ret;
        }
        public static CutList GetCutList(string fighterName, string text)
        {
            string exp = RegexHelpers.GetEnduranceDescriptorRegex(fighterName);


            Match match = Regex.Match(text, exp,RegexOptions.Multiline);
            CutList ret = new CutList();
            string temp = text.Substring(match.Index);
            temp = temp.GetFirstLine();

            string[] splitted = temp.Split(new string[] { ". " }, StringSplitOptions.RemoveEmptyEntries);
            for(int i = 1; i<splitted.Length; i++)
            {
                string cutString = splitted[i];
                Cut c = Cut.Parse(cutString);
                ret.Add(c);
            }
            return ret; 
        }

        public static void ParseDamage(Round res, string text)
        {
            MatchCollection col = Regex.Matches(text, RegexConstants.Damage);
            res.Fighter1Round.DamageDealt = new RoundDamage();            
            RoundDamage rdF1 = new RoundDamage()
            {
                EnduranceDamage = col[0].GroupAsDouble("edmg"),
                StunDamage = col[0].GroupAsDouble("sdmg")
            };            
            res.Fighter1Round.DamageReceived = rdF1;
            res.Fighter2Round.DamageDealt = rdF1;

            RoundDamage rdF2 = new RoundDamage()
            {
                EnduranceDamage = col[1].GroupAsDouble("edmg"),
                StunDamage = col[1].GroupAsDouble("sdmg")
            };
            res.Fighter2Round.DamageReceived = rdF2;
            res.Fighter1Round.DamageDealt = rdF2;

            res.Fighter1Round.FatigueEndRound = col[0].GroupAsDouble("fatigue");
            res.Fighter2Round.FatigueEndRound = col[1].GroupAsDouble("fatigue");
            
        }
        private static void GetEnduranceAndTactics(FighterRound r1, ref string text, out string fighterName)
        {
            Match match = Regex.Match(text, RegexConstants.EnduranceStart, RegexOptions.Multiline);
            r1.StartEndurance = match.GroupAsDouble("endurance");
            fighterName = match.Groups["fighter"].Value;
            match = Regex.Match(text, RegexConstants.Tactics, RegexOptions.Multiline);
            r1.Tactics = GetTactics(match);

            text = RegexHelpers.RemoveMatchAndLine(match, text);
            r1.Tactics.TargetArea = GetTargetArea(text);
        }
        
        public static TargetArea GetTargetArea(string text)
        {
            string nextLine = text.GetFirstLine();
            TargetArea ret = TargetArea.Opportunistic;
            if (nextLine.Contains(RegexConstants.Head))
            {
                ret = TargetArea.Head;
            }
            else if (nextLine.Contains(RegexConstants.Body))
            {
                ret = TargetArea.Body;
            }
            else if (nextLine.Contains(RegexConstants.Cut))
            {
                ret = TargetArea.Cut;
            }
            return ret; 
        }
        public static FightingStyle GetFightingStyle(string text, string fighterName)
        {
            FightingStyle ret = FightingStyle.None;

            string styleJoin = String.Join("|",RegexConstants.StyleDict.Select(s => s.Key));
            string exp = String.Format(RegexConstants.Strategy, Regex.Escape(fighterName), styleJoin);
            Match match = Regex.Match(text, exp, RegexOptions.Multiline);
            if (match.Success)
            {
                ret = RegexConstants.StyleDict[match.Groups["value"].Value];
            }
            return ret; 

        }
        public static FighterRoundPlan GetTactics(Match match)
        {
            FighterRoundPlan ret = new FighterRoundPlan(match.GroupAsDouble("agg"), 
                match.GroupAsDouble("pow"), 
                match.GroupAsDouble("def"), 
                FightingStyle.None, 
                TargetArea.Opportunistic, false);
            ret.Rest = match.GroupAsDouble("rest");
            return ret; 
        }

        public static FighterRoundPlan GetTactics(string text)
        {
            Match match = Regex.Match(text, RegexConstants.Tactics, RegexOptions.Multiline);
            return GetTactics(match);
        }
        
        public static void GetRoundEndResult(Round res, string fighter1Name, string fighter2Name, string text)
        {
            string exp = String.Format(RegexConstants.TKOCut, Regex.Escape(fighter1Name), Regex.Escape(fighter2Name));
            Match m = Regex.Match(text, exp, RegexOptions.Multiline);
            if(m.Success)
            {
                res.TKOCut = m.Groups["cut"].Value;
                res.ResultType = FightResultType.TKO;
                res.IsEndOfBout = true;
                res.Fighter1Win = m.Groups["winner"].Value == fighter1Name;
                return; 
            }
            exp = String.Format(RegexConstants.KO, Regex.Escape(fighter1Name), Regex.Escape(fighter2Name));
            m = Regex.Match(text, exp, RegexOptions.Multiline);
            if(m.Success)
            {
                res.ResultType = FightResultType.Knockout;
                res.IsEndOfBout = true; 
                res.Fighter1Win = m.Groups["winner"].Value == fighter1Name;
                return;
            }

            exp = String.Format(RegexConstants.DQ, Regex.Escape(fighter1Name), Regex.Escape(fighter2Name));
            m = Regex.Match(text, exp, RegexOptions.Multiline);
            if (m.Success)
            {
                res.ResultType = FightResultType.DQ;
                res.IsEndOfBout = true;
                res.Fighter1Win = m.Groups["winner"].Value == fighter1Name;
                return;
            }
            res.ResultType = FightResultType.Decision;
            ParseDecision(res,fighter1Name, fighter2Name, text);
            ParsePunchStats(res, text);
        }

        public static void ParseDecision(Round res, string fighter1Name, string fighter2Name, string text)
        {
            Match m = Regex.Match(text, RegexConstants.TieRound, RegexOptions.Multiline);
            if(m.Success)
            {
                res.Fighter1Score = Convert.ToInt32(m.Groups["score1"].Value);
                res.Fighter2Score = Convert.ToInt32(m.Groups["score2"].Value);
                return; 
            }

            string exp = String.Format(RegexConstants.WinRound, Regex.Escape(fighter1Name), Regex.Escape(fighter2Name));
            m = Regex.Match(text, exp, RegexOptions.Multiline);
            if(m.Groups["winner"].Value == fighter1Name)
            {
                res.Fighter1Score = Convert.ToInt32(m.Groups["score1"].Value);
                res.Fighter2Score = Convert.ToInt32(m.Groups["score2"].Value);
            }
            else
            {
                res.Fighter1Score = Convert.ToInt32(m.Groups["score2"].Value);
                res.Fighter2Score = Convert.ToInt32(m.Groups["score1"].Value);
            }
        }

        public static PunchStats[] ParsePunchStats(Round round, string text)
        {
            PunchStats[] ret = new PunchStats[2];

            ret[0] = new PunchStats(round.Fighter1Round.Tactics);
            ret[1] = new PunchStats(round.Fighter2Round.Tactics);

            MatchCollection matches = Regex.Matches(text, RegexConstants.Punches, RegexOptions.Multiline);
            ret[0].JabsLanded = (int)matches[0].GroupAsDouble("jabs");
            ret[0].PowerPunchesLanded = (int) matches[0].GroupAsDouble("power");
            ret[0].RightsLanded = (int) matches[0].GroupAsDouble("rights");

            ret[1].JabsLanded = (int) matches[1].GroupAsDouble("jabs");
            ret[1].PowerPunchesLanded = (int) matches[1].GroupAsDouble("power");
            ret[1].RightsLanded = (int) matches[1].GroupAsDouble("rights");

            round.Fighter1Round.PunchStats = ret[0];
            round.Fighter2Round.PunchStats = ret[1];
            return ret; 

        }

        public static Fight ParseFight(string text)
        {
           
            List<Round> res = new List<Model.Round>();
            MatchCollection m = Regex.Matches(text, RegexConstants.Round,RegexOptions.Multiline);
            string f1Name = null;
            string f2Name = null;
            for(int i = 0; i< m.Count; i++)
            {
                string round = text.Substring(m[i].Index);
                if(i < (m.Count - 1))
                {
                    round = round.Substring(0, m[i + 1].Index - m[i].Index - 1);
                }
                Round rr = ParseWeblRound(round, out f1Name, out f2Name);
                
                res.Add(rr);
            }
            List<JudgeRound> judgeRounds = ParseJudges(text, f1Name, f2Name);
            foreach(var jr in judgeRounds.GroupBy(r=>r.RoundNumber))
            {
                Round round = res.First(r => r.RoundNumber == jr.Key);
                round.JudgeRounds = jr.OrderBy(j => j.JudgeNumber).ToList();
            }
            Fight ret = new Fight(res);
            return ret; 
        }

        public static List<JudgeRound> ParseJudges(string text, string fighter1Name, string fighter2Name)
        {
            List<JudgeRound> ret = new List<JudgeRound>();

            string[] split = Regex.Split(text, RegexConstants.Judge, RegexOptions.Multiline);
            for(int i = 1; i< split.Length; i++ )
            {

                MatchCollection ties = Regex.Matches(split[i], RegexConstants.JudgeTie, RegexOptions.Multiline);
                foreach(Match m in ties)
                {

                    ret.Add(GetJudgeRound(i, m));
                }
                string exp = String.Format(RegexConstants.JudgeWin, Regex.Escape(fighter1Name), Regex.Escape(fighter2Name), RegexOptions.Multiline);
                MatchCollection winRounds = Regex.Matches(split[i], exp, RegexOptions.Multiline);
                foreach(Match m in winRounds)
                {
                    JudgeRound j = (GetJudgeRound(i, m));
                    if(m.Groups["winner"].Value != fighter1Name)
                    {
                        int score1 = j.Score.Fighter1Score;
                        j.Score.Fighter1Score = j.Score.Fighter2Score;
                        j.Score.Fighter2Score = score1; 
                    }
                    ret.Add(j);
                }
            }
            ret = ret.OrderBy(r => r.JudgeNumber).ThenBy(r => r.RoundNumber).ToList();
            return ret; 
        }

        private static JudgeRound GetJudgeRound(int i, Match m)
        {
            int score1 = Convert.ToInt32(m.Groups["score1"].Value);
            int score2 = Convert.ToInt32(m.Groups["score2"].Value);
            int round = Convert.ToInt32(m.Groups["round"].Value);
            JudgeRound j = new JudgeRound(i, score1, score2, round);
            return j; 
        }
    }
}
