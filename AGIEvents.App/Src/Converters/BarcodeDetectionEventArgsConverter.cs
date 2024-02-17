using System.Globalization;
using ZXing.Net.Maui;

namespace AGIEvents.App.Converters;

public class BarcodeDetectionEventArgsConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is BarcodeDetectionEventArgs args && args.Results.FirstOrDefault()?.Value is { } qrCode)
            return qrCode;

        return string.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}