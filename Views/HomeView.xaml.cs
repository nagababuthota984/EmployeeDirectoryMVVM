using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EmployeeDirectoryMVVM.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>

    public partial class HomeView : UserControl
    {
        
        public HomeView()
        {
            InitializeComponent();
        }

        private void OnHover(object sender, MouseEventArgs e)
        {
            this.Resources["HeadingColor"] = new SolidColorBrush(Colors.Red);
        }

        
    }
}

