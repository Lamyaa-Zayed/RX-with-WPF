using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RxWithWPF
{
   public class ViewModel : INotifyPropertyChanged
    {


        public ViewModel()
        {
            TimerValue = 0;
            StoppingObservable = new Subject<Unit>();
            stopTimerCommand = new RelayCommand(StopTimer);
        }



        private long timerValue;

        public long TimerValue
        {
            get { return timerValue; }
            set { timerValue = value; OnPropertyChanged("TimerValue"); }
        }



        public Subject<Unit> StoppingObservable { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        #region Rx config

        public void Timer(TimeSpan timeSpan, SynchronizationContext synchronizationContext)
        {

            try
            {
                //ObserveOn(synchronizationContext)
                var intervalObservable = Observable.Interval(timeSpan).TakeUntil(StoppingObservable);

                intervalObservable.Subscribe(interval =>
                {
                    TimerValue = interval;


                });


            }
            catch (Exception ex)
            {

                throw;
            }


        }

        #endregion


        #region Command







        private RelayCommand stopTimerCommand;



        public RelayCommand StopTimedCommand
        {
            get { return stopTimerCommand; }

        }

        public void StopTimer(object parameter)
        {



            StoppingObservable.OnNext(Unit.Default);
            //MessageBox.Show("Timer");

        }




        #endregion



    }
}
