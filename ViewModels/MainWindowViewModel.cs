using EmployeeDirectoryMVVM.Views;
using System.ComponentModel;

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
            
            CurrentView = new HomeView();
        }
    }
}
