namespace AGIEvents.Lib.Models;

// TODO: use a record?
public class Event
{
    public string id;
    public string title;
    public string image;
    public DateTime startDate;
    public DateTime endDate;

    public Event(
        string id,
        string title,
        string image,
        DateTime startDate,
        DateTime endDate
    )
    {
        this.id = id;
        this.title = title;
        this.image = image;
        this.startDate = startDate;
        this.endDate = endDate;
    }

    public static List<Event> Samples() =>
    [
        new Event(
            id: "0",
            title: "Sign&Print Scandinavia",
            image: "sopse_logo.png",
            startDate: DateTime.Now.AddDays(100),
            endDate: DateTime.Now.AddDays(103)
        ),
        new Event(
            id: "1",
            title: "Sign Print & Pack Denmark",
            image: "sopdk_logo.png",
            startDate: DateTime.Now.AddDays(125),
            endDate: DateTime.Now.AddDays(128)
        ),
        new Event(
            id: "2",
            title: "Sign, Print & Promotion",
            image: "sopno_logo.png",
            startDate: DateTime.Now.AddDays(150),
            endDate: DateTime.Now.AddDays(153)
        ),
        new Event(
            id: "3",
            title: "Sign, Print & Promotion",
            image: "sopno_logo.png",
            startDate: DateTime.Now.AddDays(150),
            endDate: DateTime.Now.AddDays(153)
        ),
        new Event(
            id: "4",
            title: "Sign, Print & Promotion",
            image: "sopno_logo.png",
            startDate: DateTime.Now.AddDays(150),
            endDate: DateTime.Now.AddDays(153)
        ),
        new Event(
            id: "5",
            title: "Sign, Print & Promotion",
            image: "sopno_logo.png",
            startDate: DateTime.Now.AddDays(150),
            endDate: DateTime.Now.AddDays(153)
        )
    ];
}