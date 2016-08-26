using System.Windows;
using System.Windows.Controls;
using Weebul.Data;
using Weebul.UI.Helpers;
using Weebul.UI.ViewModel;

namespace Weebul.UI.View
{
    /// <summary>
    /// Description for FighterView.
    /// </summary>
    public partial class FighterView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the FighterView class.
        /// </summary>
        /// 

        
        public FighterView()
        {
            InitializeComponent();
        }

        private void rdfFighters_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {
            
            Shared.Entities.SaveChanges();
            
        }

        private void rdfFighters_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {
            if(e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            {
                Shared.Entities.SaveChanges();
            }
        }

        private void rdfFighters_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        {
            Shared.Entities.SaveChanges();
        }

        private void rdfFighters_AutoGeneratingField(object sender, Telerik.Windows.Controls.Data.DataForm.AutoGeneratingFieldEventArgs e)
        {
            if(e.PropertyName == "Fights1" || e.PropertyName== "Fights2")
            {
                e.Cancel = true; 
            }
        }
    }
}