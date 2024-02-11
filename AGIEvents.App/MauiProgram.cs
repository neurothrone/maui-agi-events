using AGIEvents.Lib.ViewModels;
using Microsoft.Extensions.Logging;

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
            })
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
            // Views
            .AddSingleton<Views.EventsPage>()
            .AddSingleton<Views.LeadsPage>()
            .AddSingleton<Views.SettingsPage>()
            .AddTransient<Views.LeadDetailPage>()
            .AddTransient<Views.AddLeadPage>()
            ;

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}