using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySave.Display;
using EasySave.Model;

namespace EasySave.ViewModel
{
    class View_Model
    {   //Implementation of the view and model objects
        JsonTask _jsonTask;
        View _view;
        string choice;
        ExecuteSave _executeSave;

        //View model constructor starting the view
        public View_Model(View view)
        {

            _jsonTask = new JsonTask();

            _view = view;
            view.Start();
            ReadMenu();
        }

        //Method for creating tasks 
        public void CreatTask()
        {

            string[] TaskTab = new string[5];
            _view.FormTask().CopyTo(TaskTab, 0);
            _jsonTask.CreatJsonTask(TaskTab[0], TaskTab[1], TaskTab[2], TaskTab[3], TaskTab[4]);
            Continue();


        }
        //Method to delete a choosed task
        public void DeleteTask()
        {
            if (File.Exists("Task.json"))
            {
                _view.ListTasks(_jsonTask.ListAllTasks());
                _jsonTask.DeleteTask(_view.TaskChoice());
                Continue();
            }
            else
            {
                _view.NoTasks();
                Continue();
            }
        }
        //Method to execute a saved task
        public void ExecuteTask()
        {
            if (File.Exists("Task.json"))
            {
                _view.ListTasks(_jsonTask.ListAllTasks());
                int NumTask = _jsonTask.ListAllTasks().GetLength(0);

                int tasknumber = _view.TaskChoice();

                if (tasknumber > NumTask)
                {
                    _view.UncorrectChoice();
                    ExecuteTask();

                }
                else
                {
                    if (_jsonTask.FindTask(tasknumber).GetValue(1).ToString() == "Mirror")
                    {
                        _executeSave = new ExecuteSave(new MirrorSave());
                        _executeSave.DoSaveStrategy(_jsonTask.FindTask(tasknumber));
                    }
                    else if (_jsonTask.FindTask(tasknumber).GetValue(1).ToString() == "Differential")
                    {
                        _executeSave = new ExecuteSave(new DifferentialSave());
                        _executeSave.DoSaveStrategy(_jsonTask.FindTask(tasknumber));
                    }

                    Continue();

                }
            }
            else
            {
                _view.NoTasks();
                Continue();
            }

        }

        //Method to execute all saved tasks 
        public void ExecuteAllTasks()
        {
            if (File.Exists("Task.json"))
            {
                int NumTask = _jsonTask.ListAllTasks().GetLength(0);

                for (int tasknumber = 0; tasknumber < NumTask; tasknumber++)
                {
                    if (_jsonTask.FindTask(tasknumber).GetValue(1).ToString() == "Mirror")
                    {
                        _executeSave = new ExecuteSave(new MirrorSave());
                        _executeSave.DoSaveStrategy(_jsonTask.FindTask(tasknumber));
                    }
                    else if (_jsonTask.FindTask(tasknumber).GetValue(1).ToString() == "Differential")
                    {
                        _executeSave = new ExecuteSave(new DifferentialSave());
                        _executeSave.DoSaveStrategy(_jsonTask.FindTask(tasknumber));
                    }

                }
                Continue();
            }
            else
            {
                _view.NoTasks();
                Continue();
            }


        }
        //Method to read the user choice in the first menu
        public void ReadMenu()
        {
            choice = _view.Menu();

            switch (choice)
            {
                case "1":

                    CreatTask();
                    break;
                case "2":

                    ExecuteTask();
                    break;
                case "3":
                    ;
                    ExecuteAllTasks();
                    break;
                case "4":
                    DeleteTask();
                    break;
                case "5":
                    break;
                case "6":
                    _view.ChangeViewStrategy();
                    Process.Start("EasySave.exe");
                    break;
                default:
                    Console.Clear();
                    _view.UncorrectChoice();
                    _view.Menu();
                    break;
            }

        }
        //Method to continue the program
        public void Continue()
        {
            string choice = _view.ContinuChoice();

            if (choice == "Y")
            {
                ReadMenu();
            }
            else if (choice == "N") { }

            else
            {
                _view.UncorrectChoice();
                Continue();
            }

        }

    }

}

