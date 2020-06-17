using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySave.Display;
using EasySave.ViewModel;

namespace EasySave
{
    class Program
    {
        static void Main(string[] args)
        {
            View view = new View();
            view.ReadViewStrategy();
            View_Model operation = new View_Model(view);

        }
    }
}
