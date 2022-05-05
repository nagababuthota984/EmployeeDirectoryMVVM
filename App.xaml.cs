using EmployeeDirectoryMVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace EmployeeDirectoryMVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow window = new();
            window.Show();
            base.OnStartup(e);
        }
    }
}
