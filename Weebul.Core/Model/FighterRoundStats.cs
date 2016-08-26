using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Helpers;

namespace Weebul.Core.Model
{
    public class FighterRoundStats
    {

        public FighterRoundStats(FighterStats stats, FighterRoundPlan plan)
        {
            this.OriginalStats = stats;
            this.AdjustedStats = stats.Copy();
            this.Plan = plan;
            this.AdjustedTactics = plan.Copy();
            this.LuckFactor = 1; 
           
        }
        public FighterStats OriginalStats { get; set; }

        public FighterRoundPlan Plan { get; set; }

        public FighterStats AdjustedStats { get; set; }

        public FighterTactics AdjustedTactics { get; set; }
                
        private double _damageAdjustment = 1;

        public double DamageAdjustment
        {
            get
            {
                return _damageAdjustment;
            }
            set
            {
                _damageAdjustment = value;
            }
        }
        public double LuckFactor { get; set; }

        public double AdditionalFatigue { get; set; }
        public double AdditionalEnduranceDamage { get; set; }
        public double CutPercent { get; set; }

        public double CutAggravatePercent { get; set; }

        public double PunchLandPercent { get; set; }

        public double JabLandPercent { get; set; }

        public bool PunchLuckAdjusted { get; set; }
        #region fighting styles


        public void AdjustStyle(FightingStyle style, FighterRoundStats other)
        {

            switch (style)
            {
                case FightingStyle.AllOut:
                    AdjustAllout(other);
                    break;
                case FightingStyle.Clinch:
                    AdjustClinch(other);
                    break;
                case FightingStyle.Counter:
                    AdjustCounter(other);

                    break;
                case FightingStyle.Feint:
                    AdjustFeint(other);
                    break;
                case FightingStyle.Inside:
                    AdjustInside(other);
                    break;
                case FightingStyle.Outside:
                    AdjustOutside(other);
                    break;
                case FightingStyle.Ring:
                    AdjustRing(other);
                    break;
                case FightingStyle.Ropes:
                    AdjustRopes(other);
                    break;

            }
        }

        public void AdjustAllout(FighterRoundStats other)
        {

            this.DamageAdjustment *= (1 + 1 * Resources.Style_Multiplier);
            other.DamageAdjustment *= (1 + 3 * Resources.Style_Multiplier);
        }

        public void AdjustClinch(FighterRoundStats other)
        {
            this.AdjustedStats.Agility += 1 * Resources.Style_Multiplier;
            double diff = this.OriginalStats.Strength - other.OriginalStats.Strength;
            if (diff > 0)
            {
                this.AdjustedStats.Agility += 0.5 * diff * Resources.Style_Multiplier;
            }
            double aggAdjust = other.Plan.Aggressiveness * 0.15 * Resources.Style_Multiplier;

            this.AdjustedTactics.Aggressiveness -= aggAdjust;
            this.AdjustedTactics.Rest += aggAdjust;
        }

        public void AdjustCounter(FighterRoundStats other)
        {
            this.AdjustedStats.Strength += 1 * Resources.Style_Multiplier;
            this.AdjustedTactics.Aggressiveness -= this.AdjustedTactics.Aggressiveness * 0.15 * Resources.Style_Multiplier;
            double diff = this.OriginalStats.Height + this.OriginalStats.Speed - other.OriginalStats.Height - other.OriginalStats.Speed;

            double adjust = diff * (1d / 3) * Resources.Style_Multiplier;
            if (adjust < -1 * this.OriginalStats.Agility * 0.5)
            {
                adjust = -1 * this.OriginalStats.Agility * 0.5;
            }
            this.AdjustedStats.Agility += adjust;


            adjust = -1 * diff * (1d / 3) * Resources.Style_Multiplier;
            if (adjust < -1 * other.OriginalStats.Agility * 0.5)
            {
                adjust = -1 * other.OriginalStats.Agility * 0.5;
            }
            other.AdjustedStats.Agility += adjust;
        }

        public void AdjustFeint(FighterRoundStats other)
        {
            this.AdjustedStats.Speed += 1 * Resources.Style_Multiplier;

            double fatigue = this.AdjustedTactics.Aggressiveness + 0.5 * this.AdjustedTactics.Power;

            if (fatigue >= this.AdjustedStats.Conditioning / 2)
            {
                this.AdditionalFatigue += 1 * Resources.Style_Multiplier;
            }
            double diff = this.OriginalStats.Speed - other.OriginalStats.Speed;
            if (diff > 0)
            {
                this.AdjustedStats.Speed += diff * 0.5 * Resources.Style_Multiplier;
            }
        }

        public void AdjustInside(FighterRoundStats other)
        {
            this.AdjustedStats.Strength += 1 * Resources.Style_Multiplier;
            double diff = this.OriginalStats.Strength - other.OriginalStats.Strength;
            if (diff > 0)
            {
                this.AdjustedStats.Strength += diff * 0.5 * Resources.Style_Multiplier;
            }
            other.DamageAdjustment *= (1 + 0.1 * Resources.Style_Multiplier);
        }
        public void AdjustOutside(FighterRoundStats other)
        {
            this.AdjustedStats.Agility += 0.5;
            this.AdjustedStats.Speed += 0.5;
            double diff = this.OriginalStats.Height - other.OriginalStats.Height;
            if (diff > 0)
            {
                this.AdjustedStats.Height += 0.5 * diff * Resources.Style_Multiplier;
            }
            this.AdjustedTactics.Power -= this.AdjustedTactics.Power * 0.15 * Resources.Style_Multiplier;
        }

        public void AdjustRing(FighterRoundStats other)
        {

            this.AdjustedStats.Agility += 1 * Resources.Style_Multiplier;
            double diff = this.OriginalStats.Agility - other.OriginalStats.Agility;

            if (diff > 0)
            {
                this.AdjustedStats.Agility += diff * 0.5 * Resources.Style_Multiplier;
            }
            this.AdditionalFatigue += 1 * Resources.Style_Multiplier;
        }

        public void AdjustRopes(FighterRoundStats other)
        {
            this.AdjustedStats.Agility -= 1 * Resources.Style_Multiplier;
            double diff = this.OriginalStats.Agility - other.OriginalStats.Agility;
            double adjust = diff * (2d / 3);
            if (other.OriginalStats.Agility - adjust <= 8)
            {
                adjust = other.OriginalStats.Agility - 8;
                if (adjust < 0) adjust = 0;
            }
            other.AdjustedStats.Agility -= adjust;
        }

        #endregion

        public void AdjustHeight(FighterRoundStats other)
        {
            double diff = this.AdjustedStats.Height - other.AdjustedStats.Height;
            if (diff > 0)
            {
                this.AdjustedStats.Speed += diff / 2;
                this.AdjustedStats.Agility += diff / 2;
                this.AdjustedStats.Height -= diff;
            }
            else if (diff < 0)
            {
                diff *= -1;
                other.AdjustedStats.Speed += diff / 2;
                other.AdjustedStats.Agility += diff / 2;
                other.AdjustedStats.Height -= diff;
            }

        }
        public void AdjustEndurance(double endurancePoints)
        {
            this.AdjustedStats = FighterStats.FatigueStats(this.AdjustedStats, endurancePoints);                        
        }
        public void AdjustCuts(IEnumerable<Cut> cuts)
        {

            if(cuts.Any(f=>f.Type == CutType.InjuredNose))
            {
                this.AdditionalFatigue += 1; 
            }
            this.AdjustedStats.AdjustCutStats(cuts);
            return; 
            foreach(Cut cut in cuts)
            {
                if(cut.Type == CutType.BleedAboveLeft || cut.Type == CutType.BleedAboveRight)
               // if(cut.Type == Cut.CutType.BleedBelowLeft || cut.Type == Cut.CutType.BleedBelowRight)
                {
                    this.AdjustedStats.Speed -= 0.5 * (int) cut.Level;
                    if(cut.Level == CutSeverity.High)
                    {
                        this.AdjustedStats.Agility -= 0.5;
                    }
                    else if (cut.Level == CutSeverity.Critical)
                    {
                        this.AdjustedStats.Agility -= 1; 
                    }
                }
                else if (cut.IsBleeding )
                {
                    double penalty = ((int) cut.Level - 1) * 0.5;
                    this.AdjustedStats.Speed -= penalty;
                    this.AdjustedStats.Agility -= penalty;
                }
                else if (cut.IsSwelling && (int)cut.Level >= 2)
                {
                    double penalty = ((int) cut.Level - 1) * 0.5;
                    this.AdjustedStats.Agility -= penalty;
                    this.AdjustedStats.Speed -= penalty; 
                }
                else if (cut.Type == CutType.InjuredNose)
                {
                    this.AdditionalFatigue += 1;
                 //   this.AdditionalEnduranceDamage += (int) (cut.Level) - 1;
                }
                
            }
        }
        
        public void SetCutPercent(FighterRoundStats other)
        {
            this.CutPercent = 2 - (this.AdjustedStats.CutResistance * 0.5) + this.OriginalStats.NumberOfFights * 0.005; 
            if(other.Plan.TargetArea == TargetArea.Body)
            {
                this.CutPercent *= 0.25;
            }
            else if (other.Plan.TargetArea == TargetArea.Head || other.Plan.TargetArea == TargetArea.Cut)
            {
                this.CutPercent *= 1.5;
            }
            this.CutPercent = CutPercent;
            this.CutAggravatePercent = CutPercent * Resources.CutAggPercentMultiplier;            
            if(other.Plan.TargetArea == TargetArea.Cut)
            {
                this.CutAggravatePercent *= 1.5;
            }

        }
        public void SetLuckFactor(double stdDeviation)
        {
            this.LuckFactor = RandomGen.GetRandomNormal(1, stdDeviation);
        }
        
        public void AdjustDirty(bool isWarned)
        {
            if(this.Plan.Dirty)
            {
                this.DamageAdjustment *= (isWarned) ? 1.05 : 1.1; 
            }
        }

        public void SetPunchAccuracy(FighterRoundStats other)
        {
            int rating = this.AdjustedStats.Rating + other.AdjustedStats.Rating;
            this.PunchLandPercent = PunchStats.GetPercentageNoLuck(this.AdjustedStats.Speed, rating, other.AdjustedStats.Agility, false);
            this.JabLandPercent = PunchStats.GetPercentageNoLuck(this.AdjustedStats.Speed, rating, other.AdjustedStats.Agility, true);
            double mult = 15 / (this.AdjustedTactics.Defense + other.AdjustedTactics.Defense);
            if(this.Plan.TargetArea == TargetArea.Body || this.Plan.TargetArea == TargetArea.Head)
            {
                mult *= 0.8;
            }
            else if (this.Plan.TargetArea == TargetArea.Cut)
            {
                mult *= 0.9;
            }
            this.PunchLandPercent *= mult;
            this.JabLandPercent *= mult;
            this.PunchLuckAdjusted = false; 
        }

      
        public void AdjustPunchLuck()
        {
            if (PunchLuckAdjusted) return;

            this.PunchLandPercent = PunchStats.GetPercentageLuck(this.PunchLandPercent, this.LuckFactor);
            this.JabLandPercent = PunchStats.GetPercentageLuck(this.JabLandPercent, this.LuckFactor);
            this.PunchLuckAdjusted = true; 
        }
        public void FixAdjustments()
        {
            if (AdjustedTactics.Defense < 1)
            {
                AdjustedTactics.Defense = 1;
            }
            if (AdjustedTactics.Power < 1)
            {
                AdjustedTactics.Power = 1;
            }
            if (AdjustedTactics.Aggressiveness < 1)
            {
                AdjustedTactics.Aggressiveness = 1;
            }
            if (AdjustedStats.Agility < 1)
            {
                AdjustedStats.Agility = 1;
            }
            if (AdjustedStats.Chin < 1)
            {
                AdjustedStats.Chin = 1;
            }
            if (AdjustedStats.Speed < 1)
            {
                AdjustedStats.Speed = 1;

            }
            if (AdjustedStats.Strength < 1)
            {
                AdjustedStats.Strength = 1;
            }

        }
        

    }
}
