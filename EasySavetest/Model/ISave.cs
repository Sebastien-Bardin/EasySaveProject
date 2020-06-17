using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace EasySavetest.Model
{/*Strategy interface implementing the save method
    */
    interface ISave
    {
        void save(Work work,ManualResetEvent ProcessVerif);


    }
}
