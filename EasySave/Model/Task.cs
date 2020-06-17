using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasySave.Model
{
    //Task pattern
    class Task
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string DType { get; set; }
    }

    class Tasks
    {
        public List<Task> AllTasks { get; set; }
    }

}
