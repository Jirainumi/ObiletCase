using System.Text.Json;
using System.Text.Json.Serialization;

namespace ObiletCase.Helpers;

/// <summary>
/// String veya int olarak gelen deðerleri int?'a çeviren converter
/// </summary>
public class FlexibleIntConverter : JsonConverter<int?>
{
    public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Number:
                return reader.GetInt32();
            
            case JsonTokenType.String:
                var stringValue = reader.GetString();
                if (string.IsNullOrEmpty(stringValue))
                    return null;
                
                if (int.TryParse(stringValue, out int result))
                    return result;
                
                return null;
            
            case JsonTokenType.Null:
                return null;
            
            default:
                throw new JsonException($"Cannot convert {reader.TokenType} to int?");
        }
    }

    public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteNumberValue(value.Value);
        else
            writer.WriteNullValue();
    }
}