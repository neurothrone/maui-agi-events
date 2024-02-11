namespace AGIEvents.App.Views;

public partial class EventsPage : ContentPage
{
    public EventsPage(Lib.ViewModels.EventsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}