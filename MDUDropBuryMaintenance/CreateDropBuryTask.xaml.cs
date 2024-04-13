/* Title:           Create Drop Bury Task
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
    /// Interaction logic for CreateDropBuryTask.xaml
    /// </summary>
    public partial class CreateDropBuryTask : Window
    {
        //setting up the classes
        WPFMessagesClass TheMessagesClass = new WPFMessagesClass();
        DropBuryMDUClass TheDropBuryMDUClass = new DropBuryMDUClass();

        FindDropBuryTaskByDescriptionDataSet TheFindDropBuryTaskByDescriptionDataSet = new FindDropBuryTaskByDescriptionDataSet();

        public CreateDropBuryTask()
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDropBuryTask.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //setting local variables
            string strDropBuryTask;
            int intRecordsReturned;
            bool blnFatalError;

            strDropBuryTask = txtDropBuryTask.Text;
            if(strDropBuryTask == "")
            {
                TheMessagesClass.ErrorMessage("The Drop Bury Task Was Not Entered");
                return;
            }

            TheFindDropBuryTaskByDescriptionDataSet = TheDropBuryMDUClass.FindDropBuryTaskByDescription(strDropBuryTask);

            intRecordsReturned = TheFindDropBuryTaskByDescriptionDataSet.FindDropBuryTaskByDescription.Rows.Count;

            if(intRecordsReturned > 0 )
            {
                TheMessagesClass.InformationMessage("The Drop Bury Task Exists");
                return;
            }
            else
            {
                blnFatalError = TheDropBuryMDUClass.InsertDropBuryTask(strDropBuryTask);

                if(blnFatalError == true)
                {
                    TheMessagesClass.ErrorMessage("There Was A Problem, Contact IT");
                    return;
                }
                else
                {
                    txtDropBuryTask.Text = "";
                    txtDropBuryTask.Focus();
                    TheMessagesClass.InformationMessage("The Drop Bury Task Has Been Saved");
                }
            }
        }

        private void btnViewDropBuryTasks_Click(object sender, RoutedEventArgs e)
        {
            ViewDropBuryTasks ViewDropBuryTasks = new ViewDropBuryTasks();
            ViewDropBuryTasks.ShowDialog();
        }
    }
}
