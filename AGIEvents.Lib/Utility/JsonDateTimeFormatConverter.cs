using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AGIEvents.Lib.Utility;

public class JsonDateTimeFormatConverter(string format) : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString() ?? string.Empty;
        return DateTime.ParseExact(str, format, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(format, CultureInfo.InvariantCulture));
    }
}