using System.Text.Json.Serialization;

namespace AGIEvents.Lib.Services.Firebase.Domain;

public class Visitor : Participant
{
    [JsonPropertyName("position")] public string Position { get; set; }
    [JsonPropertyName("addressField")] public string Address { get; set; }
    [JsonPropertyName("zipcode")] public string ZipCode { get; set; }
    [JsonPropertyName("city")] public string City { get; set; }
}