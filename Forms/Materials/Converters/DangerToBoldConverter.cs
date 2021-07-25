using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LabBook.Forms.Materials.Converters
{
    public class DangerToBoldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return FontWeights.Normal;

            bool danger = (bool)value;
            if (danger)
                return FontWeights.Bold;
            else
                return FontVariants.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
