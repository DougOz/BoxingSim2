using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Model;
using Weebul.Util;

namespace Weebul.Core.Helpers
{
    public static class FightHelpers
    {

        public static RoundDamage CalculateDamageForDataRound(Data.Round round, FighterStats fStats, FighterStats oStats, List<CutBase> cuts, bool isFirst)
        {
            Data.FighterRound round1 = (isFirst) ? round.First : round.Second;
            Data.FighterRound round2 = (isFirst) ? round.Second : round.First;
            FighterStats adjusted1 = FighterStats.FatigueStats(fStats, round1.Endurance_Start);
            FighterStats adjusted2 = FighterStats.FatigueStats(oStats, round2.Endurance_Start);
            FighterRoundPlan fp1 = new FighterRoundPlan(round1.Tactic);
            FighterRoundPlan fp2 = new FighterRoundPlan(round2.Tactic);
            if (cuts != null)
            {
                adjusted1.AdjustCutStats(cuts);
            }
            RoundDamage ret = RoundDamage.CalculateRoundDamage(adjusted1, fp1, adjusted2, fp2, fp1.TargetArea);
            return ret; 

        }

        public static HashSet<CutBase> TestDamageForDataFight(Data.Fight fight, double threshold)
        {
            HashSet<CutBase> ret = new HashSet<Model.CutBase>();
            
            fight.SetCuts();
            FighterStats stats1 = new FighterStats(fight.Fighter1);
            FighterStats stats2 = new FighterStats(fight.Fighter2);
            foreach(Data.Round round in fight.Rounds)
            {
                if(RoundMatchesExpectations(round,stats1,stats2, true,threshold))
                {
                    foreach(var c in GetCutsForDataRound(round.First))
                    {
                        if (!ret.Contains(c)) ret.Add(c);
                    }
                    foreach(var c in GetCutsForDataRound(round.Second))
                    {
                        if (!ret.Contains(c)) ret.Add(c);
                    }
                }
            }
            return ret; 
            
        }

        
        public static bool RoundMatchesExpectations(Data.Round round, FighterStats f1Stats, FighterStats f2Stats, bool skipCuts, double threshold = 0)
        {
            List<CutBase> cuts1 = skipCuts ? new List<CutBase>() : round.First.GetCutsForDataRound();
            List<CutBase> cuts2 = skipCuts ? new List<CutBase>() : round.Second.GetCutsForDataRound();
            
            RoundDamage rd = CalculateDamageForDataRound(round, f1Stats, f2Stats, cuts1, true);
            if(!IsDamageEqual(round.First,rd,threshold))
            {
                return false; 
            }
            RoundDamage rd2 = CalculateDamageForDataRound(round, f2Stats, f1Stats, cuts2, false);
            if(!IsDamageEqual(round.Second, rd2,threshold))
            {
                return false; 
            }
            return true; 


        }
        public static bool IsDamageEqual(this Data.FighterRound dataFighterRound, RoundDamage damage, double threshold = 0)
        {
            double diff = Math.Abs(Math.Round(dataFighterRound.Stun_Damage,1) - Math.Round(damage.StunDamage,1)); 
            if(diff > threshold)
            {
                return false; 
            }
            return true; 
            //diff = Math.Abs(Math.Round(dataFighterRound.Endurance_Damage, 1) - Math.Round(damage.EnduranceDamage, 1));
            //if(diff > threshold)
            //{
            //    return false; 
            //}
            //return true; 
        }
        public static List<CutBase> GetCutsForDataRound(this Data.FighterRound dataFighterRound)
        {
            if(dataFighterRound.PreviousCuts == null)
            {
                return new List<Model.CutBase>();
            }
            return dataFighterRound.PreviousCuts.Select(c => new CutBase(c)).ToList();
            
        }
      
    }
}
