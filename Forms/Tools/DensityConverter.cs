using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LabBook.Forms.Tools
{
    public class DensityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var density = 0d;
            var num = value.ToString();

            if (!string.IsNullOrEmpty(num))
                Double.TryParse(num, out density);

            return density > 0 ? density.ToString() : "Brak";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Replace(",", ".");
        }
    }
}
