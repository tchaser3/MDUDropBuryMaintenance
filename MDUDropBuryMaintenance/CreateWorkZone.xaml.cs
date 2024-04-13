/* Title:           Create Work Zone
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
using CustomersDLL;

namespace MDUDropBuryMaintenance
{
    /// <summary>
    /// Interaction logic for CreateWorkZone.xaml
    /// </summary>
    public partial class CreateWorkZone : Window
    {
        WPFMessagesClass TheMessagesClass = new WPFMessagesClass();
        CustomersClass TheCustomerClass = new CustomersClass();

        FindWorkZoneByZoneNameDataSet TheFindWorkzoneByZoneNameDataSet = new FindWorkZoneByZoneNameDataSet();

        public CreateWorkZone()
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            TheMessagesClass.CloseTheProgram();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //setting local variables
            string strWorkZone;
            int intRecordsReturned;
            bool blnFatalError;

            strWorkZone = txtWorkZone.Text;
            if(strWorkZone == "")
            {
                TheMessagesClass.ErrorMessage("Work Zone Was Not Entered");
                return;
            }

            TheFindWorkzoneByZoneNameDataSet = TheCustomerClass.FindWorkZoneByZoneName(strWorkZone);

            intRecordsReturned = TheFindWorkzoneByZoneNameDataSet.FindWorkZoneByZoneName.Rows.Count;

            if(intRecordsReturned > 0)
            {
                TheMessagesClass.ErrorMessage("Work Zone Already Existins");
                return;
            }
            else
            {
                blnFatalError = TheCustomerClass.InsertWorkZone(strWorkZone);

                if(blnFatalError == true)
                {
                    TheMessagesClass.ErrorMessage("There Was a Problem, Contact IT");
                    return;
                }
                else
                {
                    txtWorkZone.Text = "";
                    txtWorkZone.Focus();
                    TheMessagesClass.InformationMessage("Work Zone Was Saved");
                }
            }
        }

        private void btnViewWorkZones_Click(object sender, RoutedEventArgs e)
        {
            ViewWorkZones ViewWorkZones = new ViewWorkZones();
            ViewWorkZones.ShowDialog();
        }
    }
}
