using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Data;

namespace Weebul.Core.Model
{
    public class FighterRoundPlan : FighterTactics , IEquatable<FighterRoundPlan>
    {
        public FighterRoundPlan(double aggressiveness, double power, double defense) : this(aggressiveness, power, defense, FightingStyle.None, TargetArea.Opportunistic, false)
        {

        }
        public FighterRoundPlan(double aggressiveness, double power, double defense, FightingStyle style, TargetArea targetArea, bool dirty) : base(aggressiveness, power, defense)
        {            
            this.Style = style;
            this.TargetArea = targetArea;
            this.Dirty = dirty;
        }
        public FighterRoundPlan()
        {

        }
        public FighterRoundPlan(Data.Tactic dataTactic) : this(dataTactic.Aggressiveness, dataTactic.Power, dataTactic.Defense, (FightingStyle)dataTactic.Strategy, (TargetArea)dataTactic.Target, dataTactic.Dirty)
        {
        }

        public FighterRoundPlan(FighterTactics tactics, FighterRoundPlan roundPlan) : this(tactics.Aggressiveness, tactics.Power, tactics.Defense, roundPlan.Style, roundPlan.TargetArea, roundPlan.Dirty )
        {
            this.Rest = roundPlan.Rest; 
        }
        public static FighterRoundPlan Adjusted(FighterRoundPlan parent, FighterTactics tactics)
        {
            FighterRoundPlan ret = new Model.FighterRoundPlan(tactics.Aggressiveness, tactics.Power, tactics.Defense, parent.Style, parent.TargetArea, parent.Dirty);
            return ret; 
        }
        public FightingStyle Style { get; set; }

        public TargetArea TargetArea { get; set; }

        public static FighterRoundPlan Parse(string text)
        {
            string text1 = text.ToUpper().Trim().TrimEnd(';');
            string[] arr = text1.Split('/');
            if (arr.Length == 1)
            {
                if(text.Trim().ToUpper() == "TOWEL")
                {
                    return new FighterRoundPlan
                    {
                        IsTowel = true
                    };
                }
            }
        
            FightingStyle style = FightingStyle.None;
            TargetArea target = TargetArea.Opportunistic;
            int defense = 0; 
            if(arr[2].EndsWith(")"))
            {
                string[] arrStyle = arr[2].Split('(');
                string sString = arrStyle[1].TrimEnd(')');
                defense = int.Parse(arrStyle[0].TrimEnd()); 
                style = (FightingStyle) Enum.Parse(typeof(FightingStyle), sString,true);
            }
            else
            {
                string def = arr[2].TrimEnd();
                def = def.TrimEnd(';');
                defense = int.Parse(def);
            }
            int aggressiveNess = int.Parse(arr[0].TrimEnd('C', 'H', 'B'));
            int power = int.Parse(arr[1].TrimEnd('!'));
            bool dirty = arr[1].EndsWith("!");
            if(arr[0].EndsWith("C"))
            {
                target = TargetArea.Cut; 
            }
            else if (arr[0].EndsWith("H"))
            {
                target = TargetArea.Head;
            }
            else if (arr[0].EndsWith("B"))
            {
                target = TargetArea.Body; 
            }
            return new FighterRoundPlan(aggressiveNess, power, defense, style, target, dirty);
               
        }

        public bool IsTowel { get; set; }
        public bool Dirty { get; set; }
        public string StyleString()
        {
            return this.Style == FightingStyle.None ? "" :
                String.Format("({0})", Enum.GetName(typeof(FightingStyle), this.Style));
        }
        public int HitLineNumber { get; set; }
        public override string ToString()
        {

            if (IsTowel) return "Towel";
            string sTarget = this.TargetArea == TargetArea.Body ? "B" :
                this.TargetArea == TargetArea.Head ? "H" :
                this.TargetArea == TargetArea.Cut ? "C" : "";
            string ret = string.Format("{0}{1}/{2}{3}/{4} {5}", this.Aggressiveness, sTarget, this.Power, Dirty ? "!" : "", this.Defense, this.StyleString()).Trim();

            return ret; 
        }

        public bool Equals(FighterRoundPlan other)
        {
            return other != null && this.Aggressiveness == other.Aggressiveness && this.Defense == other.Defense && this.Dirty == other.Dirty &&
                this.Power == other.Power && this.Rest == other.Rest && this.Style == other.Style && this.TargetArea == other.TargetArea;
        }

        public Data.Tactic GetDataTactic()
        {
            Tactic ret = DataHelpers.Entities.Tactics.FirstOrDefault(t => t.Aggressiveness == this.Aggressiveness 
            && t.Defense == this.Defense 
            && t.Power == this.Power 
            && t.Rest == this.Rest 
            && t.Strategy == (int) this.Style 
            && t.Target == (int) this.TargetArea 
            && t.Dirty == this.Dirty);
            if (ret == null)
            {
                ret = DataHelpers.Entities.Tactics.Create();
                ret.Aggressiveness = this.Aggressiveness;
                ret.Defense = this.Defense;
                ret.Dirty = this.Dirty;
                ret.Power = this.Power;
                ret.Rest = this.Rest;
                ret.Strategy = (int) this.Style;
                ret.Target = (int) this.TargetArea;
                ret = DataHelpers.Entities.Tactics.Add(ret);
                DataHelpers.Entities.SaveChanges();
            }
            return ret; 
        }
        
        
    }
}
