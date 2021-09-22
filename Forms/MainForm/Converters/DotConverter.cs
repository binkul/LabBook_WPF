using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace LabBook.Forms.MainForm.Converters
{
    public class DotConverter : IValueConverter
    {
        private readonly IDictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Replace(".", ",");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double tmp = 0d;

            if (string.IsNullOrEmpty(value.ToString()))
            {
                return "0";
            }
            else if (!double.TryParse(value.ToString(), out tmp))
            {
                MessageBox.Show("Wprowadzona wartość nie jest liczbą! Popraw wartość.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return "0";
            }
            else
            {
                return value.ToString().Replace(",", ".");
            }

        }
    }
}
