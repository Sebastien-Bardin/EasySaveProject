using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasySave.Model
{
    class JsonTask
    {   //Creation of a reuseable string 
        public static string SerializedData;
        //Creation off a Tasks object  
        public static Tasks _Tasks { get; set; }

        public JsonTask()
        {   //implementation of the tasks object
            _Tasks = new Tasks();
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
        public void DeleteTask(int tasknumber)
        {
            _Tasks.AllTasks.Remove(_Tasks.AllTasks[tasknumber]);
            SerializedData = JsonConvert.SerializeObject(_Tasks);
            File.WriteAllText("Task.json", SerializedData);
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
        public Array FindTask(int tasknumber)
        {
            string[] Taskinfo = new string[5];
            SerializedData = File.ReadAllText("Task.json");
            _Tasks = JsonConvert.DeserializeObject<Tasks>(SerializedData);
            Taskinfo[0] = _Tasks.AllTasks[tasknumber].Name;
            Taskinfo[1] = _Tasks.AllTasks[tasknumber].Type;
            Taskinfo[2] = _Tasks.AllTasks[tasknumber].Source;
            Taskinfo[3] = _Tasks.AllTasks[tasknumber].Destination;
            Taskinfo[4] = _Tasks.AllTasks[tasknumber].DType;

            return Taskinfo;

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


    }
}
