using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace AGIEvents.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            // Initialize CommunityToolkit
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            // Initialize Services
            .Services
            // ViewModels
            .AddSingleton<ViewModels.EventsViewModel>()
            .AddSingleton<ViewModels.LeadsViewModel>()
            .AddSingleton<ViewModels.SettingsViewModel>()
            .AddTransient<ViewModels.EventViewModel>()
            .AddTransient<ViewModels.LeadViewModel>()
            .AddTransient<ViewModels.LeadDetailViewModel>()
            .AddTransient<ViewModels.AddLeadViewModel>()
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