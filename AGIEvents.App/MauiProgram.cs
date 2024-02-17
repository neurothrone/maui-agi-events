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
            .AddSingleton<Lib.ViewModels.EventsViewModel>()
            .AddTransient<Lib.ViewModels.EventViewModel>()
            .AddSingleton<Lib.ViewModels.LeadsViewModel>()
            .AddTransient<Lib.ViewModels.LeadViewModel>()
            .AddTransient<Lib.ViewModels.LeadDetailViewModel>()
            .AddTransient<Lib.ViewModels.AddLeadViewModel>()
            .AddTransient<Lib.ViewModels.QrScannerViewModel>()
            .AddSingleton<Lib.ViewModels.SettingsViewModel>()
            // Views
            .AddSingleton<Views.Events.EventsPage>()
            .AddSingleton<Views.Leads.LeadsPage>()
            .AddTransient<Views.Leads.LeadDetailPage>()
            .AddTransient<Views.Leads.AddLeadPage>()
            .AddTransient<Views.Scanner.QrScannerPage>()
            .AddSingleton<Views.Settings.SettingsPage>()
            ;

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}