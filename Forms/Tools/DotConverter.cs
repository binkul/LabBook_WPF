using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.Tools
{
    public class DotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Replace(".", ",");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Replace(",", ".");
        }
    }
}
