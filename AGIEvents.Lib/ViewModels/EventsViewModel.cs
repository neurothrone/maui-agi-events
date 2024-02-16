using System.Collections.ObjectModel;
using AGIEvents.Lib.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.Lib.ViewModels;

public partial class EventsViewModel : ObservableObject
{
    [ObservableProperty] private bool _isLoading;

    private ObservableCollection<EventGroup> _groupedEvents = new();

    public ObservableCollection<EventGroup> GroupedEvents
    {
        get => _groupedEvents;
        private set => SetProperty(ref _groupedEvents, value);
    }

    public EventsViewModel()
    {
        FetchEvents();
    }

    private async void FetchEvents()
    {
        IsLoading = true;
        await Task.Delay(500);

        // TODO: fetch from database
        var eventViewModels = new List<EventViewModel>(
            Event.Samples()
                .Select(e => new EventViewModel(e))
                .ToList()
        );

        // Order by ascending (StartDate) will sort the events from the ones that are
        // scheduled to start soonest.
        var savedEvents = new EventGroup("Your Events",
            eventViewModels
                .Where(e => e.IsSaved)
                .OrderBy(e => e.StartDate)
                .ToList());
        var upcomingEvents = new EventGroup("Coming Soon",
            eventViewModels
                .Where(e => !e.IsSaved)
                .OrderBy(e => e.StartDate)
                .ToList());
        GroupedEvents = [savedEvents, upcomingEvents];

        IsLoading = false;
    }

    private void MoveEventToGroup(int eventId, string sourceGroupName, string targetGroupName)
    {
        //Find the source group
        var sourceGroup = GroupedEvents.FirstOrDefault(g => g.GroupName == sourceGroupName);

        if (sourceGroup != null)
        {
            //Find the event in the source group
            var eventToMove = sourceGroup.FirstOrDefault(e => e.Id == eventId);

            if (eventToMove != null)
            {
                eventToMove.ToggleIsSaved();
                sourceGroup.Remove(eventToMove);

                //Find the target group
                var targetGroup = GroupedEvents.FirstOrDefault(g => g.GroupName == targetGroupName);

                if (targetGroup != null)
                {
                    targetGroup.Add(eventToMove);
                }
                else
                {
                    //If target group doesn't exist, create it and add the event
                    targetGroup = new EventGroup(targetGroupName, new List<EventViewModel> { eventToMove });
                    GroupedEvents.Add(targetGroup);
                }

                OnPropertyChanged(nameof(GroupedEvents));
            }
        }
    }

    [RelayCommand]
    private async Task AddEvent()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            var group = GroupedEvents.FirstOrDefault(g => g.GroupName == "Your Events");

            if (group == null)
            {
                group = new EventGroup("Your Events", new List<EventViewModel>());
                GroupedEvents.Add(group);
            }


            group.Add(
                new EventViewModel(new Event(
                        id: 2,
                        eventId: "sopno398632",
                        title: "Sign, Print & Promotion",
                        image: "sopno_logo.png",
                        startDate: DateTime.Now.AddDays(150),
                        endDate: DateTime.Now.AddDays(152),
                        isSaved: true
                    )
                )
            );
        });
    }

    [RelayCommand]
    private async Task ClearEvents()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            foreach (var group in GroupedEvents)
                group.Clear();
        });
    }

    [RelayCommand]
    private async Task AddEvents()
    {
        MainThread.BeginInvokeOnMainThread(async () => { FetchEvents(); });
    }

    [RelayCommand]
    private async Task EventTapped(EventViewModel eventViewModel)
    {
        // If IsSaved, navigate to Leads
        // TODO: Else open QR Scanner

        if (eventViewModel.IsSaved)
        {
            await NavigateToLeadsForEvent(eventViewModel);
        }
        else
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                MoveEventToGroup(eventViewModel.Id, "Coming Soon", "Your Events");
            });
        }
    }

    private async Task NavigateToLeadsForEvent(EventViewModel eventViewModel)
    {
        // TODO: send the EventViewModel instead, because the Id property is for the database in contrast to EventId
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