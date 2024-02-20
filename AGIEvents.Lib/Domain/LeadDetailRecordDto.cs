namespace AGIEvents.Lib.Domain;

public record LeadDetailRecordDto(
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
) : ILeadHashable
{
    public string FormattedScannedDate => ScannedDate.ToString("HH:mm ddd dd MMM yyyy");
}