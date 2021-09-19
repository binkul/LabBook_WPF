using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LabBook.Forms.Composition.Converters
{
    public class SumPercentToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double sum = System.Convert.ToDouble(value);

            if (sum > 100)
                return Brushes.Red;
            else if (sum < 100)
                return Brushes.Blue;
            else
                return Brushes.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
