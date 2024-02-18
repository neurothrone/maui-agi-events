using System.Text;
using System.Text.Json;
using AGIEvents.Lib.Services.Database.DTO;

namespace AGIEvents.Lib.Services.Events;

public class EventsFileService(Task<Stream> eventsStreamTask) : IEventsService
{
    private EventRecord[]? _events = null;

    private async Task<EventRecord[]> ReadEventsFromStream()
    {
        var stream = await eventsStreamTask;

        string json = string.Empty;
        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        {
            json = reader.ReadToEnd();
        }

        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        return JsonSerializer.Deserialize<EventRecord[]>(json, serializeOptions) ?? [];
    }


    async Task<EventRecord[]> IEventsService.LoadEvents()
    {
        _events ??= await ReadEventsFromStream();
        return _events.ToArray();
    }
}