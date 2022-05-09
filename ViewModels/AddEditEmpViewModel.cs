using EmployeeDirectoryMVVM.Data;
using EmployeeDirectoryMVVM.Models;
using EmployeeDirectoryMVVM.Views;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmployeeDirectoryMVVM.ViewModels
{
    public class AddEditEmpViewModel : ViewModelBase
    {
        #region Fields
        private Employee _employee;
        private string _headingText;
        private string _okBtnContent;
        #endregion
        #region Commands
        public ICommand CancelCommand { get; set; }
        public ICommand OkCommand { get; set; }
        #endregion
        #region Properties
        public Employee SelectedEmployee
        {
            get { return _employee; }
            set { _employee = value; OnPropertyChange(nameof(SelectedEmployee)); }
        }
        public string HeadingText
        {
            get { return _headingText; }
            set { _headingText = value; OnPropertyChange(nameof(HeadingText)); }
        }
        public string OkBtnContent
        {
            get { return _okBtnContent; }
            set { _okBtnContent = value; OnPropertyChange(nameof(OkBtnContent)); }
        }
        #endregion

        public AddEditEmpViewModel(string headingText,string okBtnContent,Employee selectedEmployee)
        {
            _employee = selectedEmployee;
            HeadingText = headingText;
            OkBtnContent = okBtnContent;
            CancelCommand = new CommandBase(OnCancel);
            SetOkBtnCommand(OkBtnContent);
        }

        private void SetOkBtnCommand(string okBtnContent)
        {
            if (!string.IsNullOrWhiteSpace(okBtnContent))
            {
                if (OkBtnContent.Equals("Add Employee", StringComparison.OrdinalIgnoreCase))
                    OkCommand = new CommandBase(OnAddEmp,CanAddEmp);
                else
                    OkCommand = new CommandBase(OnUpdate,CanUpdate);
            }
        }

        private bool CanUpdate()
        {
            //validation goes here.
            return true;
        }

        private bool CanAddEmp()
        {
            //validation goes here
            return true;
        }

        private void OnUpdate()
        {
            EmployeeData.Employees.Remove(EmployeeData.Employees.FirstOrDefault(emp => emp.Id.Equals(SelectedEmployee.Id, StringComparison.OrdinalIgnoreCase)));
            EmployeeData.Employees.Add(SelectedEmployee);
            JsonHelper.WriteToJson<Employee>();
            MessageBox.Show($"Details have been updated succesfully.", "Employee Updated");
            OnCancel();
        }
        private void OnAddEmp()
        {
            EmployeeData.Employees.Add(new(SelectedEmployee));
            JsonHelper.WriteToJson<Employee>();
            MessageBox.Show($"Employee has been added succesfully.", "Employee Added");
            OnCancel();
        }

        private void OnCancel()
        {
            MainWindowViewModel.CurrentView = new HomeView();
        }
    }
}
