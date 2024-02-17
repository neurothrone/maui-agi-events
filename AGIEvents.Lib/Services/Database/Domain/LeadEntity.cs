using SQLite;

namespace AGIEvents.Lib.Services.Database.Domain;

[Table("Leads")]
public class LeadEntity
{
    [PrimaryKey, AutoIncrement]
    [Column("LeadId")]
    public int LeadId { get; init; }

    [Column("EventId")] [Indexed] [Unique] public string EventId { get; init; }

    [Column("Title")] public string FirstName { get; init; }
    [Column("Title")] public string LastName { get; init; }
    [Column("Title")] public string Company { get; init; }
    [Column("Title")] public string Email { get; init; }
    [Column("Title")] public string Phone { get; init; }
    [Column("Title")] public string Address { get; init; }
    [Column("Title")] public string ZipCode { get; init; }
    [Column("Title")] public string City { get; init; }
    [Column("ScannedDate")] [Indexed] public DateTime ScannedDate { get; init; }
}