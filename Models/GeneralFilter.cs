using System.ComponentModel;
using static EmployeeDirectoryMVVM.Models.Enums;

namespace EmployeeDirectoryMVVM.Models
{
    public class GeneralFilter : INotifyPropertyChanged
    {
        private bool _visible=true;
        public string Name { get; set; }
        public int Count { get; set; }
        public GeneralFilterCategories Category { get; set; }

        public bool IsVisible
        {
            get { return _visible; }
            set
            {
                _visible = value; PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(IsVisible))); 
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
