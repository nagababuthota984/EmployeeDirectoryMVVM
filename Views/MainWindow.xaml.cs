using EmployeeDirectoryMVVM.Data;
using EmployeeDirectoryMVVM.Views;
using System.Windows;

namespace EmployeeDirectoryMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            JsonHelper.InitGeneralFiltersData();
            JsonHelper.InitEmployeeData();
            MainContent.Content = new HomeView();

        }

    }

}
