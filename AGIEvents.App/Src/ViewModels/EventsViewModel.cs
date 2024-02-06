using AGIEvents.App.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.App.ViewModels;

public partial class EventsViewModel : ObservableObject
{
    [RelayCommand]
    private async Task NavigateToLeads()
    {
        Console.WriteLine("✅ -> Event Clicked, Navigate to LeadsPage");
        await Shell.Current.GoToAsync(nameof(LeadsPage));
    }
}