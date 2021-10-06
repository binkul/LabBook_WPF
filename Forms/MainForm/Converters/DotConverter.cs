using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LabBook.Forms.MainForm.Converters
{
    public class DotConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? value.ToString().Replace(".", ",") : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double tmp = 0d;

            if (string.IsNullOrEmpty(value.ToString()))
            {
                return "0";
            }
            else if (!double.TryParse(value.ToString(), out tmp))
            {
                MessageBox.Show("Wprowadzona wartość nie jest liczbą! Popraw wartość.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return "0";
            }
            else
            {
                return value.ToString().Replace(",", ".");
            }

        }
    }
}
