using EmployeeDirectoryMVVM.Models;
using System.Collections.Generic;
using System.IO;

namespace EmployeeDirectoryMVVM.Data
{
    public class EmployeeData
    {
        public static List<Employee> Employees = new();
        public static List<GeneralFilter> Departments = new();
        public static List<GeneralFilter> JobTitles = new();
    }
}
