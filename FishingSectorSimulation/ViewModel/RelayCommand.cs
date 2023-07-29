using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FishingSectorSimulation.ViewModel
{
    public class RelayCommand : ICommand
    {
        Action<object> _executeMethod;
        Func<object, bool> _canexecuteMethod;

        public RelayCommand(Action<object> executeMethod, Func<object, bool> canexecuteMethod)
        {
            _canexecuteMethod = canexecuteMethod;
            _executeMethod = executeMethod;

        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object? parameter)
        {
            if (_canexecuteMethod != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Execute(object? parameter)
        {
            _executeMethod(parameter);
        }
    }
}