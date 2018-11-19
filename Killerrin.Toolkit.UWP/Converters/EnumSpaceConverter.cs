using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Killerrin.Toolkit.UWP.Converters
{
    public class EnumSpaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Enum e = ((Enum)value);
            return Killerrin.Toolkit.Core.Helpers.StringHelpers.AddSpacesToSentence(e.ToString(), true);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
