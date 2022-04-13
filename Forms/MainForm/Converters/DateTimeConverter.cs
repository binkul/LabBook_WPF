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
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return string.Format("{0:dd-MM-yyyy}", date);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string date = value.ToString();
            return DateTime.TryParse(date, out DateTime result) ? result : value;
        }
    }
