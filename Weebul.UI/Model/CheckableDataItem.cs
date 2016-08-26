using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weebul.Core.Model;

namespace Weebul.UI.Model
{
    public class CheckableDataItem : ModelBase
    {

        public CheckableDataItem(object dataObject, string name, int id)
        {
            this.DataObject = dataObject;
            this.Name = name;
            this.Id = id;
        }

        public CheckableDataItem(Data.FightPlan fightPlan) : this(fightPlan, fightPlan.PlanName, fightPlan.PlanId)
        {

        }

        public CheckableDataItem(Data.Fighter fighter) : this(fighter, fighter.Name, fighter.FighterId )
        {
            WeightClass weightClass = FighterStats.GetWeightClass(fighter.Weight, fighter.Conditioning);
            string name = Enum.GetName(typeof(WeightClass), weightClass);
            this.Name = String.Format("{0} ({1})", fighter.Name, name);
        }
        public CheckableDataItem(TrainingStat stat) : this(stat, Enum.GetName(typeof(TrainingStat), stat),(int)stat)
        {

        }
        public Object DataObject { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }
        /// 
        private bool _isChecked = false;

        /// <summary>
        /// Sets and gets the IsChecked property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }

            set
            {
                if (_isChecked == value)
                {
                    return;
                }

                _isChecked = value;
                OnPropertyChanged();
            }
        }

        
    }
}
