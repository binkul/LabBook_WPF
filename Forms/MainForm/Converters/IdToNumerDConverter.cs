using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.MainForm.Converters
{
    public class IdToNumerDConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "D-" + value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
