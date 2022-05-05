using System;
using System.Windows.Input;

namespace EmployeeDirectoryMVVM.ViewModels
{
    public class CommandBase : ICommand
    {
        //Action - A delegate which stores a method(which may take 0 or more params & not return any value) 
        Action _executeMethod;
        //Func<out return type> - a delegate which stores a method(which may take 0 or more params & return a value) 
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

        public event EventHandler? CanExecuteChanged;

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
    }
}
