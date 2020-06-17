using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySavetest.Model
{/*
   Class for json file used to store file priority 
     */
    class ConfigPrio
    {
        public string Priority { get; set; }


    }



    class ListPrio
    {
        public List<ConfigPrio> AllPrio { get; set; }
    }

}
