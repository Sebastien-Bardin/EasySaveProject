using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySavetest.Model
{
    class JsonLog
    {
        public static SaveStat log;
        public string LastUpdate { get; set; }
        public string TaskName { get; set; }
        public string FileSource { get; set; }
        public string FileDestination { get; set; }
        public long FileSize { get; set; }
        public TimeSpan CryptTime { get; set; }
        public void CreateLog(Work work, SaveStat saveStat)
        {
            log = new SaveStat();
            WriteLog(work, saveStat);
        }



        void WriteLog(Work work, SaveStat saveStat)
        {
            string Filename = DateTime.Now.ToString("MM.dd.yyyy") + "JsonLog.json";
            if (File.Exists(Filename))
            {
                //Creating Json object
                JsonLog save1 = new JsonLog() { LastUpdate = DateTime.Now.ToString("dd/mm/yy HH:mm"), FileSource = saveStat.source, FileDestination = saveStat.destination, FileSize = saveStat.totalsize, TaskName = work.informations.Name, CryptTime = saveStat.CryptTime };

                //Writing Json object in the file
                string jsonSerializedObj1 = JsonConvert.SerializeObject(save1, Formatting.Indented);
                System.IO.File.AppendAllText(Filename, jsonSerializedObj1);
            }
            else
            {
                //Creating Json object
                JsonLog save = new JsonLog() { LastUpdate = DateTime.Now.ToString("dd/mm/yy HH:mm"), FileSource = saveStat.source, FileDestination = saveStat.destination, FileSize = saveStat.totalsize, TaskName = work.informations.Name, CryptTime = saveStat.CryptTime };

                //Writing Json object in the file 
                string jsonSerializedObj = JsonConvert.SerializeObject(save, Formatting.Indented);
                File.WriteAllText(Filename, jsonSerializedObj);
            }
        }

    }
}
