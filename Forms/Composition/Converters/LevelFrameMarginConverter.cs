using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LabBook.Forms.Composition.Converters
{
    public class LevelFrameMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int level = (int)value;

            switch (level)
            {
                case 1:
                    return new Thickness(9, 0, 0, 0);
                case 2:
                    return new Thickness(18, 0, 0, 0);
                case 3:
                    return new Thickness(27, 0, 0, 0);
                case 4:
                    return new Thickness(36, 0, 0, 0);
                default:
                    return new Thickness(0, 0, 0, 0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
