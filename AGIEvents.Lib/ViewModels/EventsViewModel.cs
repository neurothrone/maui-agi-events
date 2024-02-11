using System.Collections.ObjectModel;
using AGIEvents.Lib.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.Lib.ViewModels;

public partial class EventsViewModel : ObservableObject
{
    [ObservableProperty] private bool _isLoading;

    private ObservableCollection<EventViewModel> _events;

    public ObservableCollection<EventViewModel> Events
    {
        get => _events;
        set => SetProperty(ref _events, value);
    }

    public EventsViewModel()
    {
        _events = [];
        FetchEvents();
    }

    private async void FetchEvents()
    {
        IsLoading = true;
        await Task.Delay(500);

        Events = new ObservableCollection<EventViewModel>(
            Event.Samples()
                .Select(e => new EventViewModel(e))
                .OrderBy(e => e.Id)
                .ToList()
        );

        IsLoading = false;
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