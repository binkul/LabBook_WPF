using LabBook.Security;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LabBook.Forms.Tools
{
    public class IconSourcePermissionConverter : IMultiValueConverter
    {
        private const string _pathDel = "\\Img\\delete.png";
        private const string _pathLock = "\\Img\\lock.png";
        private const string _pathUnLock = "\\Img\\unlock.png";
        private const string _pathOk = "\\Img\\ok.png";
        private static ImageSource _sourceDel = new BitmapImage(new Uri(_pathDel, UriKind.Relative));
        private static ImageSource _sourceLock = new BitmapImage(new Uri(_pathLock, UriKind.Relative));
        private static ImageSource _sourceUnLock = new BitmapImage(new Uri(_pathUnLock, UriKind.Relative));
        private static ImageSource _sourceOk = new BitmapImage(new Uri(_pathOk, UriKind.Relative));

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var del = System.Convert.ToBoolean(values[0]);
            var id = System.Convert.ToInt64(values[1]);

            if (del)
                return _sourceDel;
            else if (id == UserSingleton.Id)
                return _sourceOk;
            else if (id != UserSingleton.Id && UserSingleton.Permission.Equals("admin"))
                return _sourceUnLock;
            else
                return _sourceLock;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
