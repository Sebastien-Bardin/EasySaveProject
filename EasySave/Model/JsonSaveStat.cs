using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.Model
{
    class SaveStat
    {
        //Variables creation and setting
        public string source;
        public string destination;
        public string currentfiletocopy;
        public int filecount = 0;
        public int remainingfiles;
        public long totalsize = 0;
        public long remainingsize = 0;
        public int time = 0;
        public void RefreshSaveStat(SaveStat saveStat)
        {
            //Creating Json object
            LogPattern save = new LogPattern() { LastUpdate = DateTime.Now.ToString("dd/mm/yy HH:mm"), FilesNumber = saveStat.filecount, FilesSize = saveStat.totalsize, FilesRemaining = saveStat.remainingfiles, SizeRemaining = saveStat.remainingsize, LastWork = saveStat.currentfiletocopy, Progression = 100 - ((saveStat.remainingsize + 1 / (saveStat.totalsize + 1)) * 100) + "%", Duration = saveStat.time };
            //Serializing the object to fit the Json file

            string jsonSerializedObj = JsonConvert.SerializeObject(save, Formatting.Indented);
            //Writing at the end of Json file without deleting is content
            File.WriteAllText("SaveStat.json", jsonSerializedObj);
        }
    }




}

