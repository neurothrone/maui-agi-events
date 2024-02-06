using AGIEvents.App.ViewModels;

namespace AGIEvents.App.Views;

public partial class LeadsPage : ContentPage
{
    public LeadsPage()
    {
        InitializeComponent();
        BindingContext = new LeadsViewModel();
    }
}