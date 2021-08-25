using LabBook.ADO.Service;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.SemiProduct.Converters
{
    public class PriceWithPlnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double price = 0d;
            string result = "";
            string num = value.ToString();

            if (!string.IsNullOrEmpty(num))
            {
                _ = double.TryParse(num, out price);
            }

            switch (price)
            {
                case (double)PriceError.NoRecipe:
                    result = "Brak rec.";
                    break;
                case (double)PriceError.NoMaterialPrice:
                    result = "Brak ceny sur.";
                    break;
                case (double)PriceError.NoCurrency:
                    result = "Brak waluty";
                    break;
                case (double)PriceError.NoSemiproduct:
                    result = "Brak polprod.";
                    break;
                case 0:
                    result = "-- Brak --";
                    break;
                default:
                    result = price.ToString() + " zł";
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
