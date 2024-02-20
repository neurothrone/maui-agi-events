using AGIEvents.App.Views.Events;
using AGIEvents.App.Views.Leads;
using AGIEvents.App.Views.Scanner;
using AGIEvents.App.Views.Settings;
using AGIEvents.Lib.Services;
using AGIEvents.Lib.Services.Database;
using AGIEvents.Lib.Services.Events;
using AGIEvents.Lib.Services.Firebase;
using AGIEvents.Lib.Services.Realtime;
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
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("MaterialSymbolsRounded.ttf", "MaterialSymbolsRounded");
            });
        // Required to use CommunityToolkit.Maui
        builder.UseMauiCommunityToolkit();

        // Third Party
        builder.UseBarcodeReader();

        builder.Services.AddSingleton<EventsPage>();
        builder.Services.AddSingleton<EventsViewModel>();
        builder.Services.AddTransient<EventViewModel>();

        builder.Services.AddTransient<LeadsPage>();
        builder.Services.AddTransient<LeadsViewModel>();
        builder.Services.AddTransient<LeadItemViewModel>();

        builder.Services.AddTransient<LeadDetailPage>();
        builder.Services.AddTransient<LeadDetailViewModel>();

        builder.Services.AddTransient<AddLeadPage>();
        builder.Services.AddTransient<AddLeadViewModel>();

        builder.Services.AddTransient<QrScannerPage>();
        builder.Services.AddTransient<QrScannerViewModel>();

        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<SettingsViewModel>();

        builder.Services.AddSingleton<IAppInteractionsService, AppInteractionsService>();
        builder.Services.AddSingleton<IDatabaseRepository, DatabaseRepository>();
        builder.Services.AddTransient<IEventsService, EventsFileStorageService>(
            _ => new EventsFileStorageService(FileSystem.OpenAppPackageFileAsync("events.json"))
        );

        builder.Services.AddSingleton<INotificationService, NotificationService>();

        // Load environment variables
        IDotEnvService dotEnvService = new DotEnvService();
        dotEnvService.LoadEnvironmentVariables(File.OpenRead(".env"));

        builder.Services.AddTransient<IConfigurationService, ConfigurationService>();
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IRealtimeService, FirebaseRealtimeService>();

        builder.Services.AddSingleton<ICsvService, CsvService>();
        builder.Services.AddSingleton<IShareService, ShareService>();

        // Register routes
        Routing.RegisterRoute(nameof(LeadsPage), typeof(LeadsPage));
        Routing.RegisterRoute(nameof(LeadDetailPage), typeof(LeadDetailPage));
        Routing.RegisterRoute(nameof(AddLeadPage), typeof(AddLeadPage));
        Routing.RegisterRoute(nameof(QrScannerPage), typeof(QrScannerPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}