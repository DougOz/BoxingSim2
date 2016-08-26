using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Core.Model
{
    public class FightOptions : ModelBase
    {


        /// 
        private WeightClass _weightClass = WeightClass.Heavy;

        /// <summary>
        /// Sets and gets the MyProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public WeightClass WeightClass
        {
            get
            {
                return _weightClass;
            }

            set
            {
                if (_weightClass == value)
                {
                    return;
                }

                _weightClass = value;
                OnPropertyChanged();
            }
        }

        /// 
        private int _totalRounds = 12;

        /// <summary>
        /// Sets and gets the TotalRounds property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int TotalRounds
        {
            get
            {
                return _totalRounds;
            }

            set
            {
                if (_totalRounds == value)
                {
                    return;
                }

                _totalRounds = value;
                OnPropertyChanged();
            }
        }

        /// 
        private double _luckAmount = 0.05;

        /// <summary>
        /// Sets and gets the LuckAmount property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>

        [Description("Standard deviation of the luck factor each round, used for computing damage and punches landed")]
        [DisplayName("Luck Std. Dev")]
        public double LuckAmount
        {
            get
            {
                return _luckAmount;
            }

            set
            {
                if (_luckAmount == value)
                {
                    return;
                }

                _luckAmount = value;
                OnPropertyChanged();
            }
        }
        /// 
        private double _cutFactor = 1;

        /// <summary>
        /// Sets and gets the CutFactor property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        /// 
        [Description("Multiplier applied to likelihood of cuts (1 is normal, 0 = no cuts, 0.5 = 50% of normal, etc.)")]
        [DisplayName("Cut Likelihood")]
        public double CutFactor
        {
            get
            {
                return _cutFactor;
            }

            set
            {
                if (_cutFactor == value)
                {
                    return;
                }

                _cutFactor = value;
                OnPropertyChanged();
            }
        }

        /// 
        private double _judgeLuck = 0.05;

        /// <summary>
        /// Sets and gets the JudgeLuck property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double JudgeLuck
        {
            get
            {
                return _judgeLuck;
            }

            set
            {
                if (_judgeLuck == value)
                {
                    return;
                }

                _judgeLuck = value;
                OnPropertyChanged();
            }
        }
        /// 
        private int _judges = 3;

        /// <summary>
        /// Sets and gets the Judges property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Judges
        {
            get
            {
                return _judges;
            }

            set
            {
                if (_judges == value)
                {
                    return;
                }

                _judges = value;
                OnPropertyChanged();
            }
        }

        /// 
        private bool _hitTestFightPlan = false;

        /// <summary>
        /// Sets and gets the HitTestFightPlan property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>        
        public bool HitTestFightPlan
        {
            get
            {
                return _hitTestFightPlan;
            }

            set
            {
                if (_hitTestFightPlan == value)
                {
                    return;
                }

                _hitTestFightPlan = value;
                OnPropertyChanged();
            }
        }
        public FightOptions Copy()
        {
            return new FightOptions()
            {
                WeightClass = this.WeightClass,
                TotalRounds = this.TotalRounds,
                LuckAmount = this.LuckAmount,
                CutFactor = this.CutFactor 
                
            };
        }
        
    }
}
