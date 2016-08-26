using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Model;
using Weebul.UI.Helpers;

namespace Weebul.UI.ViewModel
{
    public class OptionsViewModel : ViewModelBase
    {
        public OptionsViewModel()
        {
            this.PlayCommand = new RelayCommand(Play);
            this.PlayMultipleCommand = new RelayCommand(PlayMultiple);
        }
        public void Play()
        {
            Shared.Locator().Main.Play();
        }

        public void PlayMultiple()
        {
            FightTracker tracker = Shared.GetFightTracker();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<Fight> resList = FightTracker.PlayMultiple(NumberOfSims, tracker);
            StringBuilder sb = new StringBuilder();
            Shared.Locator().FightPlanF1.SetLineHits(resList.SelectMany(r => r.LineHits_Fighter1));
            Shared.Locator().FightPlanF2.SetLineHits(resList.SelectMany(r => r.LineHits_Fighter2));
            int wins = resList.Count(r => r.Result.Outcome == FightOutcome.Win);
            int draws = resList.Count(r => r.Result.Outcome == FightOutcome.Draw);
            int losses = resList.Count(r => r.Result.Outcome == FightOutcome.Loss);

            double wPCt = (wins + 0.5 * draws) / (wins + draws + losses);

            sb.AppendFormat("{0}-{1}-{2}, {3:0.0%}", wins, losses, draws, wPCt);
            sb.AppendLine();

            foreach(var vk in resList.GroupBy(r=>new {r.Result.Outcome, r.Result.ResultType}).OrderBy(r=>r.Key.Outcome))
            {
                sb.AppendFormat("{0} - {1}, {2}", vk.Key.Outcome, vk.Key.ResultType, vk.Count());
                sb.AppendLine();
            }
            //var v = resList.Select(r => r.RoundResults.Last().Fighter1Round.Cuts.Union(r.RoundResults.Last().Fighter2Round.Cuts)).SelectMany(c => c).ToList();
            //foreach (var group in v.GroupBy(gp=> new { gp.Type, gp.Level}).OrderBy(gp=>gp.Key.Type).ThenBy(gp=>gp.Key.Level))
            //{
            //    sb.AppendFormat("{0} - {1}, {2}", group.Key.Type, group.Key.Level, group.Count());
            //    sb.AppendLine();
            //}

            //v = resList.Select(r => r.RoundResults.First().Fighter1Round.Cuts.Union(r.RoundResults.First().Fighter2Round.Cuts)).SelectMany(c => c).ToList();
            //foreach (var group in v.GroupBy(gp => new { gp.Type, gp.Level }).OrderBy(gp => gp.Key.Type).ThenBy(gp => gp.Key.Level))
            //{
            //    sb.AppendFormat("{0} - {1}, {2}", group.Key.Type, group.Key.Level, group.Count());
            //    sb.AppendLine();
            //}
            //v = resList.Select(r => r.RoundResults.First(f=>f.RoundNumber == 2).Fighter1Round.Cuts.Union(r.RoundResults.First().Fighter2Round.Cuts)).SelectMany(c => c).ToList();
            //foreach (var group in v.GroupBy(gp => new { gp.Type, gp.Level }).OrderBy(gp => gp.Key.Type).ThenBy(gp => gp.Key.Level))
            //{
            //    sb.AppendFormat("{0} - {1}, {2}", group.Key.Type, group.Key.Level, group.Count());
            //    sb.AppendLine();
            //}

            //int CutLevel = resList.Select(r => r.RoundResults.Last().Fighter1Round.CutLevel + r.RoundResults.Last().Fighter2Round.CutLevel).Sum();
            //double cAverage = (double) CutLevel / (NumberOfSims * 2);
            //sb.AppendFormat("Cut level avg: {0:0.00}", cAverage);
            Shared.Locator().Main.OutputText = sb.ToString();
            Console.WriteLine(string.Format("Done in {0} ms", sw.ElapsedMilliseconds));

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

        /// 
        private int _numberOfSims = 1000;

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

        public RelayCommand PlayCommand { get; private set; }
        public RelayCommand PlayMultipleCommand { get; private set; }

    }
}
