using ApptiqueClient.Platforms.Android;
using ApptiqueClient.Services;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace ApptiqueClient;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });


        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddScoped(sp => new HttpClient());
        builder.Services.AddTransient<AppService>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddHostedService<UpdateCheckService>();
        builder.Services.AddMudServices();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

#if ANDROID
        builder.Services.AddSingleton<IPackageService, PackageService>();
        builder.Services.AddSingleton<IStateChangedService, StateChangedService>();
        builder.Services.AddSingleton<IInstallerService, InstallerService>();

        builder.Services.AddTransient<IServiceTest, UpdateCheckService>();
        builder.Services.AddHostedService<UpdateCheckService>();
        builder.Services.AddMudServices();
#endif


        return builder.Build();
    }
}