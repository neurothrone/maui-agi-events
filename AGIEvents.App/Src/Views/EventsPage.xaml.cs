using AGIEvents.Lib.ViewModels;

namespace AGIEvents.App.Views;

public partial class EventsPage : ContentPage
{
    public EventsPage(EventsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}