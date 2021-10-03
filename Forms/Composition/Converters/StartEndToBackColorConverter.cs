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
    public class StartEndToBackColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if (values.Any(x => x == DependencyProperty.UnsetValue))
                return Brushes.White;

            bool rowActive = System.Convert.ToBoolean(values[0]);
            int operation = System.Convert.ToInt32(values[1]);
            bool cellContent = false;
            double cellValue = 1d;
            
            if (parameter != null)
            {
                cellContent = System.Convert.ToBoolean(parameter);
                cellValue = System.Convert.ToDouble(values[2]);
            }
            
            if (rowActive)
                return new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x78, 0xD7)); // blue

            if (cellContent && cellValue <= 0)
                return new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x33, 0x33)); // red

            if (operation > 1)
                return new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xF7, 0x00)); // yellow
            else
                return Brushes.White;                                               // white
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
