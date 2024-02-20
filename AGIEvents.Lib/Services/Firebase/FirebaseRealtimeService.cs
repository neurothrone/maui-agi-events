using AGIEvents.Lib.Services.Realtime;
using System.Text.Json;
using AGIEvents.Lib.Services.Firebase.Domain;
using Firebase.Database;
using Firebase.Database.Query;

namespace AGIEvents.Lib.Services.Firebase;

public class FirebaseRealtimeService : IRealtimeService
{
    private readonly IConnectivity _connectivity;
    private readonly FirebaseClient _firebaseClient;
    private readonly bool _isBaseUrlValid;

    public FirebaseRealtimeService(
        IConfigurationService configurationService,
        IConnectivity connectivity)
    {
        _connectivity = connectivity;
        var firebaseUrl = configurationService.GetFirebaseUrl();
        _firebaseClient = new FirebaseClient(firebaseUrl);
        _isBaseUrlValid = !string.IsNullOrEmpty(firebaseUrl);
    }

    async Task<(Exhibitor? exhibitor, string? errorMessage)> IRealtimeService.FetchExhibitorById(
        string exhibitionId,
        string eventId) => await FetchParticipantById<Exhibitor>(exhibitionId, eventId);

    async Task<(Visitor? visitor,
        string? errorMessage)> IRealtimeService.FetchVisitorById(
        string exhibitionId,
        string eventId) => await FetchParticipantById<Visitor>(exhibitionId, eventId);


    private async Task<(T?, string? errorMessage)> FetchParticipantById<T>(
        string exhibitionId,
        string eventId) where T : Participant
    {
        if (!_isBaseUrlValid)
            return (null, "Invalid Firebase URL");

        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            return (null, "No Internet");

        var className = typeof(T).Name;

        var jsonString = await _firebaseClient
            .Child(eventId)
            .Child(className.ToLower())
            .Child(exhibitionId)
            .Child("data")
            .OnceAsJsonAsync();

        if (string.IsNullOrEmpty(jsonString) || jsonString.Equals("null"))
            return (null, "The scanned QR Code is not registered for this Event");

        var participant = JsonSerializer.Deserialize<T>(jsonString);
        if (participant is null)
            return (null, $"Failed to parse JSON for {className}");

        return (participant, null);
    }
}