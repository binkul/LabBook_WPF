using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.SemiProduct.Converters
{
    public class PriceWithPlnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double density = 0d;
            string num = value.ToString();

            if (!string.IsNullOrEmpty(num))
            {
                _ = double.TryParse(num, out density);
            }

            return density > 0 ? density.ToString() + " zł" : "-- Brak --";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
