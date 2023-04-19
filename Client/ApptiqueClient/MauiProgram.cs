using Microsoft.Extensions.Logging;
using ApptiqueClient.Data;
using ApptiqueClient.Platforms.Android;
using ApptiqueClient.Services;

namespace ApptiqueClient;

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
            });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddScoped(sp => new HttpClient { });
        builder.Services.AddTransient<AppService>();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

#if ANDROID
        builder.Services.AddSingleton<IPackageService, PackageService>();
#endif

        builder.Services.AddSingleton<WeatherForecastService>();

        return builder.Build();
    }
}
