namespace AGIEvents.App.Views;

public partial class AddLeadPage : ContentPage
{
    public AddLeadPage(Lib.ViewModels.AddLeadViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}