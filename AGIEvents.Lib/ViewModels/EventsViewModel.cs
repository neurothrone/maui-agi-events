using System.Collections.ObjectModel;
using AGIEvents.Lib.Models;
using AGIEvents.Lib.Services.Database;
using AGIEvents.Lib.Services.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AGIEvents.Lib.ViewModels;

public class EventGroup(
    string groupName,
    IEnumerable<EventViewModel> events) : ObservableCollection<EventViewModel>(events)
{
    public string GroupName { get; init; } = groupName;
}

public partial class EventsViewModel : ObservableObject
{
    private const string YourEvents = "Your Events";
    private const string UpcomingEvents = "Coming Events";

    private readonly IDatabaseRepository _databaseRepository;

    [ObservableProperty] private bool _isLoading;

    private ObservableCollection<EventGroup> _groupedEvents = [];

    public ObservableCollection<EventGroup> GroupedEvents
    {
        get => _groupedEvents;
        private set => SetProperty(ref _groupedEvents, value);
    }

    public EventsViewModel(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository;
        SubscribeToMessenger();
        FetchEvents();
    }

    private void SubscribeToMessenger()
    {
        WeakReferenceMessenger.Default.Register<QrScannerCompletedMessage>(
            this,
            (_, message) => ProcessQrCode(message.QrCode)
        );
    }

    private void ProcessQrCode(string qrCode)
    {
        Console.WriteLine($"✅ -> QR Code scanned: {qrCode}");
    }

    private async void FetchEvents()
    {
        IsLoading = true;
        await Task.Delay(500);

        // TODO: fetch from database
        var eventViewModels = new List<EventViewModel>(
            EventViewModel.Samples()
                .OrderBy(e => e.StartDate)
                .ToList()
        );

        // Order by ascending (StartDate) will sort the events from the ones that are
        // scheduled to start soonest.
        var savedEvents = new EventGroup(YourEvents,
            eventViewModels
                .Where(e => e.IsSaved)
                .OrderBy(e => e.StartDate)
                .ToList());
        var upcomingEvents = new EventGroup(UpcomingEvents,
            eventViewModels
                .Where(e => !e.IsSaved)
                .OrderBy(e => e.StartDate)
                .ToList());
        GroupedEvents = [savedEvents, upcomingEvents];

        IsLoading = false;
    }

    private void MoveEventToGroup(string eventId, string sourceGroupName, string targetGroupName)
    {
        // Find the source group
        var sourceGroup = GroupedEvents.FirstOrDefault(g => g.GroupName == sourceGroupName);
        if (sourceGroup?.FirstOrDefault(e => e.EventId == eventId) is not { } eventToMove)
            return;

        sourceGroup.Remove(eventToMove);

        // Find the target group
        var targetGroup = GroupedEvents.FirstOrDefault(g => g.GroupName == targetGroupName);
        if (targetGroup != null)
        {
            targetGroup.Add(eventToMove);
        }
        else
        {
            // If target group doesn't exist, create it and add the event
            targetGroup = new EventGroup(targetGroupName, new List<EventViewModel> { eventToMove });
            GroupedEvents.Add(targetGroup);
        }
    }

    [RelayCommand]
    private void AddEvent()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            var group = GroupedEvents.FirstOrDefault(g => g.GroupName == YourEvents);

            if (group == null)
            {
                group = new EventGroup(YourEvents, new List<EventViewModel>());
                GroupedEvents.Add(group);
            }


            group.Add(
                new EventViewModel(
                    eventId: "sopno398692",
                    title: "Sign, Print & Promotion",
                    image: "sopno_logo.png",
                    startDate: DateTime.Now.AddDays(150),
                    endDate: DateTime.Now.AddDays(152),
                    isSaved: true
                )
            );
        });
    }

    [RelayCommand]
    private void ClearEvents()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            foreach (var group in GroupedEvents)
                group.Clear();
        });
    }

    [RelayCommand]
    private void AddEvents()
    {
        MainThread.BeginInvokeOnMainThread(FetchEvents);
    }

    [RelayCommand]
    private async Task EventTapped(EventViewModel eventViewModel)
    {
        if (eventViewModel.IsSaved)
        {
            await Shell.Current.GoToAsync(
                $"{nameof(AppRoute.LeadsPage)}?{nameof(EventViewModel.EventId)}={eventViewModel.EventId}"
            );
        }
        else
        {
            // TODO: Open QR Scanner
            WeakReferenceMessenger.Default.Send(new EventSavedChangedMessage(eventViewModel.EventId, true));
            MainThread.BeginInvokeOnMainThread(() =>
            {
                MoveEventToGroup(eventViewModel.EventId, UpcomingEvents, YourEvents);
            });

            // await Shell.Current.GoToAsync(nameof(AppRoute.QrScannerPage));
        }
    }

    [RelayCommand]
    private async Task NavigateToSettings()
    {
        await Shell.Current.GoToAsync(nameof(AppRoute.SettingsPage));
    }
}