using System;
using System.Globalization;
using System.Windows.Data;

namespace SyntaxWpfApp
{
    public class TwiceConverter : IValueConverter
    {
        // OneWay 구현
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return int.Parse(value.ToString()) * 2;
        }
        // Twoway 구현
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
