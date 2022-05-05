using EmployeeDirectoryMVVM.Models;
using EmployeeDirectoryMVVM.Views;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace EmployeeDirectoryMVVM.ViewModels
{
    public class EmployeeDetailsViewModel : ViewModelBase
    {
        #region Fields
        private Employee _employee;
        #endregion
        #region Properties
        public ICommand CancelCommand { get; set; }
        public Employee EmployeeContext 
        { 
            get { return _employee; } 
            set { _employee = value; OnPropertyChange(nameof(EmployeeContext)); }
        }
        #endregion
        
        public EmployeeDetailsViewModel(Employee selectedEmployee)
        {
            EmployeeContext = selectedEmployee;
            CancelCommand = new CommandBase(OnCancel);
        }

        private void OnCancel()
        {
            MainWindowViewModel.CurrentView = new HomeView();
        }
    }
}
