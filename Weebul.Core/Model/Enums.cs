using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{

    public enum CutSeverity
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }

    public enum CutType
    {
        BleedAboveRight = 1,
        BleedAboveLeft = 2,
        BleedBelowRight = 3,
        BleedBelowLeft = 4,
        SwellRight = 5,
        SwellLeft = 6,
        InjuredNose = 7,
        InjuredJaw = 8

    }

    public enum CutGroup
    {
        BleedAbove,
        BleedBelow,
        Swell, 
        Nose, 
        Jaw 
    }
    public enum TargetArea
    {
        Opportunistic,
        Body,
        Head,
        Cut
    }

    public enum WeightClass
    {
        Straw = 106,
        JuniorFly = 109,
        Fly = 112,
        SuperFly = 115,
        Bantam = 118,
        SuperBantam = 122,
        Feather = 126,
        SuperFeather = 130,
        Light = 135,
        SuperLight = 141,
        Welter = 147,
        SuperWelter = 153,
        Middle = 160,
        SuperMiddle = 167,
        LightHeavy = 175,
        Cruiser = 200,
        Heavy = 999

    }

    public enum FightOutcome
    {
        Win,
        Draw,
        Loss
    }
    public enum FightResultType
    {
        Knockout,
        TKO,
        DQ,
        Decision
    }

    public enum TrainingStat
    {
        None,
        Agility,
        Chin, 
        Conditioning,
        KOPunch,
        Speed,
        Strength
    }
}
