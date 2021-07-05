using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.MainForm.Converters
{
    public class DensityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var density = 0d;
            var num = value.ToString();

            if (!string.IsNullOrEmpty(num))
                Double.TryParse(num, out density);

            return density > 0 ? density.ToString() : "-- Brak --";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Replace(",", ".");
        }
    }
}
