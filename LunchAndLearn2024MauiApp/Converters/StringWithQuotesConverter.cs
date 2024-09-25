using System.Globalization;

namespace LunchAndLearn2024MauiApp.Converters;
public class StringWithQuotesConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value == null ? throw new ArgumentNullException(nameof(value)) : $"\"{value}\"";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
