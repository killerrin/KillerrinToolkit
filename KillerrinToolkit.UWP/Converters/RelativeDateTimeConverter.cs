using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Xaml.Data;

namespace KillerrinToolkit.UWP.Converters
{
    public class RelativeDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var dateTime = (DateTime)value;
            return KillerrinToolkit.Core.Helpers.DateTimeHelpers.ToRelativeDateTimeString(dateTime); ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
