using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Data;
using System.ComponentModel;
namespace Weebul.UI.View
{
    /// <summary>
    /// Description for FightResultsView.
    /// </summary>
    public partial class FightResultsView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the FightResultsView class.
        /// </summary>
        public FightResultsView()
        {
            InitializeComponent();
        }

        private void RadGridView_AutoGeneratingColumn(object sender, Telerik.Windows.Controls.GridViewAutoGeneratingColumnEventArgs e)
        {
            if(e.ItemPropertyInfo.PropertyType == typeof(double) ||  (e.ItemPropertyInfo.PropertyType==typeof(int) && e.Column.Name != "Round"))
            {
                AggregateFunction af = new SumFunction()
                { Caption = "Sum: " };
                if (e.ItemPropertyInfo.Name.Contains("Percent"))
                {
                    af = new AverageFunction();
                    af.Caption = "Avg: ";
                }
                var info = e.ItemPropertyInfo.Descriptor as System.ComponentModel.PropertyDescriptor;


                var att = info.Attributes.OfType<DisplayFormatAttribute>().FirstOrDefault();

                if(att != null)
                {
                    af.ResultFormatString = "{0:" + att.DataFormatString + "}";
                }
                
                    
                //if (info.CustomAttributes.Any(c=>c.AttributeType == typeof(DisplayFormatAttribute)))
                //{
                //    string format =info.CustomAttributes.First(f => f.AttributeType == typeof(DisplayFormatAttribute)).NamedArguments.First(f => f.MemberName == "DataFormatString").TypedValue.Value as String;
                //    if (format != null)
                //    {
                //        af.ResultFormatString = format;
                //    }
                //}
                


                e.Column.AggregateFunctions.Add(af);
            }
        }
    }
}