using SQLite;

namespace AGIEvents.Lib.Services.Database.Domain;

[Table(nameof(EventEntity))]
public class EventEntity
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [PrimaryKey] [Column(nameof(EventId))] public string EventId { get; init; }

    [Column(nameof(Title))] public string Title { get; init; }
    [Column(nameof(Image))] public string Image { get; init; }
#pragma warning restore CS8618
    [Column(nameof(StartDate))] [Indexed] public DateTime StartDate { get; init; }
    [Column(nameof(EndDate))] public DateTime EndDate { get; init; }
}