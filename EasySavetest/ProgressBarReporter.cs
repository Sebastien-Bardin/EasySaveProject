using EasySavetest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySavetest
{
    public class ProgressBarReporter : IObserver<TaskProgressBar>
    {
        public string progress;
        //unsubscriber object 
        private IDisposable unsubscriber;

        //subscribe method calling the observable's one and storing the return unsubscriber object
        public void Subscribe(IObservable<TaskProgressBar> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }
        //calling this method when the observer wanted to stop observation
        public void OnCompleted()
        {
            this.Unsubscribe();
        }
        //Dispose the observer
        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }
        //not implemented yet
        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }
        //Displaying changes
        public void OnNext(TaskProgressBar value)
        {
           Console.WriteLine("Task Progress " + value.progress + "%");
        }
    }
}
