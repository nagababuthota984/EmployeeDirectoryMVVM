using EmployeeDirectoryMVVM.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EmployeeDirectoryMVVM.Data
{
    public class JsonHelper
    {
        public static string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public static void WriteToJson<T>()
        {
            string data;
            if (typeof(T) == typeof(Employee))
            {
                data = JsonConvert.SerializeObject(EmployeeData.Employees, Formatting.Indented);
                File.WriteAllText($"{projectDirectory}\\Data\\Employee.json", data);
            }
            else if (typeof(T) == typeof(GeneralFilter))
            {
                data = JsonConvert.SerializeObject(EmployeeData.Departments, Formatting.Indented);
                string jobData = JsonConvert.SerializeObject(EmployeeData.JobTitles, Formatting.Indented);
                data = $"{data.Substring(0, data.Length - 1)},{jobData.Substring(1)}";
                File.WriteAllText($"{projectDirectory}\\Data\\GeneralFilters.json", data);
            }
        }   
        public static string ReadFromJson(string typeOfData)
        {
            return File.ReadAllText($"{projectDirectory}\\Data\\{typeOfData}.json");
        }
        public static void InitEmployeeData()
        {
            string data = ReadFromJson("Employee");
            if (!string.IsNullOrEmpty(data))
                EmployeeData.Employees = JsonConvert.DeserializeObject<List<Employee>>(data).Where(emp => emp.Status == Status.Existing).ToList();
        }
        public static void InitGeneralFiltersData()
        {
            string data = ReadFromJson("GeneralFilters");
            if (!string.IsNullOrEmpty(data))
            {
                EmployeeData.JobTitles = JsonConvert.DeserializeObject<List<GeneralFilter>>(data).Where(filter => filter.Category == GeneralFilterCategories.JobTitle).ToList();
                EmployeeData.Departments = JsonConvert.DeserializeObject<List<GeneralFilter>>(data).Where(filter => filter.Category == GeneralFilterCategories.Department).ToList();
            }
        }

    }
}
