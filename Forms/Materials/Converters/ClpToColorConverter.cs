using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace LabBook.Forms.Materials.Converters
{
    public class ClpToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            char start = value.ToString().ToUpper().FirstOrDefault();
            switch(start)
            {
                case 'H':
                    return Brushes.Red;
                case 'E':
                    return Brushes.Green;
                default:
                    return Brushes.Blue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
