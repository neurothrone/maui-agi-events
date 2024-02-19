using System.Collections.ObjectModel;
using AGIEvents.Lib.Domain;
using AGIEvents.Lib.Messages;
using AGIEvents.Lib.Models;
using AGIEvents.Lib.Services.Database;
using AGIEvents.Lib.Services.Events;
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

public partial class EventsViewModel : ObservableObject,
    IRecipient<QrScannerCompletedMessage>
{
    private const string YourEvents = "Your Events";
    private const string UpcomingEvents = "Coming Events";

    private readonly IDatabaseRepository _databaseRepository;
    private readonly IEventsService _eventsService;

    [ObservableProperty] private bool _isLoading;

    private ObservableCollection<EventGroup> _groupedEvents = [];

    public ObservableCollection<EventGroup> GroupedEvents
    {
        get => _groupedEvents;
        private set => SetProperty(ref _groupedEvents, value);
    }

    public EventsViewModel(
        IDatabaseRepository databaseRepository,
        IEventsService eventsService
    )
    {
        _databaseRepository = databaseRepository;
        _eventsService = eventsService;

        SubscribeToMessages();

        // TODO: implement NavigationService and OnNavigatedTo and call await FetchAndMergeEvents() from there
        // TODO: Or use the same approach when loading json data?
        FetchAndMergeEvents();
    }

    private void SubscribeToMessages()
    {
        WeakReferenceMessenger.Default.Register<QrScannerCompletedMessage>(this);
    }

    // TODO: Extract out FetchEventsFromFile and FetchEventsFromDatabase tasks?
    private async Task FetchAndMergeEvents()
    {
        IsLoading = true;

        var eventsFromDatabaseTask = _databaseRepository.FetchEventsAsync();
        var eventsFromFileTask = _eventsService.LoadEvents();

        await Task.WhenAll(eventsFromDatabaseTask, eventsFromFileTask);

        var savedEventsFromDatabase = eventsFromDatabaseTask.Result;
        var savedEventsViewModels = savedEventsFromDatabase
            .Select(e => EventViewModel.FromRecord(e, isSaved: true))
            .ToList();

        // Create a HashSet for better performance during lookups, although it will be negligible for the
        // small collections that this app will work with when considering only the Events in the database.
        var savedEventsIds = new HashSet<string>(savedEventsViewModels.Select(e => e.EventId));

        var nonSavedEventsFromFile = eventsFromFileTask.Result;
        var nonSavedViewModels = nonSavedEventsFromFile
            .Where(nonSavedEvent => !savedEventsIds.Contains(nonSavedEvent.EventId))
            .Select(e => EventViewModel.FromRecord(e))
            .ToList();

        // Order by ascending (StartDate) will sort the events from the ones that are
        // scheduled to start soonest.
        var savedEvents = new EventGroup(YourEvents, savedEventsViewModels
            .OrderBy(e => e.StartDate)
            .ToList());
        var upcomingEvents = new EventGroup(UpcomingEvents, nonSavedViewModels
            .OrderBy(e => e.StartDate)
            .ToList());

        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            IsLoading = false;
            GroupedEvents = [savedEvents, upcomingEvents];
        });
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

    private void ProcessQrCode(string qrCode)
    {
        Console.WriteLine($"✅ -> QR Code scanned: {qrCode}");
        // TODO: We need to send the EventId as well from the Message
        // TODO: Check Firebase if exhibitorId exists for the Event

        // If true, save to database and update UI

        // Else show toast/snackbar that the user is not registered for the event
    }

    void IRecipient<QrScannerCompletedMessage>.Receive(QrScannerCompletedMessage message)
    {
        ProcessQrCode(message.QrCode);
    }

    [RelayCommand]
    private async Task EventTapped(EventViewModel eventViewModel)
    {
        if (eventViewModel.IsSaved)
        {
            var record = EventRecordDto.FromViewModel(eventViewModel);
            await Shell.Current.GoToAsync(
                $"{nameof(AppRoute.LeadsPage)}",
                new Dictionary<string, object> { { nameof(EventRecordDto), record } }
            );
        }
        else
        {
            // TODO: Open QR Scanner
            // Save to database
            await _databaseRepository.SaveEventAsync(EventRecordDto.FromViewModel(eventViewModel));

            // Update UI
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

    [RelayCommand]
    private async Task ClearEvents()
    {
        await _databaseRepository.DeleteAllDataAsync();

        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            foreach (var group in GroupedEvents)
                group.Clear();
        });

        await FetchAndMergeEvents();
    }
}