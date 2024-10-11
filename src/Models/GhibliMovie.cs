using System.Text.Json.Serialization;

namespace Ghiblix.Models;

public record GhibliMovie
{
    [JsonPropertyName("id")] public string Id { get; init; } = "";
    [JsonPropertyName("title")] public string Title { get; init; } = "";
    [JsonPropertyName("original_title")] public string OriginalTitle { get; init; } = "";
    [JsonPropertyName("original_title_romanised")] public string OriginalTitleRomanised { get; init; } = "";
    [JsonPropertyName("image")] public string Image { get; init; } = "";
    [JsonPropertyName("movie_banner")] public string MovieBanner { get; init; } = "";
    [JsonPropertyName("description")] public string Description { get; init; } = "";
    [JsonPropertyName("director")] public string Director { get; init; } = "";
    [JsonPropertyName("producer")] public string Producer { get; init; } = "";
    [JsonPropertyName("release_date")] public int Year { get; init; } = -1;
    [JsonPropertyName("running_time")] public int RunningTimeMinutes { get; init; } = 0;
    [JsonPropertyName("rt_score")] public int RottenTomatoScore { get; init; } = 0;
    [JsonPropertyName("people")] public List<string> PeopleIds { get; init; } = []; 
    [JsonPropertyName("species")] public List<string> SpeciesIds { get; init; } = [];
}