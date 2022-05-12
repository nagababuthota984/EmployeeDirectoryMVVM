using EmployeeDirectoryMVVM.Data;
using EmployeeDirectoryMVVM.Models;
using EmployeeDirectoryMVVM.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static EmployeeDirectoryMVVM.Models.Enums;

namespace EmployeeDirectoryMVVM.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        #region Fields
        private string _searchInput;
        private string _filterInput;
        private string _viewMoreBtnContent;
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
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand ViewMoreCommand { get; set; }
        #endregion
        #region Properties

        public string SearchInput
        {
            get { return _searchInput; }
            set { _searchInput = value; FilterEmployeesBySearch(value); OnPropertyChange(nameof(SearchInput)); }
        }
        public string FilterInput
        {
            get { return _filterInput; }
            set { _filterInput = value; FilterEmployeesBySearch(SearchInput); OnPropertyChange(nameof(FilterInput)); }
        }
        public string[] FilterCategories { get; set; }
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChange(nameof(SelectedEmployee)); }
        }
        public GeneralFilter SelectedJobTitle
        {
            get { return _selectedJobTitle; }
            set { _selectedJobTitle = value; FilterEmployeesByJobTitle(value); OnPropertyChange(nameof(SelectedJobTitle)); }
        }
        public GeneralFilter SelectedDept
        {
            get { return _selectedDept; }
            set { _selectedDept = value; FilterEmployeesByDepartment(value); OnPropertyChange(nameof(SelectedDept)); }
        }
        public string ViewMoreBtnContent
        {
            get { return _viewMoreBtnContent; }
            set { _viewMoreBtnContent = value; OnPropertyChange(nameof(ViewMoreBtnContent)); }
        }
        public ObservableCollection<Employee> FilteredEmployees
        {
            get { return _filteredEmployees; }
            set { _filteredEmployees = value; OnPropertyChange(nameof(FilteredEmployees)); }
        }
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; OnPropertyChange(nameof(Employees)); }
        }
        public ObservableCollection<GeneralFilter> Departments
        {
            get { return _departments; }
            set { _departments = value; OnPropertyChange(nameof(Departments)); }
        }
        public ObservableCollection<GeneralFilter> JobTitles
        {
            get { return _jobtitles; }
            set { _jobtitles = value; OnPropertyChange(nameof(JobTitles)); }
        }

        #endregion
        public HomeViewModel()
        {
            Employees = new ObservableCollection<Employee>(EmployeeData.Employees);
            FilteredEmployees = Employees;
            Departments = new ObservableCollection<GeneralFilter>(EmployeeData.Departments);
            DeleteCommand = new CommandBase(OnDelete);
            EditCommand = new CommandBase(OnEditEmp);
            AddEmployeeCommand = new CommandBase(OnAddEmp);
            ViewMoreCommand = new CommandBase(OnViewMore);
            ViewMoreBtnContent = "view more";
            FilterCategories = Enum.GetNames(typeof(FilterCategories));
            FilterInput = FilterCategories[0];
            LoadJobTitles();

        }

        private void LoadJobTitles()
        {
            if (EmployeeData.JobTitles.Count >= 6)
                EmployeeData.JobTitles.Skip(6).ToList().ForEach(job=> job.IsVisible = false);
            JobTitles = new(EmployeeData.JobTitles);
        }

        private void OnViewMore()
        {
            if (ViewMoreBtnContent.Equals("view more", StringComparison.OrdinalIgnoreCase))
            {
                JobTitles.Skip(6).ToList().ForEach(job=> job.IsVisible = true);
                ViewMoreBtnContent = "view less";
            }
            else
            {
                JobTitles.Skip(6).ToList().ForEach(job => job.IsVisible = false);
                ViewMoreBtnContent = "view more";
            }
        }

        private void OnAddEmp()
        {
            Employee emp = new();
            MainWindowViewModel.CurrentView = new AddEditEmpView()
            {
                DataContext = new AddEditEmpViewModel("New Employee Details", "Add Employee", emp)
            };
        }
        private void OnEditEmp()
        {

            MainWindowViewModel.CurrentView = new AddEditEmpView()
            {
                DataContext = new AddEditEmpViewModel("Edit Employee Details", "Update", SelectedEmployee)
            };
        }
        public void OnDelete()
        {

            if (MessageBoxResult.Yes == MessageBox.Show($"Are you sure you want to delete {SelectedEmployee.PreferredName}?", "Delete Employee", MessageBoxButton.YesNo))
                Employees.Remove(SelectedEmployee);
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
                FilteredEmployees = new ObservableCollection<Employee>(Employees.Where(emp =>
                                             (FilterInput.Equals("Name") && emp.PreferredName.Contains(value, StringComparison.OrdinalIgnoreCase))
                                             || (FilterInput.Equals("ContactNumber") && emp.ContactNumber.ToString().Contains(value, StringComparison.OrdinalIgnoreCase))
                                             || (FilterInput.Equals("Salary") && emp.Salary.ToString().Contains(value, StringComparison.OrdinalIgnoreCase))
                                             || (FilterInput.Equals("Experience") && emp.ExperienceInYears.ToString().Contains(value, StringComparison.OrdinalIgnoreCase))).ToList());
            else
                FilteredEmployees = Employees;
        }




    }
}
