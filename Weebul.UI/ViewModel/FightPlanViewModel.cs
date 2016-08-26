using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Data;
using GalaSoft.MvvmLight;
using System.Data.Entity;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using Weebul.UI.Helpers;
using Weebul.Scripting;

namespace Weebul.UI.ViewModel
{
    public class FightPlanViewModel : ViewModelBase
    {

        public FightPlanViewModel()
        {
            if(!IsInDesignModeStatic)
            {
                Helpers.Shared.Entities.FightPlans.Load();
                this.SelectedFightPlan = FightPlans.First(f => f.PlanName == "Cut and Rest");
            }
            this.SaveCommand = new RelayCommand(SaveFightPlan);
            ValidateCommand = new RelayCommand(ValidatePlan);
        }
         
        private void ValidatePlan()
        {
            WeblScript scripter = new WeblScript();
            string modPlan;
            if (!scripter.ValidatePlan(this.FightPlanText, out modPlan, "#!"))
            {
                MessageBox.Show("Plan invalid - invalid lines marked with #!");
                this.FightPlanText = modPlan; 
            }
        }
        private void SaveFightPlan()
        {
            if (string.IsNullOrWhiteSpace(FightPlanName))
            {
                MessageBox.Show("Please Enter a Name");
                return;
            }
            var fightPlan = Shared.Entities.FightPlans.FirstOrDefault(s => s.PlanName == FightPlanName.Trim());
            if (fightPlan != null)

                Telerik.Windows.Controls.RadWindow.Confirm("Are you sure you want to overwrite?", (s, ea) =>
                {
                    fightPlan.PlanData = FightPlanText;
                    Shared.Entities.SaveChanges();
                });
            else
            {
                FightPlan fp = Shared.Entities.FightPlans.Create();
                fp.PlanName = FightPlanName;
                fp.PlanData = FightPlanText;

                Shared.Entities.FightPlans.Add(fp);
                Shared.Entities.SaveChanges();
                Helpers.Shared.Entities.FightPlans.Load();
                RaisePropertyChanged(FightPlansPropertyName);
                this.SelectedFightPlan = fp; 
            }
        }

        
        /// <summary>
            /// The <see cref="FightPlans" /> property's name.
            /// </summary>
        public const string FightPlansPropertyName = "FightPlans";

        public void SetLineHits(IEnumerable<int> hits)
        {
            if (String.IsNullOrEmpty(this.FightPlanText)) return;
            this.LineHitList = hits.ToList();
            string[] lines = this.FightPlanText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            Dictionary<int, int> tempDict = hits.GroupBy(i => i).ToDictionary(i => i.Key, i => i.Count());
            var list = new List<Tuple<int, string>>();
            for(int i = 0; i<lines.Length; i++)
            {
                int hitCount = tempDict.ContainsKey(i + 1) ? tempDict[i + 1] : 0;
                var tuple = Tuple.Create(hitCount, lines[i]);
                list.Add(tuple);
            }
            this.LineHits = list;
        }


        /// <summary>
        /// Gets the FightPlans property.        
        /// </summary>
        public ObservableCollection<FightPlan> FightPlans
        {
            get
            {
                return Helpers.Shared.Entities.FightPlans.Local; 
            }
        }

        /// 
        private FightPlan _selectedFightPlan = null;

        /// <summary>
        /// Sets and gets the SelectedFightPlan property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public FightPlan SelectedFightPlan
        {
            get
            {
                return _selectedFightPlan;
            }

            set
            {
                if (_selectedFightPlan == value)
                {
                    return;
                }

                _selectedFightPlan = value;
                if(value != null)
                {
                    FightPlanText = value.PlanData;
                    FightPlanName = value.PlanName; 
                }
                OnPropertyChanged();
            }
        }
        /// 
        private string _fightPlanText = "6/6/8";

        /// <summary>
        /// Sets and gets the FightPlanText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FightPlanText
        {
            get
            {
                return _fightPlanText;
            }

            set
            {
                if (_fightPlanText == value)
                {
                    return;
                }

                _fightPlanText = value;
                OnPropertyChanged();
                SetLineHits(new List<int>());
            }
        }

        /// 
        private string _fightPlanName = null;

        /// <summary>
        /// Sets and gets the FightPlanName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FightPlanName
        {
            get
            {
                return _fightPlanName;
            }

            set
            {
                if (_fightPlanName == value)
                {
                    return;
                }

                _fightPlanName = value;
                OnPropertyChanged();
            }
        }

        /// 
        private bool _showLineHits = false;

        /// <summary>
        /// Sets and gets the ShowLineHits property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool ShowLineHits
        {
            get
            {
                return _showLineHits;
            }

            set
            {
                if (_showLineHits == value)
                {
                    return;
                }

                _showLineHits = value;
                OnPropertyChanged();
            }
        }
        /// 
        private List<Tuple<int, string>> _lineHits = null;

        /// <summary>
        /// Sets and gets the LineHits property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<Tuple<int, string>> LineHits
        {
            get
            {
                return _lineHits;
            }

            set
            {
                if (_lineHits == value)
                {
                    return;
                }

                _lineHits = value;
                OnPropertyChanged();
            }
        }

        public List<int> LineHitList { get; set; }
        public RelayCommand SaveCommand { get; private set;  }

        public RelayCommand ValidateCommand { get; private set;  }
    }
}
