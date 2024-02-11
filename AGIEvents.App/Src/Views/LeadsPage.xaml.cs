using AGIEvents.Lib.ViewModels;

namespace AGIEvents.App.Views;

public partial class LeadsPage : ContentPage
{
    public LeadsPage(LeadsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}