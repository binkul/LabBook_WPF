using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace LabBook.Forms.Composition.Converters
{
    public class StartEndBorderColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(x => x == DependencyProperty.UnsetValue))
                return Brushes.Black;

            bool rowActive = System.Convert.ToBoolean(values[0]);
            int operation = System.Convert.ToInt32(values[1]);

            if (!rowActive && operation > 1)
                return Brushes.Blue;
            else if (rowActive && operation > 1)
                return Brushes.Red;
            else
                return Brushes.Black;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
