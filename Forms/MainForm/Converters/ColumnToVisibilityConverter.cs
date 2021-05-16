using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.MainForm.Converters
{
    public class ColumnToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var name = parameter.ToString();
            var columns = value as IDictionary<string, bool>;

            if (columns.ContainsKey(name))
                if (columns[name])
                    return System.Windows.Visibility.Visible;
                else
                    return System.Windows.Visibility.Hidden;
            else
                return System.Windows.Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
