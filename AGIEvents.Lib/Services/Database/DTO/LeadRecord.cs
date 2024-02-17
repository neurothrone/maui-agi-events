namespace AGIEvents.Lib.Services.Database.DTO;

public record LeadRecord(
    int LeadId,
    string EventId,
    string FirstName,
    string LastName,
    string Company,
    string Email,
    string Phone,
    string Address,
    string ZipCode,
    string City,
    DateTime ScannedDate
);