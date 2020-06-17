using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySave.ViewModel;


namespace EasySave.Display
{
    class View
    {
        public IView _view;



        public void SetViewStrategy(IView view)
        {
            this._view = view;
        }

        public void ReadViewStrategy()
        {
            if (!File.Exists("Language.txt"))
            {
                File.WriteAllText("Language.txt", "en");
            }
            string Language = File.ReadAllText("Language.txt");
            Console.WriteLine(Language);
            if (Language == "en")
            {
                SetViewStrategy(new EnglishView());
            }
            else
            {
                SetViewStrategy(new FrenchView());
            }

        }

        public void ChangeViewStrategy()
        {
            string language;
            int choice = _view.LanguageChoice();
            if (choice == 1)
            {
                language = "en";
            }
            else
            {
                language = "fr";
            }

            File.WriteAllText("Language.txt", language);
        }
        //Method to Start the view with the banner and a space
        public void Start()
        {
            _view.Start();
        }
        // Method use to creat the EasySave banner
        public void Banner()
        {
            _view.Banner();
        }

        //Method to show the software features menu and to collect the user choice
        public string Menu()
        {
            return _view.Menu();
        }
        //Methode to collect the user task choice
        public int TaskChoice()
        {
            return _view.TaskChoice();
        }

        //Method to collect new task parameters
        public Array FormTask()
        {
            return _view.FormTask();

        }

        //Method to show save types options and collect the user choice
        public string SaveTypeChoice()
        {
            return _view.SaveTypeChoice();
        }

        //Method to list all saved tasks
        public void ListTasks(Array ListTasks)
        {
            _view.ListTasks(ListTasks);
        }

        //Method to show continue dialog 
        public string ContinuChoice()
        {
            return _view.ContinuChoice();
        }
        public void UncorrectChoice()
        {
            _view.UncorrectChoice();
        }
        //Method to list drivers options 
        public string DTypeChoice()
        {
            return _view.DTypeChoice();
        }

        //Method to show that 5 tasks have already been saved
        public void NoTasks()
        {
            _view.NoTasks();
        }

    }
}
