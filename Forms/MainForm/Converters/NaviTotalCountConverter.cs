using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.MainForm.Converters
{
    public class NaviTotalCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string count = value.ToString();
            return "z " + count;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
