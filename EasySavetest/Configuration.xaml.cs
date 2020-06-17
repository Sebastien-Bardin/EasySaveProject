using EasySavetest.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logique d'interaction pour Configuration.xaml
    /// </summary>
    public partial class Configuration : Window
    {
        private static MainWindow MainWindow { get; set; }
        //View model implementation
        View_Model View_Model = new View_Model();
        public Configuration(MainWindow mainWindow)
        {
            //Main window 
            MainWindow = mainWindow;
            InitializeComponent();
            //Listing all configurations
            if (File.Exists("ConfigExtention.json"))
            {

                ExecList.Items.Clear();
                foreach (string Exec in View_Model.ListAllExec())
                {
                    ExecList.Items.Add(Exec);

                }
            }
            if (File.Exists("ConfigMetier.json"))
            {
                SoftList.Items.Clear();
                foreach (string Soft in View_Model.ListAllSoft())
                {
                   
                    SoftList.Items.Add(Soft);
                }
            }
            if (File.Exists("ConfigPriority.json"))
            {
                ListPrio.Items.Clear();
                foreach (string Prio in View_Model.ListAllPrio())
                {

                    ListPrio.Items.Add(Prio);
                }
            }

        }

        //Button to exit the window
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Show();
            Close();
            
        }
        //Button to add extention to encrypt
        private void AddExec_Click(object sender, RoutedEventArgs e)
        {
           
                View_Model.CreatExec(ExecName.Text);
                RefreshList();



        }
        //Button to add soft to watch
        private void AddSoft_Click(object sender, RoutedEventArgs e)
        {
            View_Model.CreatSoft(SoftName.Text);
            RefreshList();
        }
        //Button to add an extention priority 
        private void AddPrio_Click(object sender, RoutedEventArgs e)
        {
            View_Model.CreatPrio(Prio.Text);
            RefreshList();
        }

        //Button to delete a crypted extention
        private void DeleteExec_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("ConfigExtention.json")&&ExecList.SelectedItem!=null)
            {
                View_Model.DeleteExec(ExecList.SelectedItem.ToString());
                RefreshList();
            }
          
         

        }

        //Button to delete a watched software
        private void DeleteSoft_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("ConfigMetier.json") && SoftList.SelectedItem != null)
            {
                View_Model.DeleteSoft(SoftList.SelectedItem.ToString());
                RefreshList();
            }
        
           

        }

        //method to refresh all lists
        private void RefreshList()
        {
            if (File.Exists("ConfigExtention.json"))
            {
                ExecList.Items.Clear();
                foreach (string Exec in View_Model.ListAllExec())
                {
                    ExecList.Items.Add(Exec);

                }
            }
            if (File.Exists("ConfigMetier.json"))
            {
                SoftList.Items.Clear();
                foreach (string Soft in View_Model.ListAllSoft())
                {
                   
                    SoftList.Items.Add(Soft);
                }
            }
            if (File.Exists("ConfigPriority.json"))
            {
                ListPrio.Items.Clear();
                foreach (string Prio in View_Model.ListAllPrio())
                {

                    ListPrio.Items.Add(Prio);
                }
            }


        }

       //Button deleting extention priority
        private void DeletePrio_Click(object sender, RoutedEventArgs e)
        {

            if (File.Exists("ConfigPriority.json") && ListPrio.SelectedItem != null)
            {
                View_Model.DeletePrio(ListPrio.SelectedItem.ToString());
                RefreshList();
            }
          
        }
    }
}
