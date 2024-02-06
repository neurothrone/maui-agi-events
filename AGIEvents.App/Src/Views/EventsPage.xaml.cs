namespace AGIEvents.App.Views;

public partial class EventsPage : ContentPage
{
    public EventsPage()
    {
        InitializeComponent();
    }

    private void OnEventTapped(object? sender, TappedEventArgs e)
    {
        Console.WriteLine("✅ -> Tapped");
    }

    private void OnEventClicked(object? sender, EventArgs e)
    {
        Console.WriteLine("✅ -> Tapped");
    }
}