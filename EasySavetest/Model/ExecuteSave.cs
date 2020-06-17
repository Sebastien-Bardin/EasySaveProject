using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasySavetest.Model
{
    /*Context for the strategy pattern
     * Implement seter for the chosen strategy
     * and execute the save methode of the corresponding strategy
     */
    class ExecuteSave
    {
        public ISave _save;

        public ExecuteSave(ISave save)
        {
            this._save = save;
        }

        public void SetSaveStrategy(ISave save)
        {
            this._save = save;
        }

        public void DoSaveStrategy(Work work, ManualResetEvent ProcessVerif)
        {
            this._save.save(work, ProcessVerif);
        }
    }
}
