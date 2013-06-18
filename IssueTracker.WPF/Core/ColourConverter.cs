using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace IssueTracker.WPF.Core
{
    internal class ColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color)ColorConverter.ConvertFromString("#2ba6cb");

            if (value is Color)
            {
                color = (Color)value;
            }
            else
            {
                try
                {
                    color = ColorConverter.ConvertFromString(value.ToString()) as Color? ?? color;
                }
                catch
                {

                }
            }

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}
