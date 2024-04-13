/* Title:           Create Work Order Status
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
    /// Interaction logic for CreateWorkOrderStatus.xaml
    /// </summary>
    public partial class CreateWorkOrderStatus : Window
    {
        //setting up the class
        WPFMessagesClass TheMessagesClass = new WPFMessagesClass();
        WorkOrderClass TheWorkOrderClass = new WorkOrderClass();

        FindWorkOrderStatusByStatusDataSet TheFindWorkOrderStatusByStatusDataSet = new FindWorkOrderStatusByStatusDataSet();

        public CreateWorkOrderStatus()
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

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenu MainMenu = new MainMenu();
            MainMenu.Show();
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //setting local variables
            string strStatus;
            int intRecordsReturned;
            bool blnFatalError;

            strStatus = txtWorkOrderStatus.Text;
            if(strStatus == "")
            {
                TheMessagesClass.ErrorMessage("The Work Order Status Was Not Entered");
                return;
            }

            TheFindWorkOrderStatusByStatusDataSet = TheWorkOrderClass.FindWorkOrderStatusByStatus(strStatus);

            intRecordsReturned = TheFindWorkOrderStatusByStatusDataSet.FindWorkOrderStatusByStatus.Rows.Count;

            if(intRecordsReturned > 0)
            {
                TheMessagesClass.InformationMessage("The Work Order Status Is Already Entered");
                return;
            }
            else
            {
                blnFatalError = TheWorkOrderClass.InsertWorkOrderStatus(strStatus);

                if(blnFatalError == true)
                {
                    TheMessagesClass.ErrorMessage("There Was A Problem, Contact IT");
                    return;
                }
                else
                {
                    txtWorkOrderStatus.Text = "";
                    txtWorkOrderStatus.Focus();
                    TheMessagesClass.InformationMessage("The Work Order Status Is Saved");
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtWorkOrderStatus.Focus();
        }

        private void btnViewWorkOrderStatus_Click(object sender, RoutedEventArgs e)
        {
            ViewWorkOrderStatus ViewWorkOrderStatus = new ViewWorkOrderStatus();
            ViewWorkOrderStatus.ShowDialog();
        }
    }
}
