using AGIEvents.Lib.ViewModels;

namespace AGIEvents.Lib.Domain;

public record EventRecordDto(
    string EventId,
    string Title,
    string Image,
    DateTime StartDate,
    DateTime EndDate
)
{
    public static EventRecordDto FromViewModel(EventViewModel record)
    {
        return new EventRecordDto(
            record.EventId,
            record.Title,
            record.Image,
            record.StartDate,
            record.EndDate);
    }
};