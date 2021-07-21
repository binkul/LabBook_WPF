using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.Materials.Converters
{
    public class BoolToNotCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool tmp = System.Convert.ToBoolean(value);
            if (tmp)
                return System.Windows.Visibility.Collapsed;
            else
                return System.Windows.Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
