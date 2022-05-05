using System.ComponentModel;
using System.Windows.Input;

namespace EmployeeDirectoryMVVM.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public ICommand NavigateToEditEmpView { get; set; }
        
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { _currentViewModel = value; OnPropertyChange(nameof(CurrentViewModel)); }
        }

        

        public MainWindowViewModel()
        {
            //CurrentViewModel = new EmployeeDetailsViewModel();
            NavigateToEditEmpView = new CommandBase(NavigateToEditEmp);
        }

        private void NavigateToEditEmp()
        {
            CurrentViewModel = new EmployeeDetailsViewModel();
        }
        
    }
}
