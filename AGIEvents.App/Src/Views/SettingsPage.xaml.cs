namespace AGIEvents.App.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(Lib.ViewModels.SettingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}