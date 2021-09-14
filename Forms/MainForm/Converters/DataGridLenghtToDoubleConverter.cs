using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace LabBook.Forms.MainForm.Converters
{
    public class DataGridLenghtToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double number = (double)value;
            return new DataGridLength(number);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var number = (DataGridLength)value;
                return number.Value;
            }
            catch
            {
                return 0d;
            }
        }
    }
}
