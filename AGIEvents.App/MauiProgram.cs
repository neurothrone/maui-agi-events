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
            .AddTransient<ViewModels.EventViewModel>()
            .AddTransient<ViewModels.LeadsViewModel>()
            .AddTransient<ViewModels.LeadViewModel>()
            .AddTransient<ViewModels.LeadDetailViewModel>()
            .AddTransient<ViewModels.AddLeadViewModel>()
            .AddTransient<ViewModels.SettingsViewModel>()
            // Views
            .AddSingleton<Views.EventsPage>()
            .AddTransient<Views.LeadsPage>()
            .AddTransient<Views.LeadDetailPage>()
            .AddTransient<Views.AddLeadPage>()
            .AddTransient<Views.SettingsPage>()
            ;

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}