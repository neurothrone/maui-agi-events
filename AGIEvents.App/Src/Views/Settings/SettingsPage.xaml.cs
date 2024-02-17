namespace AGIEvents.App.Views.Settings;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(Lib.ViewModels.SettingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}