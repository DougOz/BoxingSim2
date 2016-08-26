
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;

using Weebul.Core.Helpers;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Text;
using Weebul.Data;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Weebul.UI.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class FighterViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the FighterViewModel class.
        /// </summary>        
        public FighterViewModel()
        {
            if (!IsInDesignModeStatic)
            {                
                Helpers.Shared.Entities.Fighters.Load();
                this.SelectedFighter = Fighters.First(f => f.Name == "Test B 0001");

              
            }
        }

        private void Fighters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Helpers.Shared.Entities.SaveChanges();
        }

        public const string FightersPropertyName = "Fighters";

        public ObservableCollection<Fighter> Fighters
        {
            get
            {
             
                return Helpers.Shared.Entities.Fighters.Local;
            }
        }
        /// 
        private Fighter _selectedFighter = null;

        /// <summary>
        /// Sets and gets the SelectedFighter property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Fighter SelectedFighter
        {
            get
            {
                return _selectedFighter;
            }

            set
            {
                if (_selectedFighter == value)
                {
                    return;
                }

                _selectedFighter = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveCommand { get; private set; }

      
    }
}