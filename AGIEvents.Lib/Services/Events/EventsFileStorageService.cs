using System.Text;
using System.Text.Json;
using AGIEvents.Lib.Domain;
using AGIEvents.Lib.Utility;

namespace AGIEvents.Lib.Services.Events;

public class EventsFileStorageService(Task<Stream> eventsStreamTask) : IEventsService
{
    private const string DateTimeFormat = "yyyy/MM/dd HH:mm";
    private EventRecordDto[]? _events;

    private async Task<EventRecordDto[]> ReadEventsFromStream()
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

        var dictionary = JsonSerializer.Deserialize<Dictionary<string, EventRecordDto>>(json, serializeOptions);
        return dictionary?.Values.ToArray() ?? Array.Empty<EventRecordDto>();
    }


    async Task<EventRecordDto[]> IEventsService.LoadEvents()
    {
        _events ??= await ReadEventsFromStream();
        return _events.ToArray();
    }
}