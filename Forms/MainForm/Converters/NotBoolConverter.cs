using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.MainForm.Converters
{
    public class NotBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return !System.Convert.ToBoolean(value);
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return !System.Convert.ToBoolean(value);
            else
                return false;
        }
    }
}
