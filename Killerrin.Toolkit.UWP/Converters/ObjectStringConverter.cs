﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Killerrin.Toolkit.UWP.Converters
{
    public class ObjectStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ((object)value);
        }
    }
}
