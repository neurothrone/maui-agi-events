using AGIEvents.App.ViewModels;

namespace AGIEvents.App.Views;

public partial class AddLeadPage : ContentPage
{
    public AddLeadPage()
    {
        InitializeComponent();
        BindingContext = new AddLeadViewModel();
    }
}