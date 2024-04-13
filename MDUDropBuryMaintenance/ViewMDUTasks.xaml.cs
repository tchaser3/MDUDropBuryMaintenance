/* Title:           View MDU Tasks
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
using DropBuryMDUDLL;

namespace MDUDropBuryMaintenance
{
    /// <summary>
    /// Interaction logic for ViewMDUTasks.xaml
    /// </summary>
    public partial class ViewMDUTasks : Window
    {
        //setting up the class
        DropBuryMDUClass TheDropBuryMDUClass = new DropBuryMDUClass();

        //setting up the data
        FindMDUTasksSortedDataSet TheFindMDUTasksSortedDataSet = new FindMDUTasksSortedDataSet();

        public ViewMDUTasks()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TheFindMDUTasksSortedDataSet = TheDropBuryMDUClass.FindMDUTasksSorted();

            dgrMDUTasks.ItemsSource = TheFindMDUTasksSortedDataSet.FindMDUTasksSorted;
        }
    }
}
