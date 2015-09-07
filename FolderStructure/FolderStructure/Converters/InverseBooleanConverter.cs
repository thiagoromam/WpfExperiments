using System;
using System.Globalization;
using System.Windows.Data;

namespace FolderStructure.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public IValueConverter BooleanConverter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BooleanConverter.Convert(!(bool)value, targetType, parameter, culture);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}