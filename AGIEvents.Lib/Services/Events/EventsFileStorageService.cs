using System.Text;
using System.Text.Json;
using AGIEvents.Lib.Services.Database.DTO;
using AGIEvents.Lib.Utility;

namespace AGIEvents.Lib.Services.Events;

public class EventsFileStorageService(Task<Stream> eventsStreamTask) : IEventsService
{
    private const string DateTimeFormat = "yyyy/MM/dd HH:mm";
    private EventRecord[]? _events;

    private async Task<EventRecord[]> ReadEventsFromStream()
    {
        var stream = await eventsStreamTask;

        string json;
        using (var reader = new StreamReader(stream, Encoding.UTF8))
        {
            json = await reader.ReadToEndAsync();
        }

        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonDateTimeFormatConverter(DateTimeFormat) }
        };

        var dictionary = JsonSerializer.Deserialize<Dictionary<string, EventRecord>>(json, serializeOptions);
        return dictionary?.Values.ToArray() ?? Array.Empty<EventRecord>();
    }


    async Task<EventRecord[]> IEventsService.LoadEvents()
    {
        _events ??= await ReadEventsFromStream();
        return _events.ToArray();
    }
}