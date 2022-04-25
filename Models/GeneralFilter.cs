using System.ComponentModel;
using static EmployeeDirectoryMVVM.Models.Enums;

namespace EmployeeDirectoryMVVM.Models
{
    public class GeneralFilter : INotifyPropertyChanged
    {
        private string _name;
        private int _count;
        private GeneralFilterCategories _category;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChange("Name"); }
        }
        public int Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChange("Count"); }
        }
        public GeneralFilterCategories Category { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
