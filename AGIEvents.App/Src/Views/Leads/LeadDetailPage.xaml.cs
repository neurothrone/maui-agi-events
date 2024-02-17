namespace AGIEvents.App.Views.Leads;

public partial class LeadDetailPage : ContentPage
{
    public LeadDetailPage(Lib.ViewModels.LeadDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}