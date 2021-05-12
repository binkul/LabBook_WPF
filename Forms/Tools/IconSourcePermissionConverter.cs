using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace LabBook.Forms.Tools
{
    public class IconSourcePermissionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var del = System.Convert.ToBoolean(values[0]);
            //var pic1 = new BitmapImage();
            //pic1.UriSource = new Uri("/Img/delete.png", UriKind.Relative);
            //var pic2 = new BitmapImage();
            //pic1.UriSource = new Uri("/Img/lock.png", UriKind.Relative);

            if (del)
                return values[2];
            else
                return values[3];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
