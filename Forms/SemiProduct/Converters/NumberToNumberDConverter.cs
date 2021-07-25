using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.SemiProduct.Converters
{
    public class NumberToNumberDConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string number = value.ToString();
            return "D-" + number;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
