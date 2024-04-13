/* Title:           Select MDU Task
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
using NewEventLogDLL;
using DropBuryMDUDLL;

namespace MDUDropBuryMaintenance
{
    /// <summary>
    /// Interaction logic for SelectMDUTask.xaml
    /// </summary>
    public partial class SelectMDUTask : Window
    {
        WPFMessagesClass TheMessagesClass = new WPFMessagesClass();
        EventLogClass TheEventLogClass = new EventLogClass();
        DropBuryMDUClass TheDropBuryMDUClass = new DropBuryMDUClass();

        FindMDUTasksSortedDataSet TheFindMDUTaskSortedDataSet = new FindMDUTasksSortedDataSet();

        public SelectMDUTask()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenu MainMenu = new MainMenu();
            MainMenu.Show();
            Close();
        }
        
        private void LoadGrid()
        {
            TheFindMDUTaskSortedDataSet = TheDropBuryMDUClass.FindMDUTasksSorted();

            dgrMDUTasks.ItemsSource = TheFindMDUTaskSortedDataSet.FindMDUTasksSorted;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            TheMessagesClass.CloseTheProgram();
        }

        private void dgrMDUTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //setting local variables
            int intSelectedIndex;

            intSelectedIndex = dgrMDUTasks.SelectedIndex;

            if(intSelectedIndex > -1)
            {
                MainWindow.gintTaskID = TheFindMDUTaskSortedDataSet.FindMDUTasksSorted[intSelectedIndex].TaskID;

                EditMDUTask EditMDUTask = new EditMDUTask();
                EditMDUTask.ShowDialog();

                LoadGrid();
            }
        }
    }
}
