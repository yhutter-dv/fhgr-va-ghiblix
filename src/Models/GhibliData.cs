using System.Text.Json.Serialization;

namespace Ghiblix.Models;

public record GhibliData
{
    [JsonPropertyName("films")]
    public List<GhibliMovie> Movies { get; init; } = [];
    
    [JsonPropertyName("people")]
    public List<GhibliPeople> Peoples { get; init; } = [];
}