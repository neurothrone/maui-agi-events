using System.Text.Json.Serialization;

namespace AGIEvents.Lib.Services.Firebase.Domain;

public abstract class Participant
{
    [JsonPropertyName("exhibitionId")] public string ExhibitionId { get; init; }
    [JsonPropertyName("firstName")] public string FirstName { get; init; }
    [JsonPropertyName("lastName")] public string LastName { get; init; }
    [JsonPropertyName("company")] public string Company { get; init; }
    [JsonPropertyName("email")] public string Email { get; init; }
    [JsonPropertyName("phone")] public string Phone { get; init; }
}