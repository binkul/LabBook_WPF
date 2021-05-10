using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LabBook.Forms.Tools
{
    public class IgnoreNewItemPlaceholderConverter : IValueConverter
    {
        private const string newItemPlaceholderName = "{NewItemPlaceholder}";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.ToString().Equals(newItemPlaceholderName))
                return null;
            else
                return value;
        }
    }
}
