using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Helpers;
using Weebul.Data;

namespace Weebul.Core.Model
{
    public class FighterRound
    {
        public FighterRound(Round parent)
        {
            this.DamageDealt = new RoundDamage();
            this.DamageReceived = new RoundDamage();
            this.Parent = parent;
        }

        public FighterRound(Data.FighterRound dataRound, Round parent)
        {
            this.Parent = parent; 
            this.DamageReceived = new RoundDamage()
            {
                BaseDamage = dataRound.Base_Damage,
                CutDamage = dataRound.Cut_Damage,
                EnduranceDamage = dataRound.Endurance_Damage,
                StunDamage = dataRound.Stun_Damage,
            };
            this.StartEndurance = dataRound.Endurance_Start;                        
            this.IsWarned = dataRound.IsWarned;
            this.FatigueEndRound = dataRound.Fatigue_End;
            this.FatigueStartRound = dataRound.Fatigue_Start;

            this.Tactics = new FighterRoundPlan(dataRound.Tactic);
            this.Cuts = new CutList(dataRound.Cuts.Select(c => new Cut(c)));
            this.PunchStats = new Model.PunchStats(this.Tactics)
            {
                JabsLanded = dataRound.JabsLanded,
                PowerPunchesLanded = dataRound.PowerPunchesLanded,
                RightsLanded = dataRound.RightsLanded
            };
            this.EndEndurance = this.StartEndurance - dataRound.Endurance_Damage - dataRound.Fatigue_End;                        
        }
        public bool IsWarned { get; set;  }
        public bool DeductPointWarning { get; set; }
        public bool IsDisqualified { get; set; }
        public bool IsTKOedByCut { get; set; }
        public bool IsTKOedByEndurance { get; set; } 
        public bool IsTowel { get; set; }   
        public RoundDamage DamageDealt { get; set; }
        public RoundDamage DamageReceived { get; set; }
        public double LuckFactor { get; set; }
        public FighterRoundPlan Tactics { get; set; }
        public double FatigueStartRound { get; set; }
        public double FatigueEndRound { get; set; }
        public double StartEndurancePercent { get; set; }
        public double EndEndurancePercent { get; set; }
        public double StartEndurance { get; set; }
        public double EndEndurance { get; set; }
        public double EndurancePostRecover { get; set; }
        public Round Parent { get; set; }
        public int CutLevel { get
            {
                if (Cuts == null) return 0;
                return Cuts.Sum(c => (int) c.Level);
            }
        }
        public bool IsFighter1
        {
            get
            {
                return Parent != null && Parent.Fighter1Round == this;
            }
        }
        public int Score { get
            {
                if (Parent == null) return 0;
                return (IsFighter1 ? Parent.Fighter1Score : Parent.Fighter2Score);
            }
        }
        public CutList Cuts { get; set; }

        public CutList Cuts_StartRound { get; set; }
        public int StunsCaused
        {
            get
            {
                if (DamageDealt.StunValue < 1.5) return 0;
                if (DamageDealt.StunValue < 2) return 1;
                if (DamageDealt.StunValue < 2.25) return 2;
                if (DamageDealt.StunValue < 2.4) return 3;
                if (DamageDealt.StunValue < 2.5) return 4;
                return 6;
            }
        }

        public PunchStats PunchStats { get; set; }
        public void SetDamageDealt(FighterRound other)
        {
            this.DamageReceived = other.DamageDealt;     
        }

        public void SetDamageReceived(FighterRound other)
        {
            this.DamageDealt = other.DamageReceived;
        }


        public void CheckFighterWarning(FighterFight fighter)
        {
            WarningResult res = CheckWarning(fighter);
            this.IsWarned = res.IsWarned;
            this.IsDisqualified = res.IsDisqualified; 
        }
        
        public static WarningResult CheckWarning(FighterFight fight)
        {
            WarningResult ret = CheckWarning(fight.RoundStats.Plan.Dirty, fight.Warnings, fight.RoundStats.Plan.Style, fight.RoundStats.Plan.Defense);
            return ret; 
        }

        public static WarningResult CheckWarning(bool dirty, int warnings, FightingStyle style, double defense)
        {
            WarningResult ret = new WarningResult();
            if (dirty)
            {
                bool warned = RandomGen.CheckRandom(Resources.Dirty_Warning_Percent);

                if (warned)
                {
                    ret.IsWarned = true;
                    ret.IsDisqualified = CheckDqed(warnings);
                }
            }
            else
            {
                ret.IsWarned = ret.IsWarned || RandomGen.CheckRandom(Resources.Warning_NoDirty);
            }
            if (style == FightingStyle.Clinch && defense > Resources.Clinch_MaxDefense_NoWarning)
            {
                double percent = defense - Resources.Clinch_MaxDefense_NoWarning;
                percent = Math.Pow(percent, 2) / 100;
                bool warned = RandomGen.CheckRandom(percent);
                if (warned)
                {
                    ret.IsWarned = true;
                    ret.IsDisqualified = ret.IsDisqualified || CheckDqed(warnings);
                }
            }
            return ret;
        }
        public static bool CheckDqed(int warnings)
        {
            double dQPercent = DqPercent(warnings);
            bool dQed = RandomGen.CheckRandom(dQPercent);
            return dQed; 
        }
        public static double DqPercent(int warnings)
        {
            double dQPercent = Resources.DqPercent * Math.Pow(Resources.DqExponent, warnings);
            return dQPercent; 
        }
        
       
        public void MultiplyDamageLuckFactor(double luckFactor)
        {
            this.DamageDealt.BaseDamage *= luckFactor;
            this.DamageDealt.EnduranceDamage *= luckFactor;
            this.DamageDealt.StunDamage *= luckFactor;
            this.DamageDealt.StunValue *= luckFactor; 
        }


        public void SetCuts(FighterFight fighter, FighterRound otherRound)
        {
            double cP1 = otherRound.DamageDealt.BaseDamage * otherRound.DamageDealt.BaseDamage / 1000;
            double cutPercent = fighter.RoundStats.CutPercent * cP1;
            cutPercent *= Resources.CutPercentMultiplier;
            if (cutPercent > 0.5) cutPercent = 0.5;
            double cutAggPercent = fighter.RoundStats.CutAggravatePercent * cP1;
            cutAggPercent *= Resources.CutPercentMultiplier;
            if (cutAggPercent > 0.5) cutAggPercent = 0.5;
            
            SetCuts(fighter.Cuts, cutPercent, cutAggPercent);
            otherRound.DamageDealt.EnduranceDamage += fighter.Cuts.DamageForRound();
            otherRound.DamageDealt.CutDamage = fighter.Cuts.DamageForRound();
        }
        private void SetCuts(CutList cutList, double cutPercent, double aggPercent)
        {
            cutList.ResetRound();
            foreach(Cut c in cutList)
            {
                if(RandomGen.CheckRandom(aggPercent))
                {
                    c.Aggravate(); 
                }
            }
            while(RandomGen.CheckRandom(cutPercent))
            {
                Cut cut = Cut.RandomCut();
                cutList.AddCut(cut);
            }
            if (cutList.Any(c => c.CausedTko) || cutList.CantSee()) 
            {
                this.IsTKOedByCut = true; 
            }
        }

        public void SetPunches(FighterRoundStats roundStats)
        {
            this.PunchStats = PunchStats.GetPunchStats(roundStats);
        }
        public double GetScore()
        {
            return this.PunchStats.GetScore(this.DamageDealt.StunDamage, this.DamageDealt.EnduranceDamage);
        }
        public Data.FighterRound CreateInDatabase(int roundId, bool isFighter1)
        {
            WeebulEntities entities = DataHelpers.Entities;
            Data.FighterRound dataRound = entities.FighterRounds.Create();         
            dataRound.RoundId = roundId;
            dataRound.IsFighter1 = isFighter1;
            CopyToDataRound(dataRound);
            Tactic t = this.Tactics.GetDataTactic();
            dataRound.TacticsId = t.TacticsId;
            Data.FighterRound saved = entities.FighterRounds.Add(dataRound);
            entities.SaveChanges();
            SaveCuts(saved.FRID);
            entities.SaveChanges();
            return saved; 
            
        }
        public void SaveCuts(int dataId)
        {
            if(this.Cuts != null && this.Cuts.Count > 0)
            {
                foreach(Cut cut in this.Cuts )
                {
                    FighterRoundCut frc = DataHelpers.Entities.FighterRoundCuts.Create();
                    frc.RoundDamage = cut.RoundDamage;
                    frc.FRID = dataId;
                    frc.CutID = cut.DataId;
                    DataHelpers.Entities.FighterRoundCuts.Add(frc);
                }
            }
        }
        public void CopyToDataRound(Data.FighterRound dataRound)
        {
            dataRound.Base_Damage = this.DamageReceived.BaseDamage;
            dataRound.Cut_Damage = this.DamageReceived.CutDamage;
            dataRound.Endurance_Damage = this.DamageReceived.EnduranceDamage;
            dataRound.Stun_Damage = this.DamageReceived.StunDamage;
            dataRound.Endurance_Start = this.StartEndurance;
            dataRound.IsWarned = this.IsWarned;
            dataRound.Fatigue_Start = this.FatigueStartRound;
            dataRound.Fatigue_End = this.FatigueEndRound;            
            if (this.PunchStats != null)
            {
                dataRound.JabsLanded = this.PunchStats.JabsLanded;
                dataRound.PowerPunchesLanded = this.PunchStats.PowerPunchesLanded;
                dataRound.RightsLanded = this.PunchStats.RightsLanded;
            }
            
        }
        public void SetPercentages(double totalEndurance)
        {
            this.StartEndurancePercent = this.StartEndurance / totalEndurance;
            this.EndEndurancePercent = this.EndEndurance / totalEndurance;
        }
        public class WarningResult
        {

            public bool IsWarned { get; set; }

            public bool IsDisqualified { get; set; }
        }


    }
}
