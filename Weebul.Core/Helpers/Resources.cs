using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Model;

namespace Weebul.Core.Helpers
{
    public static class Resources
    {

        public static double CutAggPercentMultiplier = 1; 
        public static bool FatigueBeforeCut = false;
        public static double RoundTieThreshold = 3;
        public static double Dirty_Warning_Percent = 0.5;

        public static double Clinch_Warning_Percent = 0.01;

        public static int Clinch_MaxDefense_NoWarning = 10;

        public static double Warning_NoDirty = 0.01;

        public static double DqPercent = 0.05;

        public static double DqExponent = 2;

        public static double Round_Energy_Points = 20;

        public static double Style_Multiplier = 1;

        public static int DefaultTotalRounds = 12;

        [ThreadStatic]
        private static double _cutPercentMultiplier = 1; 
        public static double CutPercentMultiplier
        {
            get
            {
                return _cutPercentMultiplier;
            }
            set
            {
                _cutPercentMultiplier = value; 
            }
        }

        public static double DefaultLuckStdDeviation = 0.05;

        private static Dictionary<CutType, CutGroup> _cutTypeGroupDictionary = new Dictionary<CutType, CutGroup>()
        {
            { CutType.BleedAboveLeft, CutGroup.BleedAbove },
            { CutType.BleedAboveRight, CutGroup.BleedAbove },
            {CutType.BleedBelowLeft, CutGroup.BleedBelow },
            {CutType.BleedBelowRight, CutGroup.BleedBelow },
            {CutType.InjuredJaw, CutGroup.Jaw },
            {CutType.InjuredNose, CutGroup.Nose },
            {CutType.SwellLeft, CutGroup.Swell },
            {CutType.SwellRight, CutGroup.Swell }
        };

        public static Dictionary<CutType, CutGroup> CutTypeGroupDictionary
        {
            get
            {
                return _cutTypeGroupDictionary;
            }
        }
        private static Dictionary<CutGroup, CutGroupPenalty> _cutGroupPenaltyDictionary = null;

        public static Dictionary<CutGroup, CutGroupPenalty> CutGroupPenaltyDictionary
        {
            get
            {
                if(_cutGroupPenaltyDictionary == null)
                {
                    _cutGroupPenaltyDictionary = CutBase.GetPenaltyDictionary();
                }
                return _cutGroupPenaltyDictionary; 
            }
            set
            {
                _cutGroupPenaltyDictionary = value; 
            }
        }
    }
}
