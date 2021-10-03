using LabBook.ADO.Service;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LabBook.Forms.Composition.Converters
{
    public class LevelFrameTypeConverter : IValueConverter
    {
        private const string _pathBottom = "\\Img\\frameBottom.png";
        private const string _pathMiddle = "\\Img\\frameMiddle.png";
        private const string _pathTop = "\\Img\\frameTop.png";
        private static ImageSource _sourceTop = new BitmapImage(new Uri(_pathTop, UriKind.Relative));
        private static ImageSource _sourceMiddle = new BitmapImage(new Uri(_pathMiddle, UriKind.Relative));
        private static ImageSource _sourceBottom = new BitmapImage(new Uri(_pathBottom, UriKind.Relative));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SubRecipeOrdering levelType = (SubRecipeOrdering)value;

            if (levelType == SubRecipeOrdering.top)
                return _sourceTop;
            else if (levelType == SubRecipeOrdering.bottom)
                return _sourceBottom;
            else
                return _sourceMiddle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
