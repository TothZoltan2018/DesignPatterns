using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace _15CommandWpfPelda
{
    /// <summary>
    /// Az uzleti logika. Algoritmusok, a funkciok megvalositasa
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            // csak akkor indithato, ha meg nem fut
            startCommand = new SajatParancs((akarmiparameter) => Start(), (param) => !IsWorking);
            stopCommand = new SajatParancs((akarmiparameter) => Stop(), (param) => IsWorking);
        }

        //hogy ne kelljen nullvizsgalatot vegezni minden egyes alkalommal, ezert feloltjuk egy ures delgate-tel
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //WPF-es konvencio szerint keszitunk egy fgv-t, aminek a neve ugyanaz, mint az esemenye (de ele rakjuk, hogy "On")
        private void OnPropertyChanged(string propertyName) 
        {
            //ezzel valik egyuttmukodove a View-val, azaz ertesiti, ha valami megvaltozik
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private CancellationTokenSource cts = null;
        private Task task = null;

        //A ProgressValue-nak ertesiteni kell a feluletet:
        //backing field
        private int progressValue = 0;        

        public int ProgressValue 
        { 
            get { return progressValue; }

            set
            {
                //Ha nem valtozott, akkor nem csinal semmit
                if (value == ProgressValue) { return; }
                //egyebkent megadjuk, hogy mi valtozott
                progressValue = value;
                OnPropertyChanged(nameof(ProgressValue));               
            }
        }

        private ICommand startCommand;
        public ICommand StartCommand { get {return startCommand; } }

        private ICommand stopCommand;
        public ICommand StopCommand { get { return stopCommand; } }

        public bool IsWorking 
        {
            get
            {
                if (task == null) return false;
                switch (task.Status)
                {
                    case TaskStatus.Created:
                    case TaskStatus.RanToCompletion:
                    case TaskStatus.Canceled:
                    case TaskStatus.Faulted:
                        //Nem fut a Task, de letrejott:
                        return false;
                    case TaskStatus.WaitingForActivation:
                    case TaskStatus.WaitingToRun:                        
                    case TaskStatus.Running:                        
                    case TaskStatus.WaitingForChildrenToComplete:
                        //fut a Task
                        return true;
                    default:
                        //Ismeretlen statusz, nincs a program erre felkeszitve
                        throw new ArgumentOutOfRangeException(string.Format($"task.Status ismeretlen: {task.Status}"));
                }
            }                
        }


        public async void Start()
        {
            //A Task megallitasahoz
            cts = new CancellationTokenSource();

            //Task letrehozasa
            task = new Task(() => {
                for (int i = 0; i < 100; i++)
                {
                    //Erkezett megallitasi hivas?
                    if (cts.Token.IsCancellationRequested)
                    {
                        ProgressValue = 0;
                    }
                    cts.Token.ThrowIfCancellationRequested();

                    ProgressValue = i;
                    Thread.Sleep(30);
                }
            }, cts.Token);

            task.Start();

            try
            {
                await Task.WhenAll(task);
            }
            catch (OperationCanceledException)
            {
                //Ide akkor jovunk, ha a taskot megszakitottak
            }
            finally
            {//Ellenorizze a gombok allapotat, hiszen ez egy masik szalon dolgozik, es a feluletet karbantarto szal ertesitve legyen
                //CommandManager.RequerySuggested event-et general
                CommandManager.InvalidateRequerySuggested();
            
            }
            //ha vege a tasknak, nullazzuk ki
            task = null;
        }

        public void Stop() 
        {
            if (cts == null) return;
            cts.Cancel();
        }


    }
}
