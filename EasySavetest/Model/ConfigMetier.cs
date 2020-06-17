using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySavetest.Model
{
    /* Class for json file to store software to watch*/
    class ConfigMetier
    {

        public string metier { get; set; }


    }

        

    class ListMetier
    {
      public List<ConfigMetier> AllMetier { get; set; }
    }
    
}
