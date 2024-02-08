namespace AGIEvents.App.Models;

public class Lead
{
    public string id;
    public string eventId;
    public string firstName;
    public string lastName;
    public string company;
    public string email;
    public string phone;
    public string position;
    public string countryCode;
    public string address;
    public string zipCode;
    public string city;
    public string product;
    public string seller;
    public DateTime scannedAt;

    public Lead(
        string id,
        string eventId,
        string firstName,
        string lastName,
        string company,
        string email,
        string phone,
        DateTime scannedAt
    )
    {
        this.id = id;
        this.eventId = eventId;
        this.firstName = firstName;
        this.lastName = lastName;
        this.company = company;
        this.email = email;
        this.phone = phone;
        this.scannedAt = scannedAt;
    }

    public static List<Lead> Samples() =>
    [
        new Lead(
            id: "0",
            eventId: "0",
            firstName: "John",
            lastName: "Doe",
            company: "Doe Industries",
            email: "john.doe@example.com",
            phone: "+46 123 456 78 90",
            scannedAt: DateTime.Now
        ),
        new Lead(
            id: "1",
            eventId: "0",
            firstName: "Jane",
            lastName: "Doe",
            company: "Doe Industries",
            email: "jane.doe@example.com",
            phone: "+46 123 456 78 90",
            scannedAt: DateTime.Now
        ),
        new Lead(
            id: "1",
            eventId: "0",
            firstName: "Jane",
            lastName: "Doe",
            company: "Doe Industries",
            email: "jane.doe@example.com",
            phone: "+46 123 456 78 90",
            scannedAt: DateTime.Now
        ),
        new Lead(
            id: "1",
            eventId: "0",
            firstName: "Jane",
            lastName: "Doe",
            company: "Doe Industries",
            email: "jane.doe@example.com",
            phone: "+46 123 456 78 90",
            scannedAt: DateTime.Now
        ),
        new Lead(
            id: "1",
            eventId: "0",
            firstName: "Jane",
            lastName: "Doe",
            company: "Doe Industries",
            email: "jane.doe@example.com",
            phone: "+46 123 456 78 90",
            scannedAt: DateTime.Now
        ),
        new Lead(
            id: "1",
            eventId: "0",
            firstName: "Jane",
            lastName: "Doe",
            company: "Doe Industries",
            email: "jane.doe@example.com",
            phone: "+46 123 456 78 90",
            scannedAt: DateTime.Now
        ),
        new Lead(
            id: "1",
            eventId: "0",
            firstName: "Jane",
            lastName: "Doe",
            company: "Doe Industries",
            email: "jane.doe@example.com",
            phone: "+46 123 456 78 90",
            scannedAt: DateTime.Now
        ),
        new Lead(
            id: "1",
            eventId: "0",
            firstName: "Jane",
            lastName: "Doe",
            company: "Doe Industries",
            email: "jane.doe@example.com",
            phone: "+46 123 456 78 90",
            scannedAt: DateTime.Now
        ),
        new Lead(
            id: "1",
            eventId: "0",
            firstName: "Jane",
            lastName: "Doe",
            company: "Doe Industries",
            email: "jane.doe@example.com",
            phone: "+46 123 456 78 90",
            scannedAt: DateTime.Now
        ),
        new Lead(
            id: "1",
            eventId: "0",
            firstName: "Jane",
            lastName: "Doe",
            company: "Doe Industries",
            email: "jane.doe@example.com",
            phone: "+46 123 456 78 90",
            scannedAt: DateTime.Now
        ),
        new Lead(
            id: "1",
            eventId: "0",
            firstName: "Jane",
            lastName: "Doe",
            company: "Doe Industries",
            email: "jane.doe@example.com",
            phone: "+46 123 456 78 90",
            scannedAt: DateTime.Now
        ),
    ];
}