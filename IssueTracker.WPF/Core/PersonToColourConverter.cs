using System;
using System.Globalization;
using System.Windows.Data;
using IssueTracker.Data;

namespace IssueTracker.WPF.Core
{
    public class PersonToColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var person = value as Person;
            if (person != null)
            {
                if (person.IsEmployee)
                {
                    return "Green";
                }
            }

            return "Blue";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Cannot convert from a colour to a person");
        }
    }
}
