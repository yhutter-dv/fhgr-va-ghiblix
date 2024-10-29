using System.Text.Json;
using System.Text.Json.Serialization;

namespace GhiblixShared.Converters;

public class StringToIntConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringValue = "";
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                stringValue = reader.GetString();
                break;
            case JsonTokenType.Number:
                stringValue = reader.GetInt32().ToString();
                break;
            default:
                Console.WriteLine($"Unexpected token type {reader.TokenType} when parsing string to int");
                break;
        }
        const int defaultValue = -1;
        return !int.TryParse(stringValue, out var parsedInt) ? defaultValue : parsedInt;
        
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}