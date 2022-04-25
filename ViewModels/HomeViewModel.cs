using EmployeeDirectoryMVVM.Data;
using EmployeeDirectoryMVVM.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace EmployeeDirectoryMVVM.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; NotifyPropertyChanged("Employees"); }
        }
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public HomeViewModel()
        {
            Employees = new ObservableCollection<Employee>
            {
                new Employee("Anthony","Morris","anthonymorris@gmail.com",DateTime.MinValue,"Sharepoint Practice Head","IT Department",2,8464832529,35000,Enums.EmployementType.Permanent)
            };
        }
    }
}
