using EasySavetest.ViewModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;

namespace EasySavetest
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { //view model implementation
        View_Model View_Model = new View_Model();
        //mutex used for mono-instance
        private static Mutex m_Mutex;
        //boolean used for monoinstance
        private static bool createdNew;

        public MainWindow()
        {
            // mono instance implementation
           
           m_Mutex = new Mutex(true, "EasySaveMutex", out createdNew);
           if (createdNew)
           {
                InitializeComponent();
                View_Model.InitializeProcess();
           }
           else
           {
                MessageBox.Show("The application is already running.");
                this.Close();
           }
        }



       //Button to open the task managing window
        private void BtnExecute_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Task.json") )
            {
                ExecuteTask executeTask = new ExecuteTask(this);
                Hide();
                executeTask.Show();
            }
           
        }

        //Button to execute all tasks 
        private void BtnExecAll_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Task.json") && View_Model.ListAllTasks().Length != 0)
            {
                View_Model.ExecuteAllTasks();
            }
            else
            {
                ErrorExecAll.Content = "You don't have any saved task";
            }
           

        }
        //Button to enter Configuration file
        private void BtnConfig_Click(object sender, RoutedEventArgs e)
        {
            Configuration configuration = new Configuration(this);
            Hide();
            configuration.Show();
        }
        //Button to end the program
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            foreach (Process process in Process.GetProcessesByName("EasySavetest"))
            {
                process.Kill();
            }
            Close();
        }
    }
}
