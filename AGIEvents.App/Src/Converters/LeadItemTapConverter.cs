using System.Globalization;
using AGIEvents.App.ViewModels;

namespace AGIEvents.App.Converters;

public class LeadItemTapConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var eventArgs = value as ItemTappedEventArgs;
        return eventArgs?.Item as LeadViewModel;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}