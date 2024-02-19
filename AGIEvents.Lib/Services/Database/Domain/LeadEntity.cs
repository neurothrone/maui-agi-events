using SQLite;

namespace AGIEvents.Lib.Services.Database.Domain;

[Table(nameof(LeadEntity))]
public class LeadEntity
{
    [PrimaryKey, AutoIncrement]
    [Column(nameof(LeadId))]
    public int LeadId { get; init; }

    [Column(nameof(EventId))]
    [Indexed]
    public string EventId { get; init; }

    [Column(nameof(FirstName))] public string FirstName { get; init; }
    [Column(nameof(LastName))] public string LastName { get; init; }
    [Column(nameof(Company))] public string Company { get; init; }
    [Column(nameof(Email))] public string Email { get; init; }
    [Column(nameof(Phone))] public string Phone { get; init; }
    [Column(nameof(Address))] public string Address { get; init; }
    [Column(nameof(ZipCode))] public string ZipCode { get; init; }
    [Column(nameof(City))] public string City { get; init; }
    [Column(nameof(Product))] public string Product { get; init; }
    [Column(nameof(Seller))] public string Seller { get; init; }

    // TODO: should we specify a MaxLength?
    // [Column(nameof(Notes)), MaxLength(10_000)] public string Notes { get; init; }
    [Column(nameof(Notes))] public string Notes { get; init; }

    [Column(nameof(ScannedDate))]
    [Indexed]
    public DateTime ScannedDate { get; init; }
}