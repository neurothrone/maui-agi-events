using AGIEvents.Lib.Domain;

namespace AGIEvents.Lib.ViewModels;

public partial class LeadItemViewModel
{
    public int LeadId { get; }
    public string EventId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Company { get; }
    public DateTime ScannedDate { get; }

    public string FullName => $"{FirstName} {LastName}";

    public LeadItemViewModel(
        int leadId,
        string eventId,
        string firstName,
        string lastName,
        string company,
        DateTime scannedDate)
    {
        LeadId = leadId;
        EventId = eventId;
        FirstName = firstName;
        LastName = lastName;
        Company = company;
        ScannedDate = scannedDate;
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