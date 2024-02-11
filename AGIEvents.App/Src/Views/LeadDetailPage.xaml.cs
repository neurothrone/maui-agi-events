using AGIEvents.Lib.ViewModels;

namespace AGIEvents.App.Views;

public partial class LeadDetailPage : ContentPage
{
    public LeadDetailPage(LeadDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}