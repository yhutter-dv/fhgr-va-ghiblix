using System.Text.Json.Serialization;
using GhiblixShared.Converters;

namespace GhiblixShared.Models;

public record GhibliPeople 
{
    [JsonPropertyName("id")] public string Id { get; init; } = "";
    [JsonPropertyName("name")] public string Name { get; init; } = "";
    [JsonPropertyName("gender")] public string Gender { get; init; } = "";
    
    [JsonConverter(typeof(StringToIntConverter))]
    [JsonPropertyName("age")] 
    public int Age { get; init; }
    [JsonPropertyName("eye_color")] public string EyeColor { get; init; } = "";
    [JsonPropertyName("hair_color")] public string HairColor { get; init; } = "";
}