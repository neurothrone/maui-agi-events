using System.Globalization;

namespace AGIEvents.App.Converters;

public class InverseBooleanConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool booleanValue)
            return !booleanValue;

        throw new ArgumentException("Value must be a boolean");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException("InverseBooleanConverter can only be used with one way bindings");
    }
}