using EmployeeDirectoryMVVM.Data;
using EmployeeDirectoryMVVM.Models;
using EmployeeDirectoryMVVM.Utilities;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static EmployeeDirectoryMVVM.Models.Enums;

namespace EmployeeDirectoryMVVM.Views
{
    /// <summary>
    /// Interaction logic for EditEmployeeDetails.xaml
    /// </summary>
    public partial class EmployeeDetailsView : UserControl
    {
        public Employee currentEmployee;
        public EmployeeDetailsView()
        {
            InitializeComponent();
            currentEmployee = null;
        }
        private void OnCancel(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new HomeView();
        }
        public void LoadContentIntoForm(Employee emp)
        {
            if (emp != null)
            {
                fname.Text = emp.FirstName;
                lname.Text = emp.LastName;
                email.Text = emp.Email;
                jobtitle.Text = emp.JobTitle;
                department.Text = emp.Department;
                dob.SelectedDate = emp.Dob;
                salary.Text = emp.Salary.ToString();
                experience.Text = emp.ExperienceInYears.ToString();
                contactNumber.Text = emp.ContactNumber.ToString();
                currentEmployee = emp;
            }
        }
        public void ConvertFormContentToEmployee()
        {
            if (int.TryParse(experience.Text, out int exp) && decimal.TryParse(salary.Text, out decimal sal) && long.TryParse(contactNumber.Text, out long phone) && DateTime.TryParse(dob.SelectedDate.ToString(), out DateTime dateOfBirth))
                currentEmployee = new Employee(fname.Text, lname.Text, email.Text, dateOfBirth, jobtitle.Text, department.Text, exp, phone, sal, EmployementType.Permanent);
        }
        public void UpdateEmployeeDetails(object sender, RoutedEventArgs e)
        {
            ConvertFormContentToEmployee();
            if (currentEmployee != null && FormValidator.IsValidFormData(currentEmployee))
            {
                int index = EmployeeData.Employees.FindIndex(emp => emp.Email.Equals(currentEmployee.Email, StringComparison.OrdinalIgnoreCase));
                if (index > -1)
                {
                    EmployeeData.Employees[index] = currentEmployee;
                    JsonHelper.WriteToJson<Employee>();
                    JsonHelper.WriteToJson<GeneralFilter>();
                    MessageBox.Show("Updated Successfully");
                    Application.Current.MainWindow.Content = new HomeView();
                }
            }
        }
        public void HandleAddEmployee(object sender, RoutedEventArgs e)
        {
            ConvertFormContentToEmployee();
            if (FormValidator.IsValidFormData(currentEmployee))
            {
                EmployeeData.Employees.Add(currentEmployee);
                AddDepartment(currentEmployee.Department);
                AddJobTitle(currentEmployee.JobTitle);
                JsonHelper.WriteToJson<Employee>();
                JsonHelper.WriteToJson<GeneralFilter>();
                MessageBox.Show($"{currentEmployee.FirstName} has been added successfully");
            }
        }
        private void AddJobTitle(string jobTitle)
        {
            if (!string.IsNullOrWhiteSpace(jobTitle))
            {
                GeneralFilter job = EmployeeData.JobTitles.FirstOrDefault(jt => (jt.Category == GeneralFilterCategories.JobTitle && jt.Name.Equals(jobTitle, StringComparison.OrdinalIgnoreCase)));
                if (job != null)
                    job.Count += 1;
                else
                    EmployeeData.JobTitles.Add(new GeneralFilter { Name = jobTitle, Count = 1, Category = GeneralFilterCategories.JobTitle });
            }
        }
        private void AddDepartment(string department)
        {
            if (!string.IsNullOrWhiteSpace(department))
            {
                GeneralFilter dept = EmployeeData.Departments.FirstOrDefault(jt => jt.Name.Equals(department, StringComparison.OrdinalIgnoreCase));
                if (dept != null)
                    dept.Count += 1;
                else
                    EmployeeData.Departments.Add(new GeneralFilter { Name = department, Count = 1, Category = GeneralFilterCategories.Department });
            }
        }
    }
}
