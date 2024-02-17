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

    public static List<EventViewModel> Samples() =>
    [
        new(
            eventId: "sopse295842",
            title: "Sign&Print Scandinavia",
            image: "sopse_logo.png",
            startDate: DateTime.Now.AddDays(100),
            endDate: DateTime.Now.AddDays(103),
            isSaved: true
        ),
        new(
            eventId: "sopdk730956",
            title: "Sign Print & Pack Denmark",
            image: "sopdk_logo.png",
            startDate: DateTime.Now.AddDays(125),
            endDate: DateTime.Now.AddDays(127),
            isSaved: false
        ),
        new(
            eventId: "sopno398628",
            title: "Sign, Print & Promotion",
            image: "sopno_logo.png",
            startDate: DateTime.Now.AddDays(150),
            endDate: DateTime.Now.AddDays(152),
            isSaved: false
        ),
        new(
            eventId: "sopno398629",
            title: "Sign, Print & Promotion",
            image: "sopno_logo.png",
            startDate: DateTime.Now.AddDays(165),
            endDate: DateTime.Now.AddDays(168),
            isSaved: false
        ),
        new(
            eventId: "sopno398630",
            title: "Sign, Print & Promotion",
            image: "sopno_logo.png",
            startDate: DateTime.Now.AddDays(172),
            endDate: DateTime.Now.AddDays(174),
            isSaved: false
        ),
        new(
            eventId: "sopno398631",
            title: "Sign, Print & Promotion",
            image: "sopno_logo.png",
            startDate: DateTime.Now.AddDays(180),
            endDate: DateTime.Now.AddDays(183),
            isSaved: false
        )
    ];
}