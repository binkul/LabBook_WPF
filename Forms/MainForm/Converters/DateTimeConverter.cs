using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LabBook.Forms.MainForm.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        private const string _format = "dd-MM-yyyy";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;

            return date.ToString(_format);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.ParseExact((string)value, _format, culture);
        }
    }
}
