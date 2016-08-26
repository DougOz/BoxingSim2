using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Helpers;

namespace Weebul.Core.Model
{
    public class FighterTactics : ICloneable, IEquatable<FighterTactics>
    {
        
        public FighterTactics(double aggressiveness, double power, double defense)
        {
            this.Aggressiveness = aggressiveness;
            this.Power = power;
            this.Defense = defense;
            this.Rest = (Resources.Round_Energy_Points - this.Aggressiveness - this.Power - this.Defense);
            if(this.Rest < 0 )
            {
                throw new Exception("Invalid tactics");
            }
        }
        public FighterTactics(double aggressiveness, double power, double defense, double rest)
        {
            this.Aggressiveness = aggressiveness;
            this.Power = power;
            this.Defense = defense;
            this.Rest = rest;
        }
        public FighterTactics()
        {

        }
        public double Aggressiveness { get; set; }

        public double Power { get; set; }
        public double Defense { get; set; }


        public double Rest { get; set; }

        public virtual object Clone()
        {
            return new FighterTactics(this.Aggressiveness, this.Power, this.Defense, this.Rest);
        }
        public virtual FighterTactics Copy()
        {
            return Clone() as FighterTactics;
        }

        public bool Equals(FighterTactics other)
        {
            return other!= null && 
                this.Aggressiveness == other.Aggressiveness && this.Power == other.Power && this.Defense == other.Defense && this.Rest == other.Rest; 
        }
    }
}
