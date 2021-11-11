using LabBook.Forms.Composition.ModelView;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace LabBook.Forms.Composition.Converters
{
    public class MassRoundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double number = System.Convert.ToDouble(values[0]);
            Accuracy accuracy = values[1] == DependencyProperty.UnsetValue ? Accuracy.Round : (Accuracy)values[1];

            if (accuracy == Accuracy.OneZero)
                return number.ToString("F1", CultureInfo.CurrentCulture);
            else if (accuracy == Accuracy.TwoZero)
                return number.ToString("F2", CultureInfo.CurrentCulture);
            else if (accuracy == Accuracy.ThreeZero)
                return number.ToString("F3", CultureInfo.CurrentCulture);
            else if (accuracy == Accuracy.FourZero)
                return number.ToString("F4", CultureInfo.CurrentCulture);
            else
                return GetRoundFormat(number);
        }

        private string GetRoundFormat(double number)
        {
            if (number <= 1)
                return number.ToString("F4", CultureInfo.CurrentCulture);
            else if (number <= 10 && number > 1)
                return number.ToString("F3", CultureInfo.CurrentCulture);
            else if (number <= 100 && number > 10)
                return number.ToString("F2", CultureInfo.CurrentCulture);
            else
                return number.ToString("F1", CultureInfo.CurrentCulture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] ret = new object[1];
            if (string.IsNullOrEmpty(value.ToString()))
            {
                ret[0] = 0d;
                return ret;
            }
            else if (!double.TryParse(value.ToString(), out _))
            {
                _ = MessageBox.Show("Wprowadzona wartość nie jest liczbą! Popraw wartość.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                ret[0] = 0d;
                return ret;
            }
            else
            {
                ret[0] = System.Convert.ToDouble(value, CultureInfo.CurrentCulture);
                return ret;
            }
        }
    }
}
