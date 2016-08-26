/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Weebul.UI"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Weebul.UI.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
             //   SimpleIoc.Default.Register<IDataService, DesignDataService>();
            }
            else
            {
                // Create run time view services and models
                SimpleIoc.Default.Register<MainViewModel>();
                SimpleIoc.Default.Register(() => new FighterViewModel(), "F1");
                SimpleIoc.Default.Register(() => new FighterViewModel(), "F2");
                SimpleIoc.Default.Register<FightResultsViewModel>();
                SimpleIoc.Default.Register(() => new FightPlanViewModel(), "F1");
                SimpleIoc.Default.Register(() => new FightPlanViewModel(), "F2");
                SimpleIoc.Default.Register<OptionsViewModel>();
                SimpleIoc.Default.Register<ParserViewModel>();
                SimpleIoc.Default.Register<CompareDamageViewModel>();
                SimpleIoc.Default.Register<PivotSimViewModel>();
            }

     
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

        }

        /// <summary>
        /// Gets the Fighter1 property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public FighterViewModel Fighter1
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FighterViewModel>("F1");
            }
        }
        /// <summary>
        /// Gets the Fighter2 property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public FighterViewModel Fighter2
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FighterViewModel>("F2");
            }
        }


        /// <summary>
        /// Gets the FightResults property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public FightResultsViewModel FightResults
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FightResultsViewModel>();
            }
        }


        /// <summary>
        /// Gets the FightPlanF1 property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public FightPlanViewModel FightPlanF1
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FightPlanViewModel>("F1");
            }
        }

        /// <summary>
        /// Gets the FightPlanF2 property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public FightPlanViewModel FightPlanF2
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FightPlanViewModel>("F2");
            }
        }

        /// <summary>
        /// Gets the Options property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public OptionsViewModel Options
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OptionsViewModel>();
            }
        }

        /// <summary>
        /// Gets the Parser property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ParserViewModel Parser
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ParserViewModel>();
            }
        }

    

        /// <summary>
        /// Gets the CompareDamage property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public CompareDamageViewModel CompareDamage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CompareDamageViewModel>();
            }
        }


        /// <summary>
        /// Gets the PivotSim property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public PivotSimViewModel PivotSim
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PivotSimViewModel>();
            }
        }
    }
}