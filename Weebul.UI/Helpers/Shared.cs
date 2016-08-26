using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Weebul.Core.Model;
using Weebul.Data;
using Weebul.UI.ViewModel;

namespace Weebul.UI.Helpers
{
    public static class Shared
    {

        private static Lazy<WeebulEntities> _entities = new Lazy<WeebulEntities>(() =>
        {
            WeebulEntities e1 = new WeebulEntities();
            return e1;

        });
        public static WeebulEntities Entities
        {
            get
            {
                return _entities.Value; 
                
            }
        }

        public static void LoadFighter()
        {            
        }

        public static ViewModelLocator Locator()
        {
            ViewModelLocator locator = Application.Current.FindResource("Locator") as ViewModelLocator;
            return locator;
            //I can't figure out how to push without changing something;
        }
        public static FightTracker GetFightTracker()
        {
            Weebul.Core.Model.Fighter f1 = new Weebul.Core.Model.Fighter()

            {
                Stats = new FighterStats(Shared.Locator().Fighter1.SelectedFighter)
            };

            Weebul.Core.Model.Fighter f2 = new Weebul.Core.Model.Fighter()
            {
                Stats = new FighterStats(Shared.Locator().Fighter2.SelectedFighter)
            };

            Weebul.Core.Model.FightPlan fp1 = new Weebul.Core.Model.FightPlan()
            {
                FightPlanText = Shared.Locator().FightPlanF1.FightPlanText,
                WeblScript = new Scripting.WeblScript()
            };
            Weebul.Core.Model.FightPlan fp2 = new Weebul.Core.Model.FightPlan()
            {
                FightPlanText = Shared.Locator().FightPlanF2.FightPlanText,
                WeblScript = new Scripting.WeblScript()
            };
            if (!fp1.Validate())
            {
                MessageBox.Show("Fight plan 1 is invalid");
                return null; 
            }
            if (!fp2.Validate())
            {
                MessageBox.Show("Fight plan 2 is invalid");
                return null; 
            }

            FightTracker tracker = new FightTracker(f1, f2, fp1, fp2, Shared.Locator().Options.Options);
            return tracker; 
        }

    }
}
