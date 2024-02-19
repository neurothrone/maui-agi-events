using AGIEvents.Lib.ViewModels;

namespace AGIEvents.Lib.Services.Database.DTO;

public record EventRecord(
    string EventId,
    string Title,
    string Image,
    DateTime StartDate,
    DateTime EndDate
)
{
    public static EventRecord FromViewModel(EventViewModel record)
    {
        return new EventRecord(
            record.EventId,
            record.Title,
            record.Image,
            record.StartDate,
            record.EndDate);
    }
};