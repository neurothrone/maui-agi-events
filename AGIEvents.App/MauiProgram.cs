using AGIEvents.Lib.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;

namespace AGIEvents.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            // Required to use CommunityToolkit.Maui
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            // Third Party
            .UseBarcodeReader()
            // Initialize Services
            .Services
            // ViewModels
            .AddSingleton<EventsViewModel>()
            .AddSingleton<LeadsViewModel>()
            .AddSingleton<SettingsViewModel>()
            .AddTransient<EventViewModel>()
            .AddTransient<LeadViewModel>()
            .AddTransient<LeadDetailViewModel>()
            .AddTransient<AddLeadViewModel>()
            .AddTransient<QrScannerViewModel>()
            // Views
            .AddSingleton<Views.EventsPage>()
            .AddSingleton<Views.LeadsPage>()
            .AddSingleton<Views.SettingsPage>()
            .AddTransient<Views.LeadDetailPage>()
            .AddTransient<Views.AddLeadPage>()
            .AddTransient<Views.QrScannerPage>()
            ;

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}