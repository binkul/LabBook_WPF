using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace LabBook.Forms.Composition.Converters
{
    public class StartEndToThicknesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool rowActive = System.Convert.ToBoolean(values[0]);
            int operation = System.Convert.ToInt32(values[1]);
            string type = parameter.ToString();

            switch (type)
            {
                case "left":
                    if (operation == 2)
                        return new Thickness(2, 2, 0, 0);
                    else if (operation == 3)
                        return new Thickness(2, 0, 0, 0);
                    else if (operation == 4)
                        return new Thickness(2, 0, 0, 2);
                    else
                        return new Thickness(0, 0, 0, 0);
                case "mid":
                    if (operation == 2)
                        return new Thickness(0, 2, 0, 0);
                    else if (operation == 4)
                        return new Thickness(0, 0, 0, 2);
                    else
                        return new Thickness(0, 0, 0, 0);
                default:
                    return new Thickness(0, 0, 0, 0);
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
