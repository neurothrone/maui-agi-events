namespace AGIEvents.App.Views.Events;

public partial class EventsPage : ContentPage
{
    public EventsPage(Lib.ViewModels.EventsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}