using Killerrin.Toolkit.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Killerrin.Toolkit.UWP.Converters
{
    public class ShorthandCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            return CurrencyHelpers.ConvertToShorthand((double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return ""; // return CurrencyHelpers.ConvertBackToRegularForm((string)value);
        }
    }
}