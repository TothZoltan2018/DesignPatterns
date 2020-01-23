using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace _15CommandWpfPelda
{
    /// <summary>
    /// Az uzleti logika. Algoritmusok, a funkciok megvalositasa
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        //hogy ne kelljen nullvizsgalatot vegezni minden egyes alkalommal, ezert feloltjuk egy ures delgate-tel
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //WPF-es konvencio szerint keszitunk egy fgv-t, aminek a neve ugyanaz, mint az esemenye (de ele rakjuk, hogy "On")
        private void OnPropertyChanged(string propertyName)
        {
            //ezzel valik egyuttmukodove a View-val, azaz ertesiti, ha valami megvaltozik
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private CancellationTokenSource cts = null;

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

        public async void Start()
        {
            //A Task megallitasahoz
            cts = new CancellationTokenSource();

            //Task letrehozasa
            var task = new Task(() => {
                for (int i = 0; i < 100; i++)
                {
                    //Erkezett megallitasi hivas?
                    if (cts.Token.IsCancellationRequested)
                    {

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
        }

        public void Stop() 
        {
            if (cts == null) return;
            cts.Cancel();
        }


    }
}
