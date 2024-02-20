using AGIEvents.Lib.Services.Realtime;
using System.Text.Json;
using AGIEvents.Lib.Services.Firebase.Domain;
using Firebase.Database;
using Firebase.Database.Query;

namespace AGIEvents.Lib.Services.Firebase;

public class FirebaseRealtimeService : IRealtimeService
{
    private readonly IConnectivity _connectivity;
    private readonly INotificationService _notificationService;
    private readonly FirebaseClient _firebaseClient;
    private readonly bool _isBaseUrlValid;

    public FirebaseRealtimeService(
        IConfigurationService configurationService,
        IConnectivity connectivity,
        INotificationService notificationService)
    {
        _connectivity = connectivity;
        _notificationService = notificationService;

        var firebaseUrl = configurationService.GetFirebaseUrl();
        _firebaseClient = new FirebaseClient(firebaseUrl);
        _isBaseUrlValid = !string.IsNullOrEmpty(firebaseUrl);
    }

    // TODO: return Task<Exhibitor?, string errorMessage?>, Handle it from the caller of this service, not here

    async Task<Exhibitor?> IRealtimeService.FetchExhibitorById(string exhibitionId, string eventId)
    {
        var exhibitor = await FetchParticipantById<Exhibitor>(exhibitionId, eventId);
        return exhibitor;
    }

    async Task<Visitor?> IRealtimeService.FetchVisitorById(string exhibitionId, string eventId)
    {
        var visitor = await FetchParticipantById<Visitor>(exhibitionId, eventId);
        return visitor;
    }

    private async Task<T?> FetchParticipantById<T>(string exhibitionId, string eventId) where T : Participant
    {
        if (!_isBaseUrlValid)
        {
            await _notificationService.ShowNotificationAsync("Uh Oh!", "Invalid Firebase URL", "OK");
            return null;
        }

        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await _notificationService.ShowNotificationAsync("Uh Oh!", "No Internet", "OK");
            return null;
        }

        var className = typeof(T).Name;

        var jsonString = await _firebaseClient
            .Child(eventId)
            .Child(className.ToLower())
            .Child(exhibitionId)
            .Child("data")
            .OnceAsJsonAsync();

        if (string.IsNullOrEmpty(jsonString) || jsonString.Equals("null"))
        {
            await Shell.Current.DisplayAlert("Uh Oh!", "You are not registered for this Event", "OK");
            return null;
        }

        var participant = JsonSerializer.Deserialize<T>(jsonString);
        if (participant is null)
        {
            await Shell.Current.DisplayAlert("Uh Oh!", $"Failed to parse JSON for {className}", "OK");
            return null;
        }

        return participant;
    }
}