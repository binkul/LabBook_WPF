﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
                case "txtVoc":
                    result = start + colStatus + colLP + colMaterial + colAmountPr
                        + colAmountMass + colPriceKg + colPrice + colVoc;
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