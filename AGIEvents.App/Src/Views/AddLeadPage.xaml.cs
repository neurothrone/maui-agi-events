using AGIEvents.Lib.ViewModels;

namespace AGIEvents.App.Views;

public partial class AddLeadPage : ContentPage
{
    public AddLeadPage(AddLeadViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}