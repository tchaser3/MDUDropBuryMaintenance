/* Title:           View Work Order Statuses
 * Date:            8-2-17
 * Author:          Terry Holmes */

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
using WorkOrderDLL;

namespace MDUDropBuryMaintenance
{
    /// <summary>
    /// Interaction logic for ViewWorkOrderStatus.xaml
    /// </summary>
    public partial class ViewWorkOrderStatus : Window
    {
        //Setting up the classes
        WPFMessagesClass TheMessagesClass = new WPFMessagesClass();
        WorkOrderClass TheWorkOrderClass = new WorkOrderClass();

        FindWorkOrderStatusSortedDataSet TheFindWorkOrderStatusSortedDataSet = new FindWorkOrderStatusSortedDataSet();

        public ViewWorkOrderStatus()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TheFindWorkOrderStatusSortedDataSet = TheWorkOrderClass.FindWorkOrderStatusSorted();

            dgrWorkOrderStatuses.ItemsSource = TheFindWorkOrderStatusSortedDataSet.FindWorkOrderStatusSorted;
        }
    }
}
