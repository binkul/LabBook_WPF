using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace LabBook.Forms.Composition.Converters
{
    public class StartEndToBackColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool rowActive = System.Convert.ToBoolean(values[0]);
            int operation = System.Convert.ToInt32(values[1]);

            if (!rowActive && operation > 1)
                return new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xF7, 0x00));
            else if (!rowActive && operation == 1)
                return Brushes.White;
            else
                return new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x78, 0xD7));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
