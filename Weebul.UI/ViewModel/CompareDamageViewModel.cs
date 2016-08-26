using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Helpers;
using Weebul.Core.Model;
using Weebul.UI.Helpers;

namespace Weebul.UI.ViewModel
{
    public class CompareDamageViewModel : ViewModelBase
    {


        public CompareDamageViewModel()
        {
            this.CompareCurrentCommand = new RelayCommand(CompareDamage);
            if(!IsInDesignModeStatic)
            {
                Shared.Entities.Fights.Load();
            }
            this.CutPenalties = Resources.CutGroupPenaltyDictionary.Select(s => s.Value).ToList();
            this.SetDefaultCommand = new RelayCommand(SetDefaultCut);
        }
        private void CompareDamage()
        {
            Fight f = Shared.Locator().FightResults.Fight;
            Resources.FatigueBeforeCut = this.FatigueBeforeCut; 
            if(f != null)
            {
                this.CompareResults = f.CompareToCalculated();
                RoundDamage sum = RoundDamage.Sum(this.CompareResults.Select(c => c.Diff));
                this.DiffDescription = String.Format("Total diff: E: {0:0.00}, S: {1:0.00}", sum.EnduranceDamage, sum.StunDamage);
            }
        }
        
        private void SetDefaultCut()
        {
            Resources.CutGroupPenaltyDictionary = CutBase.GetPenaltyDictionary();
            this.CutPenalties = Resources.CutGroupPenaltyDictionary.Select(s => s.Value).ToList();
        }
        /// 
        private List<FighterRoundCompare> _compareResults = null;

        /// <summary>
        /// Sets and gets the CompareResults property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<FighterRoundCompare> CompareResults
        {
            get
            {
                return _compareResults;
            }

            set
            {
                if (_compareResults == value)
                {
                    return;
                }

                _compareResults = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
            /// The <see cref="Fights" /> property's name.
            /// </summary>
        public const string FightsPropertyName = "Fights";


        /// <summary>
        /// Gets the Fights property.        
        /// </summary>
        public IEnumerable<Data.Fight> Fights
        {
            get
            {
                return Shared.Entities.Fights.Local; 
            }
        }

        /// 
        private List<CutGroupPenalty> _cutPenalties = null;

        /// <summary>
        /// Sets and gets the CutPenalties property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<CutGroupPenalty> CutPenalties
        {
            get
            {
                return _cutPenalties;
            }

            set
            {
                if (_cutPenalties == value)
                {
                    return;
                }

                _cutPenalties = value;
                OnPropertyChanged();
            }
        }
        /// 
        private string _diffDescription = null;

        /// <summary>
        /// Sets and gets the DiffDescription property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string DiffDescription
        {
            get
            {
                return _diffDescription;
            }

            set
            {
                if (_diffDescription == value)
                {
                    return;
                }

                _diffDescription = value;
                OnPropertyChanged();
            }
        }
        /// 
        private bool _fatigueBeforeCut = false;

        /// <summary>
        /// Sets and gets the FatigueBeforeCut property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool FatigueBeforeCut
        {
            get
            {
                return _fatigueBeforeCut;
            }

            set
            {
                if (_fatigueBeforeCut == value)
                {
                    return;
                }

                _fatigueBeforeCut = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand CompareCurrentCommand { get; private set; }
        public RelayCommand LoadFightCommand { get; private set; }
        public RelayCommand SetDefaultCommand { get; private set; }
    }
}
