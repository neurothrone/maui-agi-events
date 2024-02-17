using AGIEvents.App.Views;

namespace AGIEvents.App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(QrScannerPage), typeof(QrScannerPage));
        Routing.RegisterRoute(nameof(LeadsPage), typeof(LeadsPage));
        Routing.RegisterRoute(nameof(LeadDetailPage), typeof(LeadDetailPage));
        Routing.RegisterRoute(nameof(AddLeadPage), typeof(AddLeadPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}