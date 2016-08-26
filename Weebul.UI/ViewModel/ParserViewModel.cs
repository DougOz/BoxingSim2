using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Weebul.Core.Helpers;
using Weebul.Core.Model;
using Weebul.UI.Helpers;

namespace Weebul.UI.ViewModel
{
    public class ParserViewModel : ViewModelBase
    {
        private Fight _parseResult;
        private string _dbSaved = null; 
        public ParserViewModel()
        {
            ParseCommand = new RelayCommand(Parse);
            SaveToDbCommand = new RelayCommand(ParseAndSave);
        }


        public void ParseAndSave()
        {
            if(ParseIt())
            {
                SaveToDb();
            }
        }
        public void SaveToDb()
        {

            if(_dbSaved != null && ParseText.Equals(_dbSaved))
                {
                MessageBox.Show("Parse text appears to be the same");
                return; 
            }

            
            int fId1 = Shared.Locator().Fighter1.SelectedFighter.FighterId;
            int fId2 = Shared.Locator().Fighter2.SelectedFighter.FighterId;
            int pId1 = Shared.Locator().FightPlanF1.SelectedFightPlan.PlanId;
            int pId2 = Shared.Locator().FightPlanF2.SelectedFightPlan.PlanId;
            FightOptions options = Shared.Locator().Options.Options;
            _parseResult.SaveToDatabase(options, fId1, fId2, pId1, pId2, this.ParseText);
            _dbSaved = ParseText;
        }

        public void Parse()
        {
            ParseIt();
        }
        public bool  ParseIt()
        {
            try
            {
                _parseResult = WeblParser.ParseFight(this.ParseText);
                _parseResult.Fighter1 = new FighterStats(Shared.Locator().Fighter1.SelectedFighter);
                _parseResult.Fighter2 = new FighterStats(Shared.Locator().Fighter2.SelectedFighter);
                foreach (Round res in _parseResult.RoundResults)
                {
                    res.Fighter1Round.SetPercentages(Shared.Locator().Fighter1.SelectedFighter.Conditioning * 10);
                    res.Fighter2Round.SetPercentages(Shared.Locator().Fighter2.SelectedFighter.Conditioning * 10);
                }
                List<RoundReportOld> roundReports = _parseResult.RoundResults.Select(f => new RoundReportOld(f)).ToList();
                Shared.Locator().FightResults.SetResults(_parseResult);
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false; 
            }
            return true;
        }

        /// 
        private string _parseText = null;

        /// <summary>
        /// Sets and gets the ParseText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ParseText
        {
            get
            {
                return _parseText;
            }

            set
            {
                if (_parseText == value)
                {
                    return;
                }

                _parseText = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand ParseCommand { get; private set; }
        public RelayCommand SaveToDbCommand { get; private set; }
    }
}
