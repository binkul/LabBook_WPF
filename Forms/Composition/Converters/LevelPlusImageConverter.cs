using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LabBook.Forms.Composition.Converters
{
    public class LevelPlusImageConverter : IMultiValueConverter
    {
        private const string _pathArrowWithPlus = "\\Img\\arrow-with_plus.png";
        private const string _pathArrowWithMinus = "\\Img\\arrow-with_minus.png";
        private const string _pathEmpty = "\\Img\\empty.png";
        private const string _pathPlus = "\\Img\\plus.png";
        private const string _pathMinus = "\\Img\\minus.png";
        private const string _pathArrow = "\\Img\\arrow-right.png";
        private static ImageSource _sourceArrowWithPlus = new BitmapImage(new Uri(_pathArrowWithPlus, UriKind.Relative));
        private static ImageSource _sourceArrowWithMinus = new BitmapImage(new Uri(_pathArrowWithMinus, UriKind.Relative));
        private static ImageSource _sourceEmpty = new BitmapImage(new Uri(_pathEmpty, UriKind.Relative));
        private static ImageSource _sourcePlus = new BitmapImage(new Uri(_pathPlus, UriKind.Relative));
        private static ImageSource _sourceMinus = new BitmapImage(new Uri(_pathMinus, UriKind.Relative));
        private static ImageSource _sourceArrow = new BitmapImage(new Uri(_pathArrow, UriKind.Relative));

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(x => x == DependencyProperty.UnsetValue))
                return _pathEmpty;

            bool isSemi = (bool)values[0];
            string status = values[1].ToString();
            bool isSelected = (bool)values[2];

            if (!isSemi && isSelected)
                return _sourceArrow;
            else if (isSemi && status == "close" && !isSelected)
                return _sourcePlus;
            else if (isSemi && status == "open" && !isSelected)
                return _sourceMinus;
            else if (isSemi && status == "close" && isSelected)
                return _sourceArrowWithPlus;
            else if (isSemi && status == "open" && isSelected)
                return _sourceArrowWithMinus;
            else
                return _sourceEmpty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
