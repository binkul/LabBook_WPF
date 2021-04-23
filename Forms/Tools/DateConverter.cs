using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.Tools
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                DateTime test = (DateTime)value;
                string date = test.ToString("dd-MM-yyyy");
                return (date);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
