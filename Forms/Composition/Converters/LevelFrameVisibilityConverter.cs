﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LabBook.Forms.Composition.Converters
{
    public class LevelFrameVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int level = (int)value;

            if (level == 0)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
