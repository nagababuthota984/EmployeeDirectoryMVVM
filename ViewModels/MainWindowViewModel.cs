using EmployeeDirectoryMVVM.Data;
using EmployeeDirectoryMVVM.Views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace EmployeeDirectoryMVVM.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static event PropertyChangedEventHandler StaticPropertyChanged;
        private static object _currentView;
        public static object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; StaticPropertyChanged?.Invoke(null,new PropertyChangedEventArgs(nameof(CurrentView))); }
        }
        public MainWindowViewModel()
        {
            JsonHelper.InitGeneralFiltersData();
            JsonHelper.InitEmployeeData();
            CurrentView = new HomeView();
        }
    }
}
