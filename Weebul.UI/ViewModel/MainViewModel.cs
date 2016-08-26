using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using Weebul.Core.Model;
using Weebul.Core.Helpers;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Text;
using Weebul.UI.View;
using Weebul.UI.Helpers;
using Weebul.UI.Windows;

namespace Weebul.UI.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        private FighterStats _TestC0001 = new FighterStats(2, 11, 9, 23, 0, 14, 10, 1, 138, 0, 0);
        private FighterStats _testA0001 = new FighterStats(8, 11, 17, 9, 0, 14, 10, 1, 137, 0, 0);
        public MainViewModel()
        {

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            this.PlayCommand = new RelayCommand(Play);
            this.ClearCommand = new RelayCommand(() =>
            {
                this.OutputText = null;
            });

            this.SetOptionsCommand = new RelayCommand(SetOptions);
        }

        private void SetOptions()
        {
            FightOptions optCopy = this.Options.Copy();
            if (PropertiesWindow.EditProperties(optCopy))
            {
                this.Options = optCopy;
            }
        }
  
        public void Play()
        {


            FightTracker tracker = Shared.GetFightTracker();
            Fight fight = null;
         //   try
       //     {
                fight = tracker.PlayFight();
                                
         //   }
        //    catch (Exception ex)
         //   {
                //MessageBox.Show("Error - " + ex.Message);
                //return;
        //    }
            StringBuilder sb = new StringBuilder();
            Shared.Locator().FightResults.SetResults(fight);
            sb.Append(tracker.ToString());
            sb.AppendLine();
            sb.AppendLine();
            foreach (Round res in fight.RoundResults)
            {
                sb.Append(res.PrintString());
                sb.AppendLine();
            }

            if (fight.Result.Outcome == FightOutcome.Draw)
            {
                sb.Append("The fight is a draw!");
            }
            else if (fight.Result.Outcome == FightOutcome.Loss)
            {
                sb.Append("Fighter 2 wins");
            }
            else
            {
                sb.Append("Fighter 1 wins");
            }
            sb.AppendLine();
            sb.AppendFormat("Total Score: Fighter 1 {0}, Fighter 2 {1}", fight.Fighter1Score, fight.Fighter2Score);
            sb.AppendLine();
            this.OutputText = sb.ToString();


        }
        /// 
   
        private string _outputText = null;

        /// <summary>
        /// Sets and gets the OutputText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string OutputText
        {
            get
            {
                return _outputText;
            }

            set
            {
                if (_outputText == value)
                {
                    return;
                }

                _outputText = value;
                OnPropertyChanged();
            }
        }
        /// 
        private bool _noLuck = false;

        /// <summary>
        /// Sets and gets the No Luck property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool NoLuck
        {
            get
            {
                return _noLuck;
            }

            set
            {
                if (_noLuck == value)
                {
                    return;
                }

                _noLuck = value;
                OnPropertyChanged();
            }
        }

        /// 
        private FightOptions _options = new FightOptions();

        /// <summary>
        /// Sets and gets the Options property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public FightOptions Options
        {
            get
            {
                return _options;
            }

            set
            {
                if (_options == value)
                {
                    return;
                }

                _options = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand PlayCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public RelayCommand<string> SetPlayerACommand { get; private set; }
        public RelayCommand<string> SetPlayerCCommand { get; private set; }
        public RelayCommand<string> LoadFighterCommand { get; private set; }
        public RelayCommand SetOptionsCommand { get; private set; }

    }
}