using LabBook.Commons;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.SemiProduct.Converters
{
    public class VocToNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double voc = 0d;
            string num = value.ToString();

            if (!string.IsNullOrEmpty(num))
            {
                _ = double.TryParse(num, out voc);
            }

            string result;
            switch (voc)
            {
                case (double)VocError.NoRecipe:
                    result = "Brak rec.";
                    break;
                case (double)VocError.NoMaterialVOC:
                    result = "Brak VOC sur.";
                    break;
                case (double)VocError.NoSemiproduct:
                    result = "Brak polprod.";
                    break;
                default:
                    result = voc.ToString() + " %";
                    break;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
