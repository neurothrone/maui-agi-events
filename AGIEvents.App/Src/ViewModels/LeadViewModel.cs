namespace AGIEvents.App.ViewModels;

public partial class LeadViewModel(Models.Lead lead)
{
    public string Id => lead.id;
    public string EventId => lead.eventId;
    public string FirstName => lead.firstName;
    public string LastName => lead.lastName;
    public string FullName => $"{FirstName} {LastName}";
    public string Company => lead.company;
    public string Email => lead.email;
    public string Phone => lead.phone;
    public DateTime ScannedAt => lead.scannedAt;
}