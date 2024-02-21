using System.Collections.ObjectModel;
using System.Diagnostics;
using AGIEvents.Lib.Domain;
using AGIEvents.Lib.Messages;
using AGIEvents.Lib.Models;
using AGIEvents.Lib.Services.Database;
using AGIEvents.Lib.Services.Events;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AGIEvents.Lib.ViewModels;

public partial class EventsViewModel : ObservableObject,
    IRecipient<QrScannedExhibitorMessage>,
    IRecipient<AllDataDeletedMessage>
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
        FetchAndMergeEvents();
    }

    private void SubscribeToMessages()
    {
        WeakReferenceMessenger.Default.Register<QrScannedExhibitorMessage>(this);
        WeakReferenceMessenger.Default.Register<AllDataDeletedMessage>(this);
    }

    void IRecipient<QrScannedExhibitorMessage>.Receive(QrScannedExhibitorMessage message)
    {
        OnQrCodeScannedSuccessfully(message.EventId);
    }

    async void IRecipient<AllDataDeletedMessage>.Receive(AllDataDeletedMessage message)
    {
        await ClearEvents();
    }

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
            .Where(nonSavedEvent => !savedEventsIds.Contains(nonSavedEvent.EventId) &&
                                    // Skip unsaved events that have already taken place.
                                    nonSavedEvent.StartDate > DateTime.Now)
            .Select(e => EventViewModel.FromRecord(e))
            .ToList();

        // Order by 'StartDate' in descending order to sort the events, starting with the ones
        // that took place most recently.
        var savedEvents = new EventGroup(YourEvents, savedEventsViewModels
            .OrderByDescending(e => e.StartDate)
            .ToList());
        // Ordering by 'StartDate' in ascending order will sort the events starting from the
        // ones scheduled to happen the soonest.
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
            targetGroup.SortByDescendingStartDate();
        }
        else
        {
            // If target group doesn't exist, create it and add the event
            targetGroup = new EventGroup(targetGroupName, new List<EventViewModel> { eventToMove });
            GroupedEvents.Add(targetGroup);
        }
    }

    private async void OnQrCodeScannedSuccessfully(string eventId)
    {
        var eventViewModel = GroupedEvents.SelectMany(g => g).FirstOrDefault(e => e.EventId == eventId);

        if (eventViewModel is null)
        {
            Debug.WriteLine($"❌ -> EventId '{eventId}' not found.");
            return;
        }

        await SaveEvent(eventViewModel);
    }

    private async Task SaveEvent(EventViewModel eventViewModel)
    {
        // Save to database
        await _databaseRepository.SaveEventAsync(EventRecordDto.FromViewModel(eventViewModel));

        // Update UI
        WeakReferenceMessenger.Default.Send(new EventSavedChangedMessage(eventViewModel.EventId, true));
        MainThread.BeginInvokeOnMainThread(() =>
        {
            MoveEventToGroup(eventViewModel.EventId, UpcomingEvents, YourEvents);
        });

        // TODO: Navigate to LeadsPage for this Event
    }

    private async Task NavigateToQrScanner(string eventId)
    {
        await Shell.Current.GoToAsync(nameof(AppRoute.QrScannerPage),
            new Dictionary<string, object>
            {
                { nameof(EventViewModel.EventId), eventId },
                { nameof(ParticipantType), ParticipantType.Exhibitor }
            });
    }

    private async Task NavigateToLeadsForEvent(EventViewModel eventViewModel)
    {
        var record = EventRecordDto.FromViewModel(eventViewModel);
        await Shell.Current.GoToAsync(
            $"{nameof(AppRoute.LeadsPage)}",
            new Dictionary<string, object> { { nameof(EventRecordDto), record } }
        );
    }

    [RelayCommand]
    private async Task EventTapped(EventViewModel eventViewModel)
    {
        if (eventViewModel.IsSaved)
        {
            await NavigateToLeadsForEvent(eventViewModel);
        }
        else
        {
            // TEMP: Testing purposes, to skip Qr Scanning
            // OnQrCodeScannedSuccessfully(eventViewModel.EventId);

            await NavigateToQrScanner(eventViewModel.EventId);
        }
    }

    [RelayCommand]
    private async Task NavigateToSettings()
    {
        await Shell.Current.GoToAsync(nameof(AppRoute.SettingsPage));
    }

    [RelayCommand]
    private async Task AddEvent()
    {
        var eventViewModel = GroupedEvents.SelectMany(g => g).FirstOrDefault();

        if (eventViewModel is null)
        {
            Debug.WriteLine($"❌ -> Failed to Add Event.");
            return;
        }

        await SaveEvent(eventViewModel);
    }
}