using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data = Weebul.Data;
namespace Weebul.Core.Model
{
    public class FighterStats : ICloneable , IEquatable<FighterStats>
    {
        public FighterStats(double height, double speed, double agility, double strength, double knockoutPunch, double conditioning, double chin, double cutResistance, double weight, int numberOfFights, int rating)
        {
            this.Height = height;
            this.Speed = speed;
            this.Agility = agility;
            this.Strength = strength;
            this.KnockoutPunch = knockoutPunch;
            this.Conditioning = conditioning;
            this.Chin = chin;
            this.CutResistance = cutResistance;
            this.Weight = weight;
            this.NumberOfFights = numberOfFights;
            this.Rating = rating;
        }
        public FighterStats(Data.Fighter fighter) : this(fighter.Height, fighter.Speed, fighter.Agility, fighter.Strength, fighter.KOPunch, fighter.Conditioning, fighter.Chin, fighter.CutResistance, fighter.Weight, fighter.Fights, fighter.Rating)
        {
            _dataFighterId = fighter.FighterId; 
        }
        private int _dataFighterId = 0;
        public int DataFighterId
        {
            get
            {
                return _dataFighterId;
            }
        }
        public double Height { get; set; }

        public double Speed { get; set; }

        public double Agility { get; set; }

        public double Strength { get; set; }

        public double KnockoutPunch { get; set; }

        public double Conditioning { get; set; }

        public double Chin { get; set; }

        public double CutResistance { get; set; }
        
        public double StunDefense
        {
            get
            {
                double ret = this.Chin + (this.Agility / this.FatiguePercent) - 10;
                ret *= FatiguePercent;
                if (ret < 1) ret = 1;
                return ret; 
            }
        }
        
        public int NumberOfFights { get; set; }
        public double Weight { get; set;  }    
        public int Rating { get; set; }


        private double _fatiguePercent = 1; 
        public double FatiguePercent
        {
            get
            {
                return _fatiguePercent; 
            }
            set
            {
                _fatiguePercent = value; 
            }
        }
        public double MinWeight
        {
            get
            {
                return GetMinWeight(this.Weight, this.Conditioning);
            }
        }

        public static double GetMinWeight(double weight, double conditioning)
        {
            return weight - 0.25 * (conditioning + 2);
        }
        public object Clone()
        {
            return new FighterStats(this.Height, Speed, Agility, Strength, KnockoutPunch, Conditioning, Chin, CutResistance, Weight, NumberOfFights, Rating)
            {
                FatiguePercent = this.FatiguePercent
            };
        }

        public FighterStats Copy()
        {
            return Clone() as FighterStats;
        }
        
        public bool Equals(FighterStats other)
        {
            bool ret = this.Height == other.Height && this.Speed == other.Speed && this.Agility == other.Agility && 
                this.Strength == other.Strength &&this.KnockoutPunch == other.KnockoutPunch && 
                this.Conditioning == other.Conditioning && this.Chin == other.Chin && this.CutResistance == other.CutResistance
                && this.Weight == other.Weight && this.NumberOfFights == other.NumberOfFights 
                && this.Rating == other.Rating && this.FatiguePercent == other.FatiguePercent;

            return ret; 
        }

        protected void Fatigue(double endurancePoints)
        {
            double percent = endurancePoints / (this.Conditioning * 10);
            this.FatiguePercent = percent; 
            this.Strength *= percent;
            this.Agility *= percent;
            this.Speed *= percent;
            if (this.Strength < 1) this.Strength = 1;            
            if (this.Agility <= 1) this.Agility = 1;
            if (this.Speed <= 1) this.Speed = 1; 
        }

        public static FighterStats FatigueStats(FighterStats orig, double endurancePoints)
        {
            FighterStats ret = orig.Copy();
            ret.Fatigue(endurancePoints);
            return ret; 
        }

        public void AdjustCutStats(IEnumerable<CutBase> cuts)
        {
            if (cuts == null) return; 
            foreach (CutBase cut in cuts)
            {
                AdjustCut(cut);
            }
        //    CutPenalty add = CutList.GetAdditionalSwellPenalty(cuts);
           // AdjustCutPenalty(add);
        }


        public FighterStats AdjustAll(double endurancePoints, IEnumerable<Cut> cuts, bool fatigueBeforeCut)
        {
            FighterStats ret = this.Copy();
            if (fatigueBeforeCut)
            {
                ret = FatigueStats(ret, endurancePoints);
                ret.AdjustCutStats(cuts);                
            }
            else
            {
                ret.AdjustCutStats(cuts);
                ret = FatigueStats(ret, endurancePoints);
            }
            return ret; 
        }
        private void AdjustCut(CutBase cut)
        {
            CutPenalty cp = cut.GetPenalty();

            AdjustCutPenalty(cp);            
        }

        private void AdjustCutPenalty(CutPenalty cp)
        {

            this.Agility -= cp.AgilityPenalty;
            this.Speed -= cp.SpeedPenalty;
            if (this.Agility <= 1) this.Agility = 1;
            if (this.Speed <= 1) this.Speed = 1;
        }


        public double GetWeight(WeightClass weightClass)
        {
            return (int) weightClass > this.Weight ? this.Weight : (double) ((int) weightClass);
        }

        public WeightClass GetWeightClass()
        {
            return GetWeightClass(this.MinWeight);
        }

        public static WeightClass GetWeightClass(double weight, double conditioning)
        {
            double minWeight = GetMinWeight(weight, conditioning);
            return GetWeightClass(minWeight);
        }
        public static WeightClass GetWeightClass(double minWeight)
        {

            WeightClass ret = WeightClass.Straw;
            foreach (var value in Enum.GetValues(typeof(WeightClass)).OfType<WeightClass>().OrderBy(r => (int) r))
            {
                if (minWeight <= (int) value)
                {
                    ret = value;
                    break;
                }
                
            }
            return ret;
        }

    }
}
