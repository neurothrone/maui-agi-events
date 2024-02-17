namespace AGIEvents.Lib.Services.Database.DTO;

public record EventRecord(
    string EventId,
    string Title,
    string Image,
    DateTime StartDate,
    DateTime EndDate
);