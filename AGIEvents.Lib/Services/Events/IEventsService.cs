using AGIEvents.Lib.Services.Database.DTO;

namespace AGIEvents.Lib.Services.Events;

// We will use this to load events from a file. Someday we might to load the events from Firebase and images from Storage
public interface IEventsService
{
    Task<EventRecord[]> LoadEvents();
}