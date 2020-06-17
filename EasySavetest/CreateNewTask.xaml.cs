using EasySavetest.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;



namespace EasySavetest
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// 
  
    public partial class CreateNewTask : Window
    {
        //Main window 
        private static MainWindow MainWindow { get; set; }
        //viewmodel implementation
        View_Model View_Model =new View_Model();
        public CreateNewTask(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            InitializeComponent();
        }
        OpenFileDialog ofd = new OpenFileDialog();
       //Recuperating task information for cereating a new task but we first verify if all textbox are completed
        void BtnCreate(object sender, RoutedEventArgs e)
        {
            bool exist = false;
            foreach(string task in View_Model.ListAllTasks())
            {
                if (TaskName.Text == task)
                {
                    exist= true;
                    ErrorName.Content = "A task have been already saved with this name";
                }

            }
         
            if (TaskName.Text != "" && SourcePath.Text!="" && DestinationPath.Text != "" && ComboBox2.SelectedItem !=null && ComboBox1.SelectedItem !=null && !exist) {
                string[] parameters = new string[5];
                parameters[0] = TaskName.Text;
                parameters[1] = ComboBox2.Text;
                parameters[2] = SourcePath.Text;
                parameters[3] = DestinationPath.Text;
                parameters[4] = ComboBox1.Text;
                View_Model.CreatTask(parameters);
                TaskName.Text = "";
                SourcePath.Text = "";
                DestinationPath.Text = "";
            }

            if (TaskName.Text == "")
            {
                ErrorName.Content= "This field is required ";
            }
            if (SourcePath.Text == "")
            {
                ErrorSource.Content = "This field is required ";
            }
            if (DestinationPath.Text == "")
            {
                ErrorDestination.Content = "This field is required ";
            }
            if (ComboBox1.SelectedItem == null)
            {
                TextTypeSave.Text = "This field is required ";
            }
            if (ComboBox2.SelectedItem == null)
            {
                TextTypeDestination.Text = "This field is required ";
            }
        }

        OpenFileDialog ofd1 = new OpenFileDialog();

        //Button to open the file browser 
        private void BrowseSource_Click(object sender, RoutedEventArgs e)
        {
            ofd.ShowDialog();
            SourcePath.Text = ofd.FileName;
        }
        //Button to open the file browser 
        private void BrowseDestination_Click(object sender, RoutedEventArgs e)
        {
            ofd1.ShowDialog();
            DestinationPath.Text = ofd1.FileName;
        }
        //button to exit the window 
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
