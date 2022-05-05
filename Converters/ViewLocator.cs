using EmployeeDirectoryMVVM.ViewModels;
using EmployeeDirectoryMVVM.Views;

using System;
using System.Globalization;
using System.Windows.Data;

namespace EmployeeDirectoryMVVM.Converters
{
    public class ViewLocator : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ViewModelBase vm)
            {
                if (vm.Name.Equals("homeviewmodel", StringComparison.OrdinalIgnoreCase))
                    return new HomeView();
                else
                    return new EmployeeDetailsView();
            }
            return new HomeView();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
