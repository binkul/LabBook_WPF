using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.Composition.Converters
{
    public class LevelFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int level = (int)value;

            if (level == 0)
                return 12.0;
            else
                return 11.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
