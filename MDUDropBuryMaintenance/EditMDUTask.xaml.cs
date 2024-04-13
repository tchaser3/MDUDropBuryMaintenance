/* Title:           Edit MDU Task
 * Date:            8-3-17
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
using DataValidationDLL;
using DropBuryMDUDLL;
using NewEventLogDLL;

namespace MDUDropBuryMaintenance
{
    /// <summary>
    /// Interaction logic for EditMDUTask.xaml
    /// </summary>
    public partial class EditMDUTask : Window
    {
        //setting up the clases
        WPFMessagesClass TheMessagesClass = new WPFMessagesClass();
        DataValidationClass TheDataValidationClass = new DataValidationClass();
        DropBuryMDUClass TheDropBuryMDUClass = new DropBuryMDUClass();
        EventLogClass TheEventLogClass = new EventLogClass();

        //setting up the data
        FindMDUTaskByTaskIDDataSet TheFindMDUTaskByTaskIDDataSet = new FindMDUTaskByTaskIDDataSet();

        public EditMDUTask()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this will load up the controls
            try
            {
                TheFindMDUTaskByTaskIDDataSet = TheDropBuryMDUClass.FindMDUTaskByTaskID(MainWindow.gintTaskID);

                txtTaskID.Text = Convert.ToString(TheFindMDUTaskByTaskIDDataSet.FindMDUTaskByTaskID[0].TaskID);
                txtTaskCode.Text = TheFindMDUTaskByTaskIDDataSet.FindMDUTaskByTaskID[0].TaskCode;
                txtTaskDescription.Text = TheFindMDUTaskByTaskIDDataSet.FindMDUTaskByTaskID[0].TaskDescription;
                txtTaskPrice.Text = Convert.ToString(TheFindMDUTaskByTaskIDDataSet.FindMDUTaskByTaskID[0].TaskPrice);
            }
            catch (Exception Ex)
            {
                TheEventLogClass.InsertEventLogEntry(DateTime.Now, "MDU Drop Bury Maintenance // Edit MDU Task // Window Loaded " + Ex.Message);

                TheMessagesClass.ErrorMessage(Ex.ToString());
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //setting local variables
            string strValueForValidation;
            float fltPrice = 0;
            string strTaskDescription;
            string strErrorMessage = "";
            bool blnFatalError = false;
            bool blnThereIsAProblem = false;

            try
            {
                //data validation
                strTaskDescription = txtTaskDescription.Text;
                if(strTaskDescription == "")
                {
                    blnFatalError = true;
                    strErrorMessage += "The Task Was Not Entered\n";
                }
                strValueForValidation = txtTaskPrice.Text;
                blnThereIsAProblem = TheDataValidationClass.VerifyDoubleData(strValueForValidation);
                if(blnThereIsAProblem == true)
                {
                    blnFatalError = true;
                    strErrorMessage += "The Price Is Not Numeric\n";
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

                if(TheFindMDUTaskByTaskIDDataSet.FindMDUTaskByTaskID[0].TaskDescription != strTaskDescription)
                {
                    blnFatalError = TheDropBuryMDUClass.UpdateMDUTaskDescription(MainWindow.gintTaskID, strTaskDescription);
                }

                if(blnFatalError == false)
                {
                    blnFatalError = TheDropBuryMDUClass.UpdateMDUTaskPrice(MainWindow.gintTaskID, fltPrice);
                }

                if(blnFatalError == true)
                {
                    TheMessagesClass.ErrorMessage("There Was A Problem, Contact IT");
                    return;
                }
                else
                {
                    TheMessagesClass.InformationMessage("The Task Has Been Updated");
                    Close();
                }
            }
            catch (Exception Ex)
            {
                TheEventLogClass.InsertEventLogEntry(DateTime.Now, "MDU Drop Bury Maintenance // Edit MDU Task // Save Button " + Ex.Message);

                TheMessagesClass.ErrorMessage(Ex.ToString());
            }
        }
    }
}
