using EasySavetest.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace EasySavetest.ViewModel
{

    class View_Model
    {
        public static ManualResetEvent ProcessVerif;
        JsonTask _jsonTask;
        MirrorSave mirrorSave;
        DifferentialSave differentialSave;

        // string choice;
        ExecuteSave _executeSave;

        public View_Model()
        {
            _jsonTask = new JsonTask();
            ProcessVerif = new ManualResetEvent(true);
            mirrorSave = new MirrorSave();
            differentialSave = new DifferentialSave();
        }
        public void InitializeProcess()
        {
            Thread ProcessV = new Thread(new ThreadStart(() => { VerifP(); })) { Name = "ProcessVerifier" };
            ProcessV.Start();
        }

        public void VerifP()
        {
            while (true)
            {
                int ActiveProcessCount = 0;

                foreach (string Soft in _jsonTask.ListConfMetier())
                    if (Process.GetProcessesByName(Soft).Length != 0)
                    {
                        ProcessVerif.Reset();
                        ActiveProcessCount++;
                    }

                if (ActiveProcessCount == 0)
                {
                    ProcessVerif.Set();
                }
                Thread.Sleep(1000);



                //Thread shudown if mainthread is off
                if (Process.GetProcessesByName("EasySavetest").Length == 0)
                {
                    foreach (Process proc in Process.GetProcessesByName("ProcessV"))
                    {
                        proc.Kill();
                    }
                }

            }
        }

        public void CreatTask(Array Parameters)
        {

            string[] TaskTab = new string[5];
            Parameters.CopyTo(TaskTab, 0);
            _jsonTask.CreatJsonTask(TaskTab[0], TaskTab[1], TaskTab[2], TaskTab[3], TaskTab[4]);

        }

        public void DeleteTask(string taskname)
        {

            _jsonTask.DeleteTask(taskname);

        }

        public void ExecuteTask(string work)
        {

            if (File.Exists("Task.json") && _jsonTask.ListAllTasks().Length != 0)
            {

                if (_jsonTask.FindTask(work).Type == "Mirror")
                {
                    _executeSave = new ExecuteSave(mirrorSave);
                    Work parameters = new Work() { informations = _jsonTask.FindTask(work), extentions = _jsonTask.ListConfEXT(), softwares = _jsonTask.ListConfMetier(), priorities = _jsonTask.ListConfPrio() };
                    _executeSave.DoSaveStrategy(parameters, ProcessVerif);
                }
                else if (_jsonTask.FindTask(work).Type == "Differential")
                {
                    _executeSave = new ExecuteSave(differentialSave);
                    Work parameters = new Work() { informations = _jsonTask.FindTask(work), extentions = _jsonTask.ListConfEXT(), softwares = _jsonTask.ListConfMetier(), priorities = _jsonTask.ListConfPrio() };
                    _executeSave.DoSaveStrategy(parameters, ProcessVerif);
                }
            }
            else
            {
                Console.WriteLine("You have no task created");

            }

        }

        public void PlayPauseTask(string nom)
        {
            if (_jsonTask.FindTask(nom).Type == "Mirror")
            {
                mirrorSave.PlayPause(nom);
            }
            else if (_jsonTask.FindTask(nom).Type == "Differential")
            {
                differentialSave.PlayPause(nom);
            }
        }

        public void Canceltask(string name)
        {
            if (_jsonTask.FindTask(name).Type == "Mirror")
            {
                mirrorSave.Cancel(name);
            }
            else if (_jsonTask.FindTask(name).Type == "Differential")
            {
                differentialSave.Cancel(name);
            }
        }

        public void ExecuteAllTasks()
        {
            foreach (string taskname in _jsonTask.ListAllTasks())
            {
                if (_jsonTask.FindTask(taskname).Type == "Mirror")
                {
                    _executeSave = new ExecuteSave(mirrorSave);
                    Work parameters = new Work() { informations = _jsonTask.FindTask(taskname), extentions = _jsonTask.ListConfEXT(), softwares = _jsonTask.ListConfMetier(), priorities = _jsonTask.ListConfPrio() };
                    _executeSave.DoSaveStrategy(parameters, ProcessVerif);
                }
                else if (_jsonTask.FindTask(taskname).Type == "Differential")
                {
                    _executeSave = new ExecuteSave(differentialSave);
                    Work parameters = new Work() { informations = _jsonTask.FindTask(taskname), extentions = _jsonTask.ListConfEXT(), softwares = _jsonTask.ListConfMetier(), priorities = _jsonTask.ListConfPrio() };
                    _executeSave.DoSaveStrategy(parameters, ProcessVerif);
                }

            }
        }

        public Array ListAllTasks()
        {

            return _jsonTask.ListAllTasks();
        }

        public Array ListAllExec()
        {
            return _jsonTask.ListConfEXT();
        }

        public Array ListAllSoft()
        {
            return _jsonTask.ListConfMetier();
        }

        public Array ListAllPrio()
        {
            return _jsonTask.ListConfPrio();
        }

        public void CreatExec(string name)
        {
            _jsonTask.CreatConfigFileEXT(name);
        }

        public void CreatSoft(string name)
        {
            _jsonTask.CreatConfigFileMetier(name);
        }

        public void CreatPrio(string name)
        {
            _jsonTask.CreatConfigFilePrio(name);
        }

        public void DeleteExec(string name)
        {
            _jsonTask.DeleteExt(name);
        }

        public void DeleteSoft(string name)
        {
            _jsonTask.DeleteMetier(name);
        }

        public void DeletePrio(string name)
        {
            _jsonTask.DeletePrio(name);
        }


    }

}


