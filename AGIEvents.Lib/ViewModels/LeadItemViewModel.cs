using AGIEvents.Lib.Domain;

namespace AGIEvents.Lib.ViewModels;

public class LeadItemViewModel(
    int leadId,
    string eventId,
    string firstName,
    string lastName,
    string company,
    DateTime scannedDate)
{
    public int LeadId { get; } = leadId;
    public string EventId { get; } = eventId;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public string Company { get; } = company;
    public DateTime ScannedDate { get; } = scannedDate;

    public string FullName
    {
        get
        {
            return (string.IsNullOrWhiteSpace(FirstName), string.IsNullOrWhiteSpace(LastName)) switch
            {
                (false, false) => $"{FirstName} {LastName}",
                (true, false) => LastName,
                (false, true) => FirstName,
                _ => string.Empty
            };
        }
    }

    public static LeadItemViewModel FromLeadItemRecord(LeadItemRecordDto record)
    {
        return new LeadItemViewModel(
            record.LeadId,
            record.EventId,
            record.FirstName,
            record.LastName,
            record.Company,
            record.ScannedDate);
    }
}