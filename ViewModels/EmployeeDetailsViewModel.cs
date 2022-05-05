using EmployeeDirectoryMVVM.Models;
using System;
using System.ComponentModel;

namespace EmployeeDirectoryMVVM.ViewModels
{
    public class EmployeeDetailsViewModel : ViewModelBase
    {
        #region Fields
        private Employee _employee;
        private string _okButtonContent;
        #endregion
        #region Properties
       
        public string OkButtonContent
        {
            get { return _okButtonContent; }
            set { _okButtonContent = value; OnPropertyChange(nameof(OkButtonContent)); }
        }
        public Employee EmployeeContext 
        { 
            get { return _employee; } 
            set { _employee = value; OnPropertyChange(nameof(EmployeeContext)); }
        }
        #endregion

        public EmployeeDetailsViewModel() : base("EmployeeDetailsViewModel")
        {
        }

    }
}
