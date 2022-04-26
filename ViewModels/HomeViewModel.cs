using EmployeeDirectoryMVVM.Data;
using EmployeeDirectoryMVVM.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmployeeDirectoryMVVM.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {

        #region Fields
        private string _searchInput;
        private ObservableCollection<Employee> _employees;
        private ObservableCollection<GeneralFilter> _departments;
        private ObservableCollection<GeneralFilter> _jobtitles;
        private Employee _selectedEmployee;
        private GeneralFilter _selectedDept;
        private GeneralFilter _selectedJobTitle;
        private ObservableCollection<Employee> _filteredEmployees;
        #endregion
        #region Commands
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        #endregion
        #region Properties
        public string SearchInput
        {
            get { return _searchInput; }
            set { _searchInput = value; FilterEmployeesBySearch(value); NotifyPropertyChanged("SearchInput"); }
        }

        public Employee SelectedEmployee 
        {
            get { return _selectedEmployee; } 
            set { _selectedEmployee = value; NotifyPropertyChanged("SelectedEmployee"); }
        }
        public GeneralFilter SelectedJobTitle
        {
            get { return _selectedJobTitle; }
            set { _selectedJobTitle = value; FilterEmployeesByJobTitle(value); NotifyPropertyChanged("SelectedJobTitle"); }
        }
        public GeneralFilter SelectedDept 
        {
            get { return _selectedDept; }
            set { _selectedDept = value; FilterEmployeesByDepartment(value); NotifyPropertyChanged("SelectedDept"); }
        }
        public ObservableCollection<Employee> FilteredEmployees
        {
            get { return _filteredEmployees; }
            set { _filteredEmployees = value; NotifyPropertyChanged("FilteredEmployees"); }
        }
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; NotifyPropertyChanged("Employees"); }
        }
        public ObservableCollection<GeneralFilter> Departments
        {
            get { return _departments; }
            set { _departments = value;  NotifyPropertyChanged("Departments"); }
        }
        public ObservableCollection<GeneralFilter> JobTitles
        {
            get { return _jobtitles; }
            set { _jobtitles = value; NotifyPropertyChanged("JobTitles"); }
        }

        #endregion
        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion
        public HomeViewModel()
        {
            Employees = new ObservableCollection<Employee>(EmployeeData.Employees);
            FilteredEmployees = Employees;
            Departments = new ObservableCollection<GeneralFilter>(EmployeeData.Departments);
            JobTitles = new ObservableCollection<GeneralFilter>(EmployeeData.JobTitles);
            DeleteCommand = new CommandBase(OnDelete);
            EditCommand = new CommandBase(OnEdit);
        }
        public void OnDelete()
        {
            MessageBox.Show("yayyyy! clicked delete");
            Employees.Remove(SelectedEmployee);
        }
        public void OnEdit()
        {
            MessageBox.Show("edit edit");

        }
        private void FilterEmployeesByJobTitle(GeneralFilter value)
        {
            FilteredEmployees = new ObservableCollection<Employee>(Employees.Where(emp => emp.JobTitle.Equals(value.Name, StringComparison.OrdinalIgnoreCase)).ToList());
        }
        private void FilterEmployeesByDepartment(GeneralFilter value)
        {
            FilteredEmployees = new ObservableCollection<Employee>(Employees.Where(emp => emp.Department.Equals(value.Name, StringComparison.OrdinalIgnoreCase)).ToList());

        }
        private void FilterEmployeesBySearch(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                FilteredEmployees = new ObservableCollection<Employee>(Employees.Where(emp => emp.PreferredName.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList());
            else FilteredEmployees = Employees;
        }
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
