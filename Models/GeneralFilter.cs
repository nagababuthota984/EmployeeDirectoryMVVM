using System.ComponentModel;
using static EmployeeDirectoryMVVM.Models.Enums;

namespace EmployeeDirectoryMVVM.Models
{
    public class GeneralFilter 
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public GeneralFilterCategories Category { get; set; }
    }
}
