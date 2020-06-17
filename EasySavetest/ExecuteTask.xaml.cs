using EasySavetest.ViewModel;
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

namespace EasySavetest
{
    /// <summary>
    /// Logique d'interaction pour ExecuteTask.xaml
    /// </summary>
    public partial class ExecuteTask : Window
    {
        private static MainWindow MainWindow { get; set; }
        //Implementing view model
        View_Model View_Model = new View_Model();
        //list of paused items
        public static List<string> PausedItems = new List<string>();
        public ExecuteTask(MainWindow mainWindow)
        {
            //Main window recuperation 
            MainWindow = mainWindow;
            InitializeComponent();
            //Adding all saved tasks to the list 
            foreach (string task in (View_Model.ListAllTasks()))
            {
                TaskList.Items.Add(task);
            }
            
        }
        //Exit the window 
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
          
            Close();
            MainWindow.Show();

        }
        //Execute the selected task 
        private void ExecBtn_Click(object sender, RoutedEventArgs e)
        {
            View_Model.ExecuteTask(TaskList.SelectedItem.ToString());
        }
        //refresh the task list
        private void RefreshList()
        {
            TaskList.Items.Clear();
            foreach (string task in (View_Model.ListAllTasks()))
            {
                
                TaskList.Items.Add(task);
            }
        }
        // Open the creat task window 
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            CreateNewTask createNewTask = new CreateNewTask(MainWindow);
            createNewTask.Show();

        }
        //Button to refresh the task list when the user creat a new task 
        private void BtnRefresh(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }
        //Button to delete a selected task 
        private void BtnDelete(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem != null)
            {
                View_Model.DeleteTask(TaskList.SelectedItem.ToString());
                RefreshList();
            }
        }
        //Button to put the specified task on pause or replay it if it was already 
        private void BtnPause(object sender, RoutedEventArgs e)
        {
           
            if (TaskList.SelectedItem != null && !PausedItems.Contains(TaskList.SelectedItem.ToString()))
            {
                View_Model.PlayPauseTask(TaskList.SelectedItem.ToString());
                PausedItems.Add(TaskList.SelectedItem.ToString());
                PauseBtn.Content = "replay";
            }
            else if (TaskList.SelectedItem != null && PausedItems.Contains(TaskList.SelectedItem.ToString()))
            {
                View_Model.PlayPauseTask(TaskList.SelectedItem.ToString());
                PausedItems.Remove(TaskList.SelectedItem.ToString());
                PauseBtn.Content = "play";
            }
        }
        // Button canceling the selected running task 
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem != null)
            {
                View_Model.Canceltask(TaskList.SelectedItem.ToString());
            }
        }
        //refreshing the display button 
        private void SelectedTaskChange(object sender, SelectionChangedEventArgs e)
        {
            foreach (string item in PausedItems)
            {
                if (item == TaskList.SelectedItem.ToString())
                {
                    PauseBtn.Content="replay";
                }
                else
                {
                    PauseBtn.Content = "Pause";
                }
            }
        }
    }
}
