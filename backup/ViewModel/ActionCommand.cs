using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ViewModel
{
    public class ActionCommand : ICommand
    {

        public Action Action { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (Action != null)
                Action.Invoke();
        }
    }
}
