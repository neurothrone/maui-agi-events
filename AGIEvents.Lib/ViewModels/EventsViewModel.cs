using System.Collections.ObjectModel;
using AGIEvents.Lib.Models;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.Lib.ViewModels;

public partial class EventsViewModel
{
    public ObservableCollection<EventViewModel> Events { get; }

    public EventsViewModel()
    {
        Events = new ObservableCollection<EventViewModel>(
            Event.Samples()
                .Select(e => new EventViewModel(e))
                .OrderBy(e => e.Id)
                .ToList()
        );
    }

    [RelayCommand]
    private async Task NavigateToLeadsForEvent(EventViewModel eventViewModel)
    {
        await Shell.Current.GoToAsync(
            $"{nameof(AppRoute.LeadsPage)}?EventId={eventViewModel.Id}"
        );
    }

    [RelayCommand]
    private async Task NavigateToSettings()
    {
        await Shell.Current.GoToAsync(nameof(AppRoute.SettingsPage));
    }
}