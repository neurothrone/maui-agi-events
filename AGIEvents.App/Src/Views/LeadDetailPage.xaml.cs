using AGIEvents.App.ViewModels;

namespace AGIEvents.App.Views;

public partial class LeadDetailPage : ContentPage
{
    public LeadDetailPage()
    {
        InitializeComponent();
        BindingContext = new LeadDetailViewModel();
    }
}