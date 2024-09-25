using System.Globalization;

namespace LunchAndLearn2024MauiApp.Converters;

public class InverseBooleanConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value != null && value is bool boolean)
        {
            return !boolean;
        }

        throw new FormatException();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value != null && value is bool boolean)
        {
            return !boolean;
        }

        throw new FormatException();
    }
}
