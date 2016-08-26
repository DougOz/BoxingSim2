using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Model;

namespace Weebul.Core.Helpers
{
    public static class RegexConstants
    {

        public const string Round = @"^ROUND.(?<round>\d{1,2}).$";
        public const string EnduranceStart = @"^(?<fighter>.+) has (?<endurance>\d{1,3}\.\d) endurance points remaining";

        public const string Tactics = @"^Your tactics: aggressiveness = (?<agg>\d+.\d+), power = (?<pow>\d+\.\d+), defense = (?<def>\d+\.\d+), resting (?<rest>\d+\.\d+)";

        public const string Head = @"You are trying to knock him out this round.";

        public const string Body = @"You are trying to wear him out with body blows.";

        public const string Cut = @"You are aiming at his injuries.";

        public const string FightPlanTactics = @"(?<agg>\d+)(bBhHcC)?\/(?<pow>\d+)!?\/(?<def>\d+)";
        public const string Strategy = @"{0}.+\((?<value>{1})\)";
        public const string InsideParentheses = @"\((?<value>.+)\)";

        public const string TKOCut = "^The Doctor won't let .* continue the fight because of (?<cut>.*) (?<winner>{0}|{1}) wins by a TKO";

        public const string DQ = "^(?<winner>{0}|{1}) wins by DQ";

        public const string KO = "^(?<winner>{0}|{1}) wins by a Knock Out";

        public const string Punches = @"landed \d+ of \d+ punches -- (?<power>\d+) power punch(es)?, (?<jabs>\d+) jabs?, (?<rights>\d+) rights?.";

        public const string Damage = @"Your fighter lost (?<edmg>\d+.\d+) points of endurance this round due to damage, " + 
@"and (?<fatigue>\d+\.\d+) points due to fatigue. He took (?<sdmg>\d+\.\d+) points of stun damage this round. " + 
@"He has accumulated (?<total>\d+) points of damage in the fight";

        public const string TieRound = @"^This round was a (?<score1>\d+)-(?<score2>\d+) tie";
        public const string WinRound = @"^(?<winner>{0}|{1}) won the round (?<score1>\d+)-(?<score2>\d+)";

        public const string Judge = @"^Judge .* as follows: .$";

        public const string JudgeTie = @"^Round (?<round>\d+): A (?<score1>\d+)-(?<score2>\d+) tie\..$";

        public const string JudgeWin = @"^Round (?<round>\d+): (?<winner>{0}|{1}) (?<score1>\d+)-(?<score2>\d+)";
        private static Dictionary<string, FightingStyle> _styleDict = new Dictionary<string, FightingStyle>()
        {
            {"using the ring", FightingStyle.Ring },
            {"ropes", FightingStyle.Ropes },
            {"all out", FightingStyle.AllOut },
            {"feinting", FightingStyle.Feint },
            {"counter-punching", FightingStyle.Counter },
            {"clinching", FightingStyle.Clinch },
            {"inside", FightingStyle.Inside },
            {"outside", FightingStyle.Outside }
        };

        public static Dictionary<string, FightingStyle> StyleDict
        {
            get
            {
                return _styleDict; 
            }
        }



        private static Dictionary<string, int> _enduranceDescriptors = new Dictionary<string, int>()
        {
{"doesn't need to rest",80},
{"remains standing while the trainer wipes him down",70},
{"grabs a water bottle and rests on his stool",60},
{"is obviously tired",50},
{"is sucking wind",40},
{"looks exhausted",30},
{"collapsing limply onto his stool",20},
{"requires medical attention from his trainer",10},
{"can't remember which corner is his",0}

        };
        public static Dictionary<string, int> EnduranceDescriptors
        {
            get
            {
                return _enduranceDescriptors;
            }

        }
        private static Dictionary<string, Cut> _cutParseDictionary = new Dictionary<string, Cut>()
        {
            { "He has swelling around his right eye", new Cut(CutType.SwellRight, CutSeverity.Low) },
            { "He has his right eye badly swollen", new Cut(CutType.SwellRight, CutSeverity.Medium) },
            { "He has his right eye nearly swollen shut", new Cut(CutType.SwellRight, CutSeverity.High) },
            { "He has his right eye swollen shut", new Cut(CutType.SwellRight, CutSeverity.Critical) },
            { "He has swelling around his left eye", new Cut(CutType.SwellLeft, CutSeverity.Low) },
            { "He has his left eye badly swollen", new Cut(CutType.SwellLeft, CutSeverity.Medium) },
            { "He has his left eye nearly swollen shut", new Cut(CutType.SwellLeft, CutSeverity.High) },
            { "He has his left eye swollen shut", new Cut(CutType.SwellLeft, CutSeverity.Critical) },
            { "He has bleeding over his right eye", new Cut(CutType.BleedAboveRight, CutSeverity.Low) },
            { "He has a cut over his right eye", new Cut(CutType.BleedAboveRight, CutSeverity.Medium) },
            { "He has a serious cut over his right eye", new Cut(CutType.BleedAboveRight, CutSeverity.High) },
            { "He has a gash over his right eye", new Cut(CutType.BleedAboveRight, CutSeverity.Critical) },
            { "He has bleeding over his left eye", new Cut(CutType.BleedAboveLeft, CutSeverity.Low) },
            { "He has a cut over his left eye", new Cut(CutType.BleedAboveLeft, CutSeverity.Medium) },
            { "He has a serious cut over his left eye", new Cut(CutType.BleedAboveLeft, CutSeverity.High) },
            { "He has a gash over his left eye", new Cut(CutType.BleedAboveLeft, CutSeverity.Critical) },
            { "He has bleeding below his right eye", new Cut(CutType.BleedBelowRight, CutSeverity.Low) },
            { "He has a cut below his right eye", new Cut(CutType.BleedBelowRight, CutSeverity.Medium) },
            { "He has a serious cut below his right eye", new Cut(CutType.BleedBelowRight, CutSeverity.High) },
            { "He has a gash below his right eye", new Cut(CutType.BleedBelowRight, CutSeverity.Critical) },
            { "He has bleeding below his left eye", new Cut(CutType.BleedBelowLeft, CutSeverity.Low) },
            { "He has a cut below his left eye", new Cut(CutType.BleedBelowLeft, CutSeverity.Medium) },
            { "He has a serious cut below his left eye", new Cut(CutType.BleedBelowLeft, CutSeverity.High) },
            { "He has a gash below his left eye", new Cut(CutType.BleedBelowLeft, CutSeverity.Critical) },
            { "He has a bloody nose", new Cut(CutType.InjuredNose, CutSeverity.Low) },
            { "He has a fractured nose", new Cut(CutType.InjuredNose, CutSeverity.Medium) },
            { "He has a broken nose", new Cut(CutType.InjuredNose, CutSeverity.Critical) },
            { "He has a bloody lip", new Cut(CutType.InjuredJaw, CutSeverity.Low) },
            { "He has a bloody mouth", new Cut(CutType.InjuredJaw, CutSeverity.Medium) },
            { "He has a broken tooth", new Cut(CutType.InjuredJaw, CutSeverity.High) },
            { "He has a broken jaw", new Cut(CutType.InjuredJaw, CutSeverity.Critical) }           
        };

        public static Dictionary<string, Cut> CutParseDictionary
        {
            get
            {
                return _cutParseDictionary;
            }
        }
    }
}
