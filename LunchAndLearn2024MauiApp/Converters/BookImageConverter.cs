using System.Globalization;

namespace LunchAndLearn2024MauiApp.Converters;
public class BookImageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value != null && value is string imageUrl && !string.IsNullOrWhiteSpace(imageUrl))
        {
            return imageUrl;
        }

        return "book.jpg";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
