using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.Model
{
    class LogPattern
    {
        //Defining fields for SaveStat Json file
        public string LastUpdate { get; set; }
        public int FilesNumber { get; set; }
        public long FilesSize { get; set; }
        public int FilesRemaining { get; set; }
        public long SizeRemaining { get; set; }
        public string LastWork { get; set; }
        public string Progression { get; set; }
        public int Duration { get; set; }

    }
}
