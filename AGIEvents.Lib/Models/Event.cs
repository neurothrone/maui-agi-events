namespace AGIEvents.Lib.Models;

// TODO: use a record?
public class Event
{
    public int id;
    public string eventId;
    public string title;
    public string image;
    public DateTime startDate;
    public DateTime endDate;
    public bool isSaved;

    public Event(
        int id,
        string eventId,
        string title,
        string image,
        DateTime startDate,
        DateTime endDate,
        bool isSaved
    )
    {
        this.id = id;
        this.eventId = eventId;
        this.title = title;
        this.image = image;
        this.startDate = startDate;
        this.endDate = endDate;
        this.isSaved = isSaved;
    }

    public static List<Event> Samples() =>
    [
        new Event(
            id: 0,
            eventId: "sopse295842",
            title: "Sign&Print Scandinavia",
            image: "sopse_logo.png",
            startDate: DateTime.Now.AddDays(100),
            endDate: DateTime.Now.AddDays(103),
            isSaved: true
        ),
        new Event(
            id: 1,
            eventId: "sopdk730956",
            title: "Sign Print & Pack Denmark",
            image: "sopdk_logo.png",
            startDate: DateTime.Now.AddDays(125),
            endDate: DateTime.Now.AddDays(127),
            isSaved: false
        ),
        new Event(
            id: 2,
            eventId: "sopno398628",
            title: "Sign, Print & Promotion",
            image: "sopno_logo.png",
            startDate: DateTime.Now.AddDays(150),
            endDate: DateTime.Now.AddDays(152),
            isSaved: false
        ),
        new Event(
            id: 3,
            eventId: "sopno398629",
            title: "Sign, Print & Promotion",
            image: "sopno_logo.png",
            startDate: DateTime.Now.AddDays(165),
            endDate: DateTime.Now.AddDays(168),
            isSaved: false
        ),
        new Event(
            id: 4,
            eventId: "sopno398630",
            title: "Sign, Print & Promotion",
            image: "sopno_logo.png",
            startDate: DateTime.Now.AddDays(172),
            endDate: DateTime.Now.AddDays(174),
            isSaved: false
        ),
        new Event(
            id: 5,
            eventId: "sopno398631",
            title: "Sign, Print & Promotion",
            image: "sopno_logo.png",
            startDate: DateTime.Now.AddDays(180),
            endDate: DateTime.Now.AddDays(183),
            isSaved: false
        )
    ];
}