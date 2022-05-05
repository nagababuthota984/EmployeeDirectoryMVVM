using EmployeeDirectoryMVVM.Models;
using EmployeeDirectoryMVVM.ViewModels;
using System.Windows.Controls;

namespace EmployeeDirectoryMVVM.Views
{
    /// <summary>
    /// Interaction logic for EditEmployeeDetails.xaml
    /// </summary>
    public partial class EmployeeDetailsView : UserControl
    {
        public EmployeeDetailsView(string heading, Employee empContext)
        {
            InitializeComponent();
            if (empContext != null)
            {
                DataContext = new EmployeeDetailsViewModel(empContext);
                Heading.Text = heading;
                SubmitBtn.Content = "Save Changes"; 
            }
            else
            {
                DataContext = new EmployeeDetailsViewModel(new Employee());
                Heading.Text = heading;
                SubmitBtn.Content = "Add Employee";
            }
        }
        
       
    }
}
