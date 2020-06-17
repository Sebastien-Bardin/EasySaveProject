using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.Display
{
    interface IView
    {
        //Method to Start the view with the banner and a space
        void Start();
        // Method use to creat the EasySave banner
        void Banner();
        //Method to show the software features menu and to collect the user choice
        string Menu();
        //Methode to collect the user task choice
        int TaskChoice();
        //Method to collect new task parameters
        Array FormTask();
        //Method to show save types options and collect the user choice
        string SaveTypeChoice();
        //Method to list all saved tasks
        void ListTasks(Array ListTasks);
        //Method to show continue dialog 
        string ContinuChoice();
        void UncorrectChoice();
        //Method to list drivers options 
        string DTypeChoice();
        //Method to show that 5 tasks have already been saved
        void NoTasks();

        int LanguageChoice();

    }
}
