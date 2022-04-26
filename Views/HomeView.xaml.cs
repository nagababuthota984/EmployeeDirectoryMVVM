using EmployeeDirectoryMVVM.Data;
using EmployeeDirectoryMVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static EmployeeDirectoryMVVM.Models.Enums;

namespace EmployeeDirectoryMVVM.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>

    public partial class HomeView : UserControl
    {
        public List<Employee> filteredData = EmployeeData.Employees;
        public static EmployeeDetailsView EmpDetailsView;
        public Employee SelectedEmployee = null;


        public HomeView()
        {
            InitializeComponent();
            //EmployeeCards.ItemsSource = filteredData;
            Filter.ItemsSource = Enum.GetNames(typeof(FilterCategories));
            Filter.SelectedValue = Filter.Items[0];
            EmpDetailsView = new EmployeeDetailsView();
            SelectedEmployee = new Employee();
        }
        public void FiltersClickHandler(object sender, SelectionChangedEventArgs e)
        {
            var lbox = sender as ListBox;
            //if (lbox.Name.Equals("DepartmentsDiv", StringComparison.OrdinalIgnoreCase))
            //{
            //    var dept = (GeneralFilter)lbox.SelectedItem;
            //    EmployeeCards.ItemsSource = GetEmployeesByDept(dept.Name);
            //}
            //else
            //{
            //    var jobTitle = (GeneralFilter)lbox.SelectedItem;
            //    EmployeeCards.ItemsSource = GetEmployeesByJobTitle(jobTitle.Name);
            //}
        }
        private List<Employee> GetEmployeesByJobTitle(string filterValue)
        {
            return filteredData = EmployeeData.Employees.Where(emp => emp.JobTitle.Equals(filterValue, StringComparison.OrdinalIgnoreCase)).ToList(); ;
        }
        private List<Employee> GetEmployeesByDept(string filterValue)
        {
            return filteredData = EmployeeData.Employees.Where(emp => emp.Department.Equals(filterValue, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        private void HandleSearchKeyUp(object sender, KeyEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
                EmployeeCards.ItemsSource = GetSearchFilteredData(tb.Text, Filter.Text);
        }
        private List<Employee> GetSearchFilteredData(string textToCompare, string filterCategory)
        {
            if (!string.IsNullOrWhiteSpace(textToCompare))
                return filteredData.Where(emp => (filterCategory.Contains("Name") && emp.PreferredName.Contains(textToCompare, StringComparison.OrdinalIgnoreCase)) || (filterCategory.Contains("Email") && emp.Email.Contains(textToCompare, StringComparison.OrdinalIgnoreCase)) || (filterCategory.Contains("ContactNumber") && emp.ContactNumber.ToString().Contains(textToCompare))).ToList();
            else
                return EmployeeData.Employees;
        }
        private void OpenAddEmployeeForm(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Collapsed;
            EmpDetailsView.Heading.Text = "New Employee";
            EmpDetailsView.SubmitBtn.Content = "Add Employee";
            EmpDetailsView.email.IsEnabled = true;
            EmpDetailsView.SubmitBtn.Click += EmpDetailsView.HandleAddEmployee;  //disposal?
            UserControlSpace.Visibility = Visibility.Visible;
            UserControlSpace.Content = EmpDetailsView;
        }
        private void EditEmployeeDetails(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Collapsed;
            SelectedEmployee = (Employee)EmployeeCards.SelectedItem;
            EmpDetailsView.LoadContentIntoForm(SelectedEmployee);
            EmpDetailsView.Heading.Text = "Edit Employee";
            EmpDetailsView.SubmitBtn.Content = "Update Details";
            EmpDetailsView.SubmitBtn.Click += EmpDetailsView.UpdateEmployeeDetails;
            UserControlSpace.Visibility = Visibility.Visible;
            UserControlSpace.Content = EmpDetailsView;
        }
        private void FilterSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0].ToString().Split(":").Length > 1)
            {
                EmployeeCards.ItemsSource = GetSearchFilteredData(Search.Text, e.AddedItems[0].ToString().Split(":")[1].Trim());
            }
        }
        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
            SelectedEmployee = (Employee)EmployeeCards.SelectedItem;
            if (MessageBoxResult.Yes == MessageBox.Show($"Are you sure you want to delete {SelectedEmployee.PreferredName}", "Delete Employee?", MessageBoxButton.YesNo))
            {
                //delete the employee here
                Employee emp = EmployeeData.Employees.FirstOrDefault(emp => emp.Email.Equals(SelectedEmployee.Email, StringComparison.OrdinalIgnoreCase));
                emp.Status = Status.Resigned;
                GeneralFilter job = EmployeeData.JobTitles.FirstOrDefault(j => j.Name.Equals(emp.JobTitle, StringComparison.OrdinalIgnoreCase));
                job.Count -= 1;
                GeneralFilter dept = EmployeeData.Departments.FirstOrDefault(j => j.Name.Equals(emp.Department, StringComparison.OrdinalIgnoreCase));
                dept.Count -= 1;
                JsonHelper.WriteToJson<GeneralFilter>();
                JsonHelper.WriteToJson<Employee>();
            }
        }
    }
}

