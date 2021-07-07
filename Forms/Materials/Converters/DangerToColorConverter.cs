using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LabBook.Forms.Materials.Converters
{
    public class DangerToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Brushes.Black;

            var danger = (bool)value;
            if (danger)
                return Brushes.Red;
            else
                return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
