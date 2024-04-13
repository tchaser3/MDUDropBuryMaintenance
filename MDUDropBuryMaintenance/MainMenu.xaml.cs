/* Title:           Main Menu
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

namespace MDUDropBuryMaintenance
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        //setting up the classes
        WPFMessagesClass TheMessagesClass = new WPFMessagesClass();

        public MainMenu()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            TheMessagesClass.CloseTheProgram();
        }

        private void btnCreateWorkZone_Click(object sender, RoutedEventArgs e)
        {
            CreateWorkZone CreateWorkZone = new CreateWorkZone();
            CreateWorkZone.Show();
            Close();
        }

        private void btnCreateWorkOrderStatus_Click(object sender, RoutedEventArgs e)
        {
            CreateWorkOrderStatus CreateWorkOrderStatus = new CreateWorkOrderStatus();
            CreateWorkOrderStatus.Show();
            Close();
        }

        private void btnCreateDropBuryTask_Click(object sender, RoutedEventArgs e)
        {
            CreateDropBuryTask CreateDropBuryTask = new CreateDropBuryTask();
            CreateDropBuryTask.Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreateMDUTask_Click(object sender, RoutedEventArgs e)
        {
            CreateMDUTask CreateMDUTask = new CreateMDUTask();
            CreateMDUTask.Show();
            Close();
        }

        private void btnEditMDUTask_Click(object sender, RoutedEventArgs e)
        {
            SelectMDUTask SelectMDUTask = new SelectMDUTask();
            SelectMDUTask.Show();
            Close();
        }
    }
}
