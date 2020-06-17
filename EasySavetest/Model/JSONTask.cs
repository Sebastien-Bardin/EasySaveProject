using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasySavetest.Model
{
    class JsonTask
    {   //Cration of a reuseable string 
        public static string SerializedData;
        //Creation off a Tasks object  
        public static Tasks _Tasks { get; set; }
        public static ListExt listExt { get; set; }
        public static ListMetier listMetier { get; set; }
        public static ListPrio listPrio { get; set; }

       

        public JsonTask()
        {   //implementation of the tasks object
            _Tasks = new Tasks();
            listExt = new ListExt();
            listMetier = new ListMetier();
            listPrio = new ListPrio();
        }

        //Method use to creat Task in the Json file
        public void CreatJsonTask(string TaskName, string Savetype, string source, string destination, string DType)
        {

            ReadJsonTask();
            _Tasks.AllTasks.Add(new Task() { Name = TaskName, Type = Savetype, Source = source, Destination = destination, DType = DType });
            SerializedData = JsonConvert.SerializeObject(_Tasks);
            File.WriteAllText("Task.json", SerializedData);

        }

        //Method to delete a selected task
        public void DeleteTask(string taskname)
        {
            // _Tasks.AllTasks.Remove(_Tasks.AllTasks[tasknumber]);
            foreach (Task task in _Tasks.AllTasks)
            {
                if (task.Name == taskname)
                {

                    _Tasks.AllTasks.Remove(task);
                    SerializedData = JsonConvert.SerializeObject(_Tasks);
                    File.WriteAllText("Task.json", SerializedData);
                    break;

                }
            }
           
        }
        //Method to read the Json task file and check he exists
        public void ReadJsonTask()
        {
            if (!File.Exists("Task.json"))
            {
                _Tasks = new Tasks() { AllTasks = new List<Task>() };
                SerializedData = JsonConvert.SerializeObject(_Tasks);
                File.WriteAllText("Task.json", SerializedData);
            }

            SerializedData = File.ReadAllText("Task.json");
            _Tasks = JsonConvert.DeserializeObject<Tasks>(SerializedData);


        }
        //Method to read the Json Task file and take the corresponding task parameters 
        public Task FindTask(string taskname)
        {
            Task taskinfo = new Task();
            SerializedData = File.ReadAllText("Task.json");
            _Tasks = JsonConvert.DeserializeObject<Tasks>(SerializedData);
            foreach (Task task in _Tasks.AllTasks)
            {
                if (task.Name==taskname)
                {
                    taskinfo = task;
                }
            }

            return taskinfo;

        }
        //Method to list all existing task
        public Array ListAllTasks()
        {
            int NumTask;
            SerializedData = File.ReadAllText("Task.json");
            _Tasks = JsonConvert.DeserializeObject<Tasks>(SerializedData);

            for (NumTask = 0; NumTask < _Tasks.AllTasks.Count; NumTask++) { }

            string[] AllTasks = new string[NumTask];

            for (int i = 0; i < NumTask; i++)
            {
                AllTasks[i] = _Tasks.AllTasks[i].Name;
            }

            return AllTasks;
        }

        
        //method adding extention to encrypt in the configuration files 
        public void CreatConfigFileEXT(string ext)
        {
            if (!File.Exists("ConfigExtention.json"))
            {
                CreatFile("Ext");
            }
            else if (File.Exists("ConfigExtention.json") && ListConfEXT().Length == 0)
            {
                File.Delete("ConfigExtention.json");
                CreatFile("Ext");

            }

            listExt.AllExt.Add(new ConfigExtention() { ext = ext });
            SerializedData = JsonConvert.SerializeObject(listExt);
            File.WriteAllText("ConfigExtention.json", SerializedData);

        }
        //method creating the specified file for the software configuration
        public void CreatFile(string file)
        {
            if (file == "metier")
            {
                listMetier = new ListMetier() { AllMetier = new List<ConfigMetier>() };
                SerializedData = JsonConvert.SerializeObject(listMetier);
                File.WriteAllText("ConfigMetier.json", SerializedData);
            }
            else if (file == "Ext")
            {
                listExt = new ListExt() { AllExt = new List<ConfigExtention>() };
                SerializedData = JsonConvert.SerializeObject(listExt);
                File.WriteAllText("ConfigExtention.json", SerializedData);

            }
            else if (file == "Prio")
            {
                listPrio = new ListPrio() { AllPrio = new List<ConfigPrio>() };
                SerializedData = JsonConvert.SerializeObject(listPrio);
                File.WriteAllText("ConfigPriority.json", SerializedData);
            }
        }

        //method adding software to watch in the configuration files 

        public void CreatConfigFileMetier(string metier)
        {
            if (!File.Exists("ConfigMetier.json"))
            {
                CreatFile("metier");
            }
            else if (File.Exists("ConfigMetier.json") && ListConfMetier().Length == 0)
            {
                File.Delete("ConfigMetier.json");
                CreatFile("metier");
            }

            listMetier.AllMetier.Add(new ConfigMetier() { metier = metier });
            SerializedData = JsonConvert.SerializeObject(listMetier);
            File.WriteAllText("ConfigMetier.json", SerializedData);
        }

        //method adding an extention priority in the configuration files 

        public void CreatConfigFilePrio(string prio)
        {
            if (!File.Exists("ConfigPriority.json"))
            {
                CreatFile("Prio");
            }
            else if (File.Exists("ConfigPriority.json") && ListConfEXT().Length == 0)
            {
                File.Delete("ConfigPriority.json");
                CreatFile("Prio");

            }

            listPrio.AllPrio.Add(new ConfigPrio() { Priority = prio });
            SerializedData = JsonConvert.SerializeObject(listPrio);
            File.WriteAllText("ConfigPriority.json", SerializedData);

        }
        //List all stored priorities 
        public Array ListConfPrio()
        {
            if (!File.Exists("ConfigPriority.json"))
            {
                CreatFile("Prio");
            }
            int NumTask;
            SerializedData = File.ReadAllText("ConfigPriority.json");
            listPrio = JsonConvert.DeserializeObject<ListPrio>(SerializedData);

            for (NumTask = 0; NumTask < listPrio.AllPrio.Count; NumTask++) { }

            string[] AllPrio = new string[NumTask];

            for (int i = 0; i < NumTask; i++)
            {
                AllPrio[i] = listPrio.AllPrio[i].Priority;
            }


            return AllPrio;
        }
        //List all crypted extention
        public Array ListConfEXT()
        {
            if (!File.Exists("ConfigExtention.json"))
            {
                CreatFile("Ext");
            }
                int NumTask;
                SerializedData = File.ReadAllText("ConfigExtention.json");
                listExt = JsonConvert.DeserializeObject<ListExt>(SerializedData);

                for (NumTask = 0; NumTask < listExt.AllExt.Count; NumTask++) { }

                string[] AllExt = new string[NumTask];

                for (int i = 0; i < NumTask; i++)
                {
                    AllExt[i] = listExt.AllExt[i].ext;
                }
            

            return AllExt;
        }

        //List all watched softwares
        public Array ListConfMetier()
        {
            if (!File.Exists("ConfigMetier.json"))
            {
                CreatFile("metier");
            }

            int NumTask;
            SerializedData = File.ReadAllText("ConfigMetier.json");
            listMetier = JsonConvert.DeserializeObject<ListMetier>(SerializedData);

            for (NumTask = 0; NumTask < listMetier.AllMetier.Count; NumTask++) { }

            string[] AllMetier = new string[NumTask];

            for (int i = 0; i < NumTask; i++)
            {
                AllMetier[i] = listMetier.AllMetier[i].metier;
            }

            return AllMetier;
        }
        //Delete a specified crypted extention 
        public void DeleteExt(string ExtName)
        {
            foreach (ConfigExtention extention  in listExt.AllExt)
            {
                if (extention.ext == ExtName)
                {
                    listExt.AllExt.Remove(extention);
                    SerializedData = JsonConvert.SerializeObject(listExt);
                    File.WriteAllText("ConfigExtention.json", SerializedData);
                    break;

                }
            }

            
        }
        //Delete the specified watched software
        public void DeleteMetier(string MetierName)
        {
            foreach (ConfigMetier metier in listMetier.AllMetier)
            {
                if (metier.metier == MetierName)
                {
                    listMetier.AllMetier.Remove(metier);
                    SerializedData = JsonConvert.SerializeObject(listMetier);
                    File.WriteAllText("ConfigMetier.json", SerializedData);
                    break;

                }
            }
           
        }
        //Delete the specified extention priority
        public void DeletePrio(string PrioName)
        {
            foreach (ConfigPrio prio in listPrio.AllPrio)
            {
                if (prio.Priority == PrioName)
                {
                    listPrio.AllPrio.Remove(prio);
                    SerializedData = JsonConvert.SerializeObject(listPrio);
                    File.WriteAllText("ConfigPriority.json", SerializedData);
                    break;

                }
            }

        }

    }
}
