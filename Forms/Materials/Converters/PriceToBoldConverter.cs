using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LabBook.Forms.Materials.Converters
{
    public class PriceToBoldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var density = 0d;
            var num = value.ToString();

            if (!string.IsNullOrEmpty(num))
                Double.TryParse(num, out density);

            return density > 0 ? FontWeights.Normal : FontWeights.Bold;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
