namespace AGIEvents.App.Views;

public partial class AddLeadPage : ContentPage
{
    public AddLeadPage(ViewModels.AddLeadViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}