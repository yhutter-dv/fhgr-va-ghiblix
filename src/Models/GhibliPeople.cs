using System.Text.Json.Serialization;

namespace Ghiblix.Models;

public record GhibliPeople
{
    [JsonPropertyName("id")] public string Id { get; init; } = "";
    [JsonPropertyName("name")] public string Name { get; init; } = "";
    [JsonPropertyName("gender")] public string Gender { get; init; } = "";
    [JsonPropertyName("age")] public string Age { get; init; } = "";
    [JsonPropertyName("eye_color")] public string EyeColor { get; init; } = "";
    [JsonPropertyName("hair_color")] public string HairColor { get; init; } = "";
}