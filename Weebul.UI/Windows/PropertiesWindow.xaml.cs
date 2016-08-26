using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Weebul.UI.Windows
{
    /// <summary>
    /// Interaction logic for PropertyWindow.xaml
    /// </summary>
    public partial class PropertiesWindow : Window
    {
        public Object EditObject { get; set; }
        public PropertiesWindow(object toEdit)
        {
            InitializeComponent();
            EditObject = toEdit;
            propGrid.SelectedObject = toEdit;
        }
        private void propGrid_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close(); 
        }

        private void btnDefault_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public static bool EditProperties(object obj)
        {
            PropertiesWindow window = new PropertiesWindow(obj);

            return window.ShowDialog() == true; 
        
        }
    }
}
