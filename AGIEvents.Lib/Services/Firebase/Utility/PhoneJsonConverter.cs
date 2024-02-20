using System.Text.Json;
using System.Text.Json.Serialization;

namespace AGIEvents.Lib.Services.Firebase.Utility;

public class PhoneJsonConverter : JsonConverter<string>
{
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Check the incoming token type
        return reader.TokenType switch
        {
            JsonTokenType.Number => reader.GetInt32().ToString(),
            JsonTokenType.String => reader.GetString() ?? string.Empty,
            _ => string.Empty
            // _ => throw new JsonException()
        };
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}