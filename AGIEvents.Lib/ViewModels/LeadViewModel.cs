using AGIEvents.Lib.Models;

namespace AGIEvents.Lib.ViewModels;

public partial class LeadViewModel(Lead lead)
{
    public string Id => lead.Id;
    public string EventId => lead.EventId;
    public string FirstName => lead.FirstName;
    public string LastName => lead.LastName;
    public string FullName => $"{FirstName} {LastName}";
    public string Company => lead.company;
    public string Email => lead.email;
    public string Phone => lead.phone;
    public DateTime ScannedAt => lead.scannedAt;
}