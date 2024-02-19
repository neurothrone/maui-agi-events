namespace AGIEvents.Lib.Services.Database.DTO;

public record LeadRecord(
    string EventId,
    string FirstName,
    string LastName,
    string Company,
    string Email,
    string Phone,
    string Address,
    string ZipCode,
    string City,
    string Product,
    string Seller,
    string Notes,
    DateTime ScannedDate,
    int LeadId = -1
);