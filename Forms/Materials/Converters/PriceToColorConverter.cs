using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LabBook.Forms.Materials.Converters
{
    public class PriceToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var price = 0d;
            var num = value.ToString();

            if (!string.IsNullOrEmpty(num))
                _ = double.TryParse(num, out price);

            return price > 0 ? Brushes.White : Brushes.Yellow;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
