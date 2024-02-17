using SQLite;

namespace AGIEvents.Lib.Services.Database.Domain;

[Table("Events")]
public class EventEntity
{
    [PrimaryKey] [Column("EventId")] public string EventId { get; init; }

    [Column("Title")] public string Title { get; init; }
    [Column("Image")] public string Image { get; init; }
    [Column("StartDate")] [Indexed] public DateTime StartDate { get; init; }
    [Column("EndDate")] public DateTime EndDate { get; init; }
}