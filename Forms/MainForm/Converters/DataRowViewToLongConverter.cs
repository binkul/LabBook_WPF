using System;
using System.Data;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.MainForm.Converters
{
    public class DataRowViewToLongConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return 0;
            else
            {
                DataRowView row = (DataRowView)value;
                long id = System.Convert.ToInt64(row.Row["id"].ToString());
                return id;
            }
        }
    }
}
