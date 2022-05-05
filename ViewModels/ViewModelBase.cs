using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectoryMVVM.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ViewModelBase()
        {
           
        }
        public ViewModelBase(string name)
        {
            Name = name;
        }
    }
}
