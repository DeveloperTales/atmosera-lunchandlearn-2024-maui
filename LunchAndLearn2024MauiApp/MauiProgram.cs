using LunchAndLearn2024MauiApp.Services;
using LunchAndLearn2024MauiApp.ViewModels;
using LunchAndLearn2024MauiApp.Views;
using Microsoft.Extensions.Logging;

namespace LunchAndLearn2024MauiApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa-brands-400.ttf", "FAB");
                fonts.AddFont("fa-400.otf", "FA");
                fonts.AddFont("fa-solid-900.otf", "FAS");
            });

        builder.Services.AddServices();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

    private static void AddServices(this IServiceCollection services)
    {
        // Pages
        services.AddTransient<AboutPage>();
        services.AddSingleton<ActiveBooksPage>();
        services.AddTransient<BookAddUpdatePage>();
        services.AddSingleton<CompletedBooksPage>();
        services.AddTransient<QuotesPage>();
        services.AddSingleton<UpcomingBooksPage>();

        // View Models
        services.AddTransient<AboutViewModel>();
        services.AddTransient<BooksViewModel>();
        services.AddTransient<QuotesViewModel>();
        services.AddTransient<BookViewModel>();

        // Services
        services.AddSingleton<IApiService, ApiService>();
        services.AddSingleton<ITelemetry, TelemetryManager>();
        services.AddSingleton<IMediaPicker>(s => MediaPicker.Default);

#if ANDROID
        services.AddSingleton<IDeviceOrientationService, LunchAndLearn2024MauiApp.Services.DeviceOrientationService>();
#elif IOS
        services.AddSingleton<IDeviceOrientationService, LunchAndLearn2024MauiApp.Services.DeviceOrientationService>();
#endif
    }
}
