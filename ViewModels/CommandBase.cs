using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeDirectoryMVVM.ViewModels
{
    public class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        Action _executeMethod;
        Func<bool> _canExecuteMethod;

        public CommandBase(Action executeMethod)
        {
            _executeMethod = executeMethod;
        }
        public CommandBase(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }
        public bool CanExecute(object? parameter)
        {
            if (_canExecuteMethod != null) return _canExecuteMethod();
            else if (_executeMethod != null) return true;
            return false;
        }

        public void Execute(object? parameter)
        {
            if (_executeMethod != null)
                _executeMethod();
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
