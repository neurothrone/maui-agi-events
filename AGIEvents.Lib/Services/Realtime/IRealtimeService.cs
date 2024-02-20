using AGIEvents.Lib.Services.Firebase.Domain;

namespace AGIEvents.Lib.Services.Realtime;

public interface IRealtimeService
{
    Task<Exhibitor?> FetchExhibitorById(string exhibitionId, string eventId);
    Task<Visitor?> FetchVisitorById(string exhibitionId, string eventId);
}