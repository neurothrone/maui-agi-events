using SQLite;

namespace AGIEvents.Lib.Services.Database.Domain;

[Table(nameof(EventEntity))]
public class EventEntity
{
    [PrimaryKey] [Column(nameof(EventId))] public string EventId { get; init; }

    [Column(nameof(Title))] public string Title { get; init; }
    [Column(nameof(Image))] public string Image { get; init; }
    [Column(nameof(StartDate))] [Indexed] public DateTime StartDate { get; init; }
    [Column(nameof(EndDate))] public DateTime EndDate { get; init; }
}