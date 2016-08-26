using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class RoundReportOld : ModelBase
    {
        private Round _roundResult = null;
        public RoundReportOld(Round result)
        {
            _roundResult = result;
            SetReport();
        }
        private void SetReport()
        {
            this.Round = _roundResult.RoundNumber;

            this.Tactics_F1 = _roundResult.Fighter1Round.Tactics.ToString();
            this.Tactics_F2 = _roundResult.Fighter2Round.Tactics.ToString();
      
         
            this.Score_F1 = _roundResult.Fighter1Score;
            this.Score_F2 = _roundResult.Fighter2Score;
            
            if (_roundResult.IsEndOfBout)
            {
                this.Result = String.Format("Win - F{0} by {1}", _roundResult.Fighter1Win ? "1" : "2", _roundResult.ResultType);
            }
            this.Score_Total_F1 = _roundResult.Fighter1TotalScore;
            this.Score_Total_F2 = _roundResult.Fighter2TotalScore;
            if (_roundResult.Fighter1Round.PunchStats != null)
            {
                this.Punches_F1 = _roundResult.Fighter1Round.PunchStats.ToShortString();
            }
            if (_roundResult.Fighter2Round.PunchStats != null)
            {
                this.Punches_F2 = _roundResult.Fighter2Round.PunchStats.ToShortString();
            }
            this.Stuns_F1 = _roundResult.Fighter1Round.StunsCaused;
            this.Stuns_F2 = _roundResult.Fighter2Round.StunsCaused;
            this.Endurance_Damage_F1 = _roundResult.Fighter2Round.DamageDealt.EnduranceDamage;
            this.Endurance_Damage_F2 = _roundResult.Fighter1Round.DamageDealt.EnduranceDamage;
            this.Stun_Damage_F1 = _roundResult.Fighter2Round.DamageDealt.StunDamage;
            this.Stun_Damage_F2 = _roundResult.Fighter1Round.DamageDealt.StunDamage;


            //this.Cuts_F1 = _roundResult.Fighter1Cuts.Sum(f => (int) f.Level);
            //this.Cuts_F2 = _roundResult.Fighter2Cuts.Sum(f => (int) f.Level);

            this.Cuts_F1 = _roundResult.Fighter1Round.Cuts.Sum(f => (int) f.Level);
            this.Cuts_F2 = _roundResult.Fighter2Round.Cuts.Sum(f => (int) f.Level);
            this.Endurance_Percent_End_F1 = _roundResult.Fighter1Round.EndEndurancePercent;
            this.Endurance_Percent_End_F2 = _roundResult.Fighter2Round.EndEndurancePercent;
        }

        public int Round { get; set; }

        public string Result { get; set; }

        [Display(Name = "Tactics 1")]
        public string Tactics_F1 { get; set; }

        [Display(Name = "Tactics 2")]
        public string Tactics_F2 { get; set; }

        [Display(Name = "Score 1")]
        public int Score_F1 { get; set; }
        [Display(Name = "Score 2")]
        public int Score_F2 { get; set; }

        [Display(Name = "Total 1", AutoGenerateField =false)]
        
        public int Score_Total_F1 { get; set; }
        [Display(Name = "Total 2", AutoGenerateField =false)]
        public int Score_Total_F2 { get; set; }

        [Display(Name = "Punches 1")]
        public string Punches_F1 { get; set; }
        [Display(Name = "Punches 2")]
        public string Punches_F2 { get; set; }


        [Display(Name = "Stuns 1")]
        public int Stuns_F1 { get; set; }
        [Display(Name = "Stuns 2")]
        public int Stuns_F2 { get; set; }

        [Display(Name = "E Dmg 1")]
        [DisplayFormat(DataFormatString =("0.00"))]
        public double Endurance_Damage_F1 { get; set; }
        [Display(Name = "E Dmg 2")]
        [DisplayFormat(DataFormatString = ("0.00"))]
        public double Endurance_Damage_F2 { get; set; }

        [Display(Name = "St Dmg 1")]
        [DisplayFormat(DataFormatString = ("0.00"))]
        public double Stun_Damage_F1 { get; set; }
        [Display(Name = "St Dmg 2")]
        [DisplayFormat(DataFormatString = ("0.00"))]
        public double Stun_Damage_F2 { get; set; }

        [Display(Name = "End Pct 1")]
        [DisplayFormat(DataFormatString = ("0.0%"))]
        public double Endurance_Percent_End_F1 { get; set; }
        [Display(Name = "End Pct 2")]
        [DisplayFormat(DataFormatString = ("0.0%"))]
        public double Endurance_Percent_End_F2 { get; set; }

        [Display(Name = "Cuts 1")]
        public int Cuts_F1 { get; set; }

        [Display(Name = "Cuts 2")]
        public int Cuts_F2 { get; set; }

    }
}
