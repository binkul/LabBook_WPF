using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.Composition.Converters
{
    public class OrderingToStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string type = values[0].ToString();
            double order = System.Convert.ToDouble(values[1]);

            if (type == "0")
                return order.ToString();
            else
                return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
