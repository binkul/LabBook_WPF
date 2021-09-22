using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.Composition.Converters
{
    public class LeftPositionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double start = 2;
            string type = parameter.ToString();
            double colStatus = System.Convert.ToDouble(values[0]);
            double colLP = System.Convert.ToDouble(values[1]);
            double colMaterial = System.Convert.ToDouble(values[2]);
            double colAmountPr = System.Convert.ToDouble(values[3]);
            double colAmountMass = System.Convert.ToDouble(values[4]);
            double colPriceKg = System.Convert.ToDouble(values[5]);
            double colPrice = System.Convert.ToDouble(values[6]);
            double rdAmountWidth = System.Convert.ToDouble(values[7]);
            double colVoc = System.Convert.ToDouble(values[8]);
            double result;

            switch (type)
            {
                case "cmbMaterial":
                    result = start + colStatus + colLP;
                    break;
                case "txtAmount":
                    result = start + colStatus + colLP + colMaterial + 2;
                    break;
                case "txtMass":
                    result = start + colStatus + colLP + colMaterial + colAmountPr;
                    break;
                case "rdAmount":
                    result = start + colStatus + colLP + colMaterial + colAmountPr 
                        + colAmountMass + 10;
                    break;
                case "rdMass":
                    result = start + colStatus + colLP + colMaterial + colAmountPr 
                        + colAmountMass + rdAmountWidth + 10;
                    break;
                case "txtPrice":
                    result = start + colStatus + colLP + colMaterial + colAmountPr
                        + colAmountMass + colPriceKg;
                    break;
                case "txtVoc":
                    result = start + colStatus + colLP + colMaterial + colAmountPr
                        + colAmountMass + colPriceKg + colPrice;
                    break;
                case "txtComment":
                    result = start + colStatus + colLP + colMaterial + colAmountPr
                        + colAmountMass + colPriceKg + colPrice + colVoc;
                    break;
                case "SumText":
                    result = start + colStatus + colLP + colMaterial - 60;
                    break;
                case "MasaText":
                    result = start + colStatus + colLP + colMaterial + colAmountPr - 55;
                    break;
                default:
                    result = 0;
                    break;
            }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
