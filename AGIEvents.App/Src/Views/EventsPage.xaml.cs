namespace AGIEvents.App.Views;

public partial class EventsPage : ContentPage
{
    public EventsPage(ViewModels.EventsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}