using System.Collections.ObjectModel;
using AGIEvents.App.Models;
using AGIEvents.App.Views;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.App.ViewModels;

internal partial class EventsViewModel
{
    public ObservableCollection<EventViewModel> Events { get; }

    public EventsViewModel()
    {
        Events = new ObservableCollection<EventViewModel>(
            Event.Samples().Select(e => new EventViewModel(e))
        );
    }


    [RelayCommand]
    private async Task NavigateToLeadsForEvent(EventViewModel eventViewModel)
    {
        await Shell.Current.GoToAsync(
            $"{nameof(LeadsPage)}?EventId={eventViewModel.Id}"
        );
    }
}