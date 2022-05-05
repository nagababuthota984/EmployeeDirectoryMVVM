using EmployeeDirectoryMVVM.Models;
using EmployeeDirectoryMVVM.Views;
using System.Windows.Input;

namespace EmployeeDirectoryMVVM.ViewModels
{
    public class AddAndEditEmpViewModel : ViewModelBase
    {
        #region Fields
        private Employee _employee;
        private string _headingText;
        private string _okBtnContent;
        #endregion
        #region Commands
        public ICommand CancelCommand { get; set; }
        #endregion
        #region Properties
        public Employee SelectedEmployee
        {
            get { return _employee; }
            set { _employee = value; OnPropertyChange(nameof(SelectedEmployee)); }
        }
        public string HeadingText
        {
            get { return _headingText; }
            set { _headingText = value; OnPropertyChange(nameof(HeadingText)); }
        }
        public string OkBtnContent
        {
            get { return _okBtnContent; }
            set { _okBtnContent = value; OnPropertyChange(nameof(OkBtnContent)); }
        }
        #endregion

        public AddAndEditEmpViewModel(string headingText,string okBtnContent,Employee selectedEmployee)
        {
            _employee = selectedEmployee;
            HeadingText = headingText;
            OkBtnContent = okBtnContent;
            CancelCommand = new CommandBase(OnCancel);
        }
        private void OnCancel()
        {
            MainWindowViewModel.CurrentView = new HomeView();
        }
    }
}
