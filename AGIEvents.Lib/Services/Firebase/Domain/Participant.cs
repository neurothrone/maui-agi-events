using System.Text.Json.Serialization;
using AGIEvents.Lib.Services.Firebase.Utility;

namespace AGIEvents.Lib.Services.Firebase.Domain;

public abstract class Participant
{
    [JsonPropertyName("exhibitionId")] public string ExhibitionId { get; init; }
    [JsonPropertyName("firstName")] public string FirstName { get; set; }
    [JsonPropertyName("lastName")] public string LastName { get; set; }
    [JsonPropertyName("company")] public string Company { get; set; }
    [JsonPropertyName("email")] public string Email { get; set; }

    [JsonConverter(typeof(PhoneJsonConverter))]
    [JsonPropertyName("phone")]
    public string Phone { get; set; }
}