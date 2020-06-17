using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySavetest.Model
{/* Class for json file used to store file extention to encrypt*/
    class ConfigExtention
    {
        public string ext { get; set; }




    }

    class ListExt
    {
        public List<ConfigExtention> AllExt { get; set; }
    }
}
