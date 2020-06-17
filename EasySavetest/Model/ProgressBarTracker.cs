using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySavetest.Model
{
    class ProgressBarTracker : IObservable<TaskProgressBar>
    {
        public ProgressBarTracker()
        {//creation of the observer list
            observers = new List<IObserver<TaskProgressBar>>();
        }
        //observer list
        private List<IObserver<TaskProgressBar>> observers;

        //subscribe method to add an observer to the list and return an unsuscriber object to him
        public IDisposable Subscribe(IObserver<TaskProgressBar> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }

        //Unsubscriber object given to every subscribed observer allowing each observer to use dispose method and to know the observer list
        private class Unsubscriber : IDisposable
        {   //This list is given to each observer
            private List<IObserver<TaskProgressBar>> _observers;
            private IObserver<TaskProgressBar> _observer;

            public Unsubscriber(List<IObserver<TaskProgressBar>> observers, IObserver<TaskProgressBar> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
        //notifying observer
        public void TrackProgress(TaskProgressBar progress)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(progress);
            }
        }
        //ending transmission
        public void EndTransmission()
        {
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted();

            observers.Clear();
        }
    }
}
