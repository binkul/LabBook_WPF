﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace LabBook.Forms.Composition.Converters
{
    public class SumAmountToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string type = parameter.ToString();
            double sum = System.Convert.ToDouble(value);
            string result;

            switch(type)
            {
                case "Percent":
                    result = sum.ToString("F4", CultureInfo.CurrentCulture) + "%";
                    break;
                case "Mass":
                    result = sum.ToString("F2", CultureInfo.CurrentCulture) + " kg";
                    break;
                default:
                    result = "0";
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
