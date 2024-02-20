using System.Text.Json.Serialization;

namespace AGIEvents.Lib.Services.Firebase.Domain;

public class Visitor : Participant
{
    [JsonPropertyName("position")] public string Position { get; set; }
    [JsonPropertyName("addressField")] public string Address { get; set; }
    [JsonPropertyName("zipcode")] public string ZipCode { get; set; }
    [JsonPropertyName("city")] public string City { get; set; }

    public static Visitor FromExhibitor(Exhibitor exhibitor)
    {
        var visitor = new Visitor
        {
            FirstName = exhibitor.FirstName,
            LastName = exhibitor.LastName,
            Company = exhibitor.Company,
            Email = exhibitor.Email,
            Phone = exhibitor.Phone,
        };
        return visitor;
    }
}