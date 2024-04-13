/* Title:           View Drop Bury Tasks
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
    /// Interaction logic for ViewDropBuryTasks.xaml
    /// </summary>
    public partial class ViewDropBuryTasks : Window
    {
        //setting up the classes
        DropBuryMDUClass TheDropBuryMDUClass = new DropBuryMDUClass();

        FindDropBuryTasksSortedDataSet TheFindDropBuryTasksSortedDataSet = new FindDropBuryTasksSortedDataSet();

        public ViewDropBuryTasks()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TheFindDropBuryTasksSortedDataSet = TheDropBuryMDUClass.FindDropBuryTasksSorted();

            dgrDropBuryTasks.ItemsSource = TheFindDropBuryTasksSortedDataSet.FindDropBuryTasksSorted;
        }
    }
}
