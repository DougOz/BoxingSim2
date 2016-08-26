using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Weebul.Core.Model;
using Weebul.Data;
using Weebul.UI.Helpers;
using Weebul.UI.Model;

namespace Weebul.UI.ViewModel
{
    public class PivotSimViewModel : ViewModelBase
    {

        public PivotSimViewModel()
        {
            if(!IsInDesignModeStatic)
            {
                
                RefreshStuff();

                TrainingStats = GetTrainingItems();
                TrainingStats2 = GetTrainingItems();
                Debug.Print("Set stuff");
                SimItCommand = new RelayCommand(SimStuff);
                RefreshCommand = new RelayCommand(RefreshStuff);
                ExportCsvCommand = new RelayCommand(ExportCsv);
            }
            else
            {

            }
        }
        private void ExportCsv()
        {
            if (this.PivotSimulator == null) return;

            SaveFileDialog sfd = new SaveFileDialog();

            sfd.DefaultExt = ".csv";
            sfd.Filter = "Comma Separated Values (*.csv)|*.csv|All Files (*.*)|All Files";
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                this.PivotSimulator.ExportToCsv(sfd.FileName);
            }
            
        }
        private void RefreshStuff()
        {
            Shared.Entities.Fighters.Load();
            Shared.Entities.FightPlans.Load();
            
            Fighters = new ObservableCollection<CheckableDataItem>
                  (Shared.Entities.Fighters.Local.Select(f => new CheckableDataItem(f) ));
            FightPlans = new ObservableCollection<CheckableDataItem>
                (Shared.Entities.FightPlans.Local.Select(fp => new CheckableDataItem(fp)));
            Fighters2 = new ObservableCollection<CheckableDataItem>
           (Shared.Entities.Fighters.Local.Select(f => new CheckableDataItem(f)));
            FightPlans2 = new ObservableCollection<CheckableDataItem>
                (Shared.Entities.FightPlans.Local.Select(fp => new CheckableDataItem(fp)));
        }
        private ObservableCollection<CheckableDataItem> GetTrainingItems()
        {
            var ret = new ObservableCollection<CheckableDataItem>()
                {
                     new CheckableDataItem(TrainingStat.None),
                    new CheckableDataItem(TrainingStat.Agility),
                    new CheckableDataItem(TrainingStat.Chin),
                    new CheckableDataItem(TrainingStat.Conditioning),
                    new CheckableDataItem(TrainingStat.KOPunch),
                    new CheckableDataItem(TrainingStat.Speed),
                    new CheckableDataItem(TrainingStat.Strength)
                };
            return ret; 
        }
        private void SimStuff()
        {
            if (!this.Fighters.Any(f => f.IsChecked) || !this.FightPlans.Any(f => f.IsChecked) || 
                !this.Fighters2.Any(f => f.IsChecked) || !this.FightPlans2.Any(f => f.IsChecked) ||
                !this.TrainingStats.Any(f=>f.IsChecked) || !this.TrainingStats2.Any(f=>f.IsChecked))

            {
                System.Windows.MessageBox.Show("At least one item must be checked in each list");
                return;
            }
            List<PivotFighter> list1 = GetPivotFighterList(this.Fighters, this.FightPlans, this.TrainingStats);
            List<PivotFighter> list2 = GetPivotFighterList(this.Fighters2, this.FightPlans2, this.TrainingStats2);
            this.PivotSimulator = new PivotFightSimulator(list1, list2, this.NumberOfSims, Shared.Locator().Options.Options);
            this.PivotSimulator.FightAll();
            this.PivotSimulator.PrintAll();
            var dict = new Dictionary<PivotFighter, Dictionary<PivotFighter, PivotFightResultSet>>();            
            for (int i = 1; i <= this.PivotSimulator.Results.GetLength(0); i++)
            {
                dict.Add(this.PivotSimulator.PivotGroup1[i-1], this.PivotSimulator.GetRowDictionary(i - 1));
            }

            this.CrossTableData = dict; 

            var group2 = dict.Values.First().Keys;
            this.ColumnFighters = group2;
            this.ColumnTotals = this.PivotSimulator.GetColumnTotals();
            this.RowTotals = this.PivotSimulator.GetRowTotals();
            this.GridDataContext = Tuple.Create(dict, group2);

        }

        private List<PivotFighter> GetPivotFighterList(IEnumerable<CheckableDataItem> fighterList, IEnumerable<CheckableDataItem> planList, IEnumerable<CheckableDataItem> trainingList)
        {
            var ret = new List<PivotFighter>();
            List<Data.Fighter> fighters = fighterList.Where(f => f.IsChecked).Select(f => f.DataObject).OfType<Data.Fighter>().ToList();
            List<Data.FightPlan> fightPlans = planList.Where(f=>f.IsChecked).Select(f => f.DataObject).OfType<Data.FightPlan>().ToList();
            List<TrainingStat> trainings = trainingList.Where(f => f.IsChecked).Select(f => f.DataObject).OfType<TrainingStat>().ToList();
            bool includeFighterInName = fighterList.Count() > 1;
            bool includeFPInName = planList.Count() > 1;
            bool includeStatInName = planList.Count() > 1; 
            foreach(Data.Fighter fighter in fighters)
            {
                FighterStats fStats = new FighterStats(fighter);
                Weebul.Core.Model.Fighter mFighter = new Core.Model.Fighter() { Stats = fStats };
                
                foreach (Data.FightPlan fp in fightPlans)
                {
                    Core.Model.FightPlan mfp = new Core.Model.FightPlan()
                    {
                        FightPlanText = fp.PlanData
                    };
                    foreach(TrainingStat stat in trainings)
                    {
                        string statName = Enum.GetName(typeof(TrainingStat), stat);
                        string name = String.Format("{0}/{1}/{2}", fighter.Name, fp.PlanName, statName);
                        PivotFighter pivotFighter = new PivotFighter(fStats, mfp, name, stat);
                        string shortName = (includeFighterInName) ? fighter.Name : null;
                        if(includeFPInName)
                        {
                            shortName = shortName == null ? fp.PlanName : shortName + "/" + fp.PlanName;                                                        
                        }
                        if(includeStatInName)
                        {

                            shortName = shortName == null ? statName : shortName + "/" + statName;
                        }
                        pivotFighter.ShortName = shortName; 
                        ret.Add(pivotFighter);
                    }

                }
            }
            return ret; 

        }
        /// 
        private ObservableCollection<CheckableDataItem> _fightPlans = null;

        /// <summary>
        /// Sets and gets the FightPlans property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<CheckableDataItem> FightPlans
        {
            get
            {
                return _fightPlans;
            }

            set
            {
                if (_fightPlans == value)
                {
                    return;
                }

                _fightPlans = value;
                OnPropertyChanged();
            }
        }
        /// 
        private ObservableCollection<CheckableDataItem> _fighters = null;

        /// <summary>
        /// Sets and gets the MyProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<CheckableDataItem> Fighters
        {
            get
            {
                return _fighters;
            }

            set
            {
                if (_fighters == value)
                {
                    return;
                }

                _fighters = value;
                OnPropertyChanged();
            }
        }

        /// 
        private ObservableCollection<CheckableDataItem> _fighters2 = null;

        /// <summary>
        /// Sets and gets the Fighters2 property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<CheckableDataItem> Fighters2
        {
            get
            {
                return _fighters2;
            }

            set
            {
                if (_fighters2 == value)
                {
                    return;
                }

                _fighters2 = value;
                OnPropertyChanged();
            }
        }

        /// 
        private ObservableCollection<CheckableDataItem> _fightPlans2 = null;

        /// <summary>
        /// Sets and gets the FightPlans2 property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<CheckableDataItem> FightPlans2
        {
            get
            {
                return _fightPlans2;
            }

            set
            {
                if (_fightPlans2 == value)
                {
                    return;
                }

                _fightPlans2 = value;
                OnPropertyChanged();
            }
        }


        /// 
        private ObservableCollection<CheckableDataItem> _trainingStats = null;

        /// <summary>
        /// Sets and gets the TrainingStats property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<CheckableDataItem> TrainingStats
        {
            get
            {
                return _trainingStats;
            }

            set
            {
                if (_trainingStats == value)
                {
                    return;
                }

                _trainingStats = value;
                OnPropertyChanged();
            }
        }
        /// 
        private ObservableCollection<CheckableDataItem> _trainingStats2 = null;

        /// <summary>
        /// Sets and gets the TrainingStats2 property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<CheckableDataItem> TrainingStats2
        {
            get
            {
                return _trainingStats2;
            }

            set
            {
                if (_trainingStats2 == value)
                {
                    return;
                }

                _trainingStats2 = value;
                OnPropertyChanged();
            }
        }

        /// 
        private int _numberOfSims = 500;

        /// <summary>
        /// Sets and gets the NumberOfSims property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int NumberOfSims
        {
            get
            {
                return _numberOfSims;
            }

            set
            {
                if (_numberOfSims == value)
                {
                    return;
                }

                _numberOfSims = value;
                OnPropertyChanged();
            }
        }

        /// 
        private object _gridDataContext = null;

        /// <summary>
        /// Sets and gets the GridDataContext property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public object GridDataContext
        {
            get
            {
                return _gridDataContext;
            }

            set
            {
                if (_gridDataContext == value)
                {
                    return;
                }

                _gridDataContext = value;
                OnPropertyChanged();
            }
        }

        /// 
        private Dictionary<PivotFighter,Dictionary<PivotFighter, PivotFightResultSet>> _crossTableData = null;

        /// <summary>
        /// Sets and gets the MyProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Dictionary<PivotFighter,Dictionary<PivotFighter, PivotFightResultSet>> CrossTableData
        {
            get
            {
                return _crossTableData;
            }

            set
            {
                if (_crossTableData == value)
                {
                    return;
                }

                _crossTableData = value;
                OnPropertyChanged();
            }
        }
        /// 
        private IEnumerable<PivotFighter> _columnFighters = null;

        /// <summary>
        /// Sets and gets the ColumnFighters property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IEnumerable<PivotFighter> ColumnFighters
        {
            get
            {
                return _columnFighters;
            }

            set
            {
                if (_columnFighters == value)
                {
                    return;
                }

                _columnFighters = value;
                OnPropertyChanged();
            }
        }
        /// 
        private IEnumerable<PivotFightResultSet> _rowTotals = null;

        /// <summary>
        /// Sets and gets the RowTotals property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IEnumerable<PivotFightResultSet> RowTotals
        {
            get
            {
                return _rowTotals;
            }

            set
            {
                if (_rowTotals == value)
                {
                    return;
                }

                _rowTotals = value;
                OnPropertyChanged();
            }
        }

        /// 
        private IEnumerable<PivotFightResultSet> _columnTotals = null;

        /// <summary>
        /// Sets and gets the columnTotals property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IEnumerable<PivotFightResultSet> ColumnTotals
        {
            get
            {
                return _columnTotals;
            }

            set
            {
                if (_columnTotals == value)
                {
                    return;
                }

                _columnTotals = value;
                OnPropertyChanged();
            }
        }
        public PivotFightSimulator PivotSimulator { get; set;  }
        public RelayCommand SimItCommand { get; private set; }
        public RelayCommand RefreshCommand { get; private set; }

        public RelayCommand ExportCsvCommand { get; private set; }

        
    }
}
