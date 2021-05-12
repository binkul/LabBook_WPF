using LabBook.Security;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.Tools
{
    public class LockVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var id = System.Convert.ToInt64(values[0]);
            var user = (User)values[1];

            if (id == user.Id)
                return System.Windows.Visibility.Hidden;
            else
                return System.Windows.Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
