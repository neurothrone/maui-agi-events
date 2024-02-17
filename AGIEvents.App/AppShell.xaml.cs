using AGIEvents.App.Views.Leads;
using AGIEvents.App.Views.Scanner;
using AGIEvents.App.Views.Settings;

namespace AGIEvents.App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(LeadsPage), typeof(LeadsPage));
        Routing.RegisterRoute(nameof(LeadDetailPage), typeof(LeadDetailPage));
        Routing.RegisterRoute(nameof(AddLeadPage), typeof(AddLeadPage));
        Routing.RegisterRoute(nameof(QrScannerPage), typeof(QrScannerPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}