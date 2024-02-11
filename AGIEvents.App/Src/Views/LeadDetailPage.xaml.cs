namespace AGIEvents.App.Views;

public partial class LeadDetailPage : ContentPage
{
    public LeadDetailPage(Lib.ViewModels.LeadDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}