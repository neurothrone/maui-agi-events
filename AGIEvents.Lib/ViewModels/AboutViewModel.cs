using AGIEvents.Lib.Services;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.Lib.ViewModels;

public partial class AboutViewModel(
    IAppInteractionsService appInteractionsService,
    IConfigurationService configurationService)
{
    private const string LinkedinUrl = "https://www.linkedin.com/in/neurothrone/";

    public string AppTitle => AppInfo.Name;
    public string Version => AppInfo.VersionString;
    public string Author => "Zane Neurothrone";
    public string DotNetTextCredits => "This app is powered by .NET MAUI";
    public string DotNetDetailCredits => "Written in XAML & C#";

    [RelayCommand]
    private async Task OpenSupportEmailLink()
    {
        await appInteractionsService.ComposeEmailAsync(
            configurationService.GetSupportEmail(),
            "GasMatic Support"
        );
    }

    [RelayCommand]
    private async Task OpenLinkedinWebLink() => await appInteractionsService.OpenBrowserAsync(LinkedinUrl);
}