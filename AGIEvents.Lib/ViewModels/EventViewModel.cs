using AGIEvents.Lib.Domain;
using AGIEvents.Lib.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace AGIEvents.Lib.ViewModels;

public class EventViewModel : ObservableObject, IRecipient<EventSavedChangedMessage>
{
    public string EventId { get; }
    public string Title { get; }
    public string Image { get; }

    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    // Format: 19-21 Sep
    public string FormattedDateRange => $"{StartDate.Day}-{EndDate.Day} {StartDate:MMM}";

    private bool _isSaved;

    public bool IsSaved
    {
        get => _isSaved;
        set => SetProperty(ref _isSaved, value);
    }

    public EventViewModel(
        string eventId,
        string title,
        string image,
        DateTime startDate,
        DateTime endDate,
        bool isSaved)
    {
        EventId = eventId;
        Title = title;
        Image = image;
        StartDate = startDate;
        EndDate = endDate;
        IsSaved = isSaved;

        WeakReferenceMessenger.Default.Register(this);
    }

    void IRecipient<EventSavedChangedMessage>.Receive(EventSavedChangedMessage message)
    {
        if (message.EventId != EventId || IsSaved)
            return;

        IsSaved = message.IsSaved;
    }

    public static EventViewModel FromRecord(EventRecordDto record, bool isSaved = false)
    {
        return new EventViewModel(
            record.EventId,
            record.Title,
            record.Image,
            record.StartDate,
            record.EndDate,
            isSaved: isSaved);
    }
}