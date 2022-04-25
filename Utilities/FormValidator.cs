using EmployeeDirectoryMVVM.Data;
using EmployeeDirectoryMVVM.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace EmployeeDirectoryMVVM.Utilities
{
    public class FormValidator
    {
        public static bool IsValidFormData(Employee emp)
        {

            if (IsValidString(emp.FirstName) && IsValidString(emp.LastName) && IsValidString(emp.JobTitle) && IsValidString(emp.Department))
            {
                if (IsValidEmailFormat(emp.Email) || EmployeeData.Employees.Any(emp => emp.Email.Equals(emp.Email, StringComparison.OrdinalIgnoreCase)))
                {
                    if (DateTime.Today.Year - emp.Dob.Year > 20)
                        return true;
                    else
                        MessageBox.Show("Invalid Dob");
                }
                MessageBox.Show("invalid Mail");
            }
            return false;
        }
        public static bool IsValidEmailFormat(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }
        public static bool IsValidString(string text)
        {
            return !(string.IsNullOrEmpty(text) || text.Any(char.IsDigit));
        }

    }
}
