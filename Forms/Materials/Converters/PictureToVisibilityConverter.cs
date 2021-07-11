using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.Materials.Converters
{
    public class PictureToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tmp = System.Convert.ToBoolean(value);
            if (tmp)
                return System.Windows.Visibility.Visible;
            else
                return System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
