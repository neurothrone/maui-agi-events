using AGIEvents.App.ViewModels;

namespace AGIEvents.App.Views;

public partial class EventsPage : ContentPage
{
    public EventsPage()
    {
        InitializeComponent();
        BindingContext = new EventsViewModel();
    }
}