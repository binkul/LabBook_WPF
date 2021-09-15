using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.Composition.Converters
{
    public class MassRoundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double number = System.Convert.ToDouble(value);

            if (number <= 1)
                return number.ToString("F4", CultureInfo.CurrentCulture);
            else if (number <= 10 && number > 1)
                return number.ToString("F3", CultureInfo.CurrentCulture);
            else if (number <= 100 && number > 10)
                return number.ToString("F2", CultureInfo.CurrentCulture);
            else
                return number.ToString("F1", CultureInfo.CurrentCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
