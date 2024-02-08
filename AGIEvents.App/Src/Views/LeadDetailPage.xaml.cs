namespace AGIEvents.App.Views;

public partial class LeadDetailPage : ContentPage
{
    public LeadDetailPage(ViewModels.LeadDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}