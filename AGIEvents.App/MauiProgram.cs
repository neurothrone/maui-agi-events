using AGIEvents.App.Views.Events;
using AGIEvents.App.Views.Leads;
using AGIEvents.App.Views.Scanner;
using AGIEvents.App.Views.Settings;
using AGIEvents.Lib.Services;
using AGIEvents.Lib.Services.Database;
using AGIEvents.Lib.Services.Events;
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

        builder.Services.AddSingleton<LeadsPage>();
        builder.Services.AddSingleton<LeadsViewModel>();
        builder.Services.AddTransient<LeadViewModel>();

        builder.Services.AddTransient<LeadDetailPage>();
        builder.Services.AddTransient<LeadDetailViewModel>();

        builder.Services.AddTransient<AddLeadPage>();
        builder.Services.AddTransient<AddLeadViewModel>();

        builder.Services.AddTransient<QrScannerPage>();
        builder.Services.AddTransient<QrScannerViewModel>();

        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<SettingsViewModel>();

        builder.Services.AddSingleton<IAppInteractionsService, AppInteractionsService>();
        builder.Services.AddSingleton<IDatabaseRepository, DatabaseRepository>();
        builder.Services.AddTransient<IEventsService, EventsFileStorageService>(
            _ => new EventsFileStorageService(FileSystem.OpenAppPackageFileAsync("events.json"))
        );

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}