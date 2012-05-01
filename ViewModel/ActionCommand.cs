using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ViewModel
{
    
    public class ActionCommand : ICommand
    {
        public Action         Action    { get; set; }
        public Action<object> ActionP   { get; set; }

        public event EventHandler CanExecuteChanged;

        public ActionCommand()
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (Action != null)
                Action.Invoke();
            if (ActionP != null)
                ActionP.Invoke(parameter);
        }
    }
}
