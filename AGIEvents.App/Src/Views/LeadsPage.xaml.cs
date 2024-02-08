namespace AGIEvents.App.Views;

public partial class LeadsPage : ContentPage
{
    public LeadsPage(ViewModels.LeadsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}