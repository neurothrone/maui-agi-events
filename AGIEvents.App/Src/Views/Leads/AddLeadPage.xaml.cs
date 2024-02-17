namespace AGIEvents.App.Views.Leads;

public partial class AddLeadPage : ContentPage
{
    public AddLeadPage(Lib.ViewModels.AddLeadViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}