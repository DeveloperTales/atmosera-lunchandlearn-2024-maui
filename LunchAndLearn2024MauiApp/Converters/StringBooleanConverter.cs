using System.Globalization;

namespace LunchAndLearn2024MauiApp.Converters;

public class StringBooleanConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value != null && value is string @string && !string.IsNullOrWhiteSpace(@string))
        {
            return true;
        }

        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
