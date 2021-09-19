using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.Composition.Converters
{
    public class PercentRounConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var number = System.Convert.ToDouble(value);

            return number.ToString("F4", CultureInfo.CurrentCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
