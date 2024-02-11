namespace AGIEvents.App.Views;

public partial class LeadsPage : ContentPage
{
    public LeadsPage(Lib.ViewModels.LeadsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}