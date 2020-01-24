using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _15CommandWpfPelda
{
    public class SajatParancs : ICommand //ICommand: a parancsminta a WPF-ben
    {
        private Action<object> execute = null;
        private Func<object, bool> canexecute = null;

        /// <summary>
        /// Ha a parancs mindig hivhato, akkor nem kell a masodik parameter.
        /// </summary>
        /// <param name="execute">Egy Action, amit hivni kell, ha a parancsot vegrhajtjak.</param>
        public SajatParancs(Action<object> execute)
        {
            this.execute = execute;
        }

        /// <summary>
        /// Ha a parancs nem mindig hivhato, akkor kell a masodik parameter, ami egy fgv, es megmondja, hogy hivhato -e a parancs.
        /// </summary>
        /// <param name="execute">Egy Action, amit hivni kell, ha a parancsot vegrhajtjak.</param>
        /// <param name="canexecute">Egy Func, ami megmondja, hogy hivhato -e a parancs.</param>
        public SajatParancs(Action<object> execute, Func<object, bool> canexecute) : this(execute)
        {
            this.canexecute = canexecute;
        }

        /// <summary>
        /// Egy esemeny, ami azt jelzi, hogy megvaltoztathato -e a parancshivas allapota
        /// Ha van CanExecute, akkor kotelezo implemetalni.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            //A SajatParancs osztalypeldany letrejottekor a .NET futtatokornyezet feliratkozik a CanExecuteChanged esemenyre.
            //Ez a feliratkozas bekerul a kozponti CommandManagerbe.
            add { CommandManager.RequerySuggested += value;  }
            remove { CommandManager.RequerySuggested -= value; }

        }

        /// <summary>
        /// Ezt hivja a keretrendszer annak eldontesere, hogy a parancs hivhato -e.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return canexecute == null ? true : canexecute(parameter);
        }

        /// <summary>
        /// Ez a parancs hivasa.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
