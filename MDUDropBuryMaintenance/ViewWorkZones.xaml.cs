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
using CustomersDLL;

namespace MDUDropBuryMaintenance
{
    /// <summary>
    /// Interaction logic for ViewWorkZones.xaml
    /// </summary>
    public partial class ViewWorkZones : Window
    {
        //setting up the classes
        WPFMessagesClass TheMessagesClass = new WPFMessagesClass();
        CustomersClass TheCustomersClass = new CustomersClass();

        FindWorkZonesDataSet TheFindWorkZonesDataSet = new FindWorkZonesDataSet();

        public ViewWorkZones()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TheFindWorkZonesDataSet = TheCustomersClass.FindWorkZones();

            dgrWorkZones.ItemsSource = TheFindWorkZonesDataSet.FindWorkZones;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
