using AGIEvents.Lib.Services.Database.DTO;

namespace AGIEvents.Lib.ViewModels;

public partial class LeadViewModel
{
    public int LeadId { get; }
    public string EventId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Company { get; }
    public string Email { get; }
    public string Phone { get; }
    public DateTime ScannedDate { get; }

    public string FullName => $"{FirstName} {LastName}";

    public LeadViewModel(
        int leadId,
        string eventId,
        string firstName,
        string lastName,
        string company,
        string email,
        string phone,
        DateTime scannedDate)
    {
        LeadId = leadId;
        EventId = eventId;
        FirstName = firstName;
        LastName = lastName;
        Company = company;
        Email = email;
        Phone = phone;
        ScannedDate = scannedDate;
    }

    public static LeadViewModel FromRecord(LeadRecord record)
    {
        return new LeadViewModel(
            record.LeadId,
            record.EventId,
            record.FirstName,
            record.LastName,
            record.Company,
            record.Email,
            record.Phone,
            record.ScannedDate);
    }
}