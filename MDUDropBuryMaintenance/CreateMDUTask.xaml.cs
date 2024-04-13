/* Title:           Create MDU Tasks
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
using DataValidationDLL;
using NewEventLogDLL;

namespace MDUDropBuryMaintenance
{
    /// <summary>
    /// Interaction logic for CreateMDUTask.xaml
    /// </summary>
    public partial class CreateMDUTask : Window
    {
        //setting up the classes
        WPFMessagesClass TheMessagesClass = new WPFMessagesClass();
        DropBuryMDUClass TheDropBuryMDUClass = new DropBuryMDUClass();
        DataValidationClass TheDataValidationClass = new DataValidationClass();
        EventLogClass TheEventLogClass = new EventLogClass();

        //settup the data
        FindMDUTaskByTaskCodeDataSet TheFindMDUTaskByTaskCodeDataSet = new FindMDUTaskByTaskCodeDataSet();
        FindMDUTaskByDescriptionDataSet TheFindMDUTaskByDescriptionDataSet = new FindMDUTaskByDescriptionDataSet();

        public CreateMDUTask()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtTaskCode.Focus();
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

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //setting local variables
            string strValueForValidation;
            string strTaskCode;
            string strTaskDescription;
            string strErrorMessage = "";
            float fltPrice = 0;
            int intRecordsReturned;
            bool blnThereIsAProblem = false;
            bool blnFatalError = false;

            try
            {
                //data validation
                strTaskCode = txtTaskCode.Text;
                if(strTaskCode == "")
                {
                    blnFatalError = true;
                    strErrorMessage += "The Task Code Is Blank\n";
                }
                else
                {
                    TheFindMDUTaskByTaskCodeDataSet = TheDropBuryMDUClass.FindMDUTaskByTaskCode(strTaskCode);

                    intRecordsReturned = TheFindMDUTaskByTaskCodeDataSet.FindMDUTasksByTaskCode.Rows.Count;

                    if(intRecordsReturned > 0)
                    {
                        blnFatalError = true;
                        strErrorMessage += "The Task Code Exists\n";
                    }
                }
                strTaskDescription = txtTaskDescription.Text;
                if(strTaskDescription == "")
                {
                    blnFatalError = true;
                    strErrorMessage += "The Task Description Was Not Entered\n";
                }
                else
                {
                    TheFindMDUTaskByDescriptionDataSet = TheDropBuryMDUClass.FindMDUTaskByDescription(strTaskDescription);

                    intRecordsReturned = TheFindMDUTaskByDescriptionDataSet.FindMDUTaskByDescription.Rows.Count;

                    if(intRecordsReturned > 0)
                    {
                        blnFatalError = true;
                        strErrorMessage += "The MDU Task Exists\n";
                    }
                }
                strValueForValidation = txtTaskPrice.Text;
                blnThereIsAProblem = TheDataValidationClass.VerifyDoubleData(strValueForValidation);
                if(blnThereIsAProblem == true)
                {
                    blnFatalError = true;
                    strErrorMessage += "The Task Price Is Not Numeric\n";
                }
                else
                {
                    blnThereIsAProblem = float.TryParse(strValueForValidation, out fltPrice);
                }

                if(blnFatalError == true)
                {
                    TheMessagesClass.ErrorMessage(strErrorMessage);
                    return;
                }

                blnFatalError = TheDropBuryMDUClass.InsertMDUTask(strTaskCode, strTaskDescription, fltPrice);
                
                if(blnFatalError == true)
                {
                    TheMessagesClass.ErrorMessage("There Has Been A Problem, Contact IT");
                    return;
                }
                else
                {
                    txtTaskCode.Text = "";
                    txtTaskDescription.Text = "";
                    txtTaskPrice.Text = "";
                    TheMessagesClass.InformationMessage("The Task Has Been Saved");
                    txtTaskCode.Focus();
                }
            }
            catch (Exception Ex)
            {
                TheEventLogClass.InsertEventLogEntry(DateTime.Now, "MDU Drop Bury Maintenance // Create MDU Task // Save Button " + Ex.Message);

                TheMessagesClass.ErrorMessage(Ex.ToString());
            }
        }

        private void btnViewMDUTasks_Click(object sender, RoutedEventArgs e)
        {
            ViewMDUTasks ViewMDUTasks = new ViewMDUTasks();
            ViewMDUTasks.ShowDialog();
        }
    }
}
