using AGIEvents.Lib.Services.Firebase.Domain;

namespace AGIEvents.Lib.Services.Realtime;

public interface IRealtimeService
{
    Task<(Exhibitor? exhibitor, string? errorMessage)> FetchExhibitorById(
        string exhibitionId,
        string eventId);

    Task<(Visitor? visitor, string? errorMessage)> FetchVisitorById(
        string exhibitionId,
        string eventId);
}