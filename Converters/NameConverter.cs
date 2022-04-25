using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace EmployeeDirectoryMVVM.Converters
{
    public class NameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Count() > 1)
                return $"{values[0]} {values[1]}";
            return "NA";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
