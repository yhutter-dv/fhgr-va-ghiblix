using System.Text.Json.Serialization;
using GhiblixPreprocessor.Utils;
using GhiblixShared.Models;

namespace GhiblixPreprocessor.DTOs;

public record GhibliMovieDto 
{
    [JsonPropertyName("id")] public string Id { get; set; } = "";
    [JsonPropertyName("title")] public string Title { get; set; } = "";
    [JsonPropertyName("original_title")] public string OriginalTitle { get; set; } = "";
    [JsonPropertyName("original_title_romanised")] public string OriginalTitleRomanised { get; set; } = "";
    [JsonPropertyName("image")] public string Image { get; set; } = ""; // TODO: Download image automatically.
    [JsonPropertyName("movie_banner")] public string MovieBanner { get; set; } = ""; // TODO: Download image automatically.
    [JsonPropertyName("description")] public string Description { get; set; } = "";
    [JsonPropertyName("director")] public string Director { get; set; } = "";
    [JsonPropertyName("producer")] public string Producer { get; set; } = "";
    [JsonPropertyName("year")] public int Year { get; set; } = -1;
    [JsonPropertyName("running_time")] public int RunningTimeMinutes { get; set; } = 0;
    [JsonPropertyName("rt_score")] public int RottenTomatoScore { get; set; } = 0;
    [JsonPropertyName("people")] public List<string> PeopleIds { get; set; } = []; 
    [JsonPropertyName("species")] public List<string> SpeciesIds { get; set; } = [];

    public override string ToString()
    {
        return $"Title: {Title}, Original Title: {OriginalTitle}, Description: {Description}, People Ids [{string.Join(", ", PeopleIds)}], Species Ids [{string.Join(", ", SpeciesIds)}]";
    }

    public static GhibliMovieDto Preprocess(GhibliMovie ghibliMovie)
    {
       return new GhibliMovieDto()
       {
           Id = ghibliMovie.Id,
           Title = ghibliMovie.Title,
           OriginalTitle = ghibliMovie.OriginalTitle,
           OriginalTitleRomanised = ghibliMovie.OriginalTitleRomanised,
           Image = ghibliMovie.Image,
           MovieBanner = ghibliMovie.MovieBanner,
           Description = ghibliMovie.Description,
           Director = ghibliMovie.Director,
           Producer = ghibliMovie.Producer,
           Year = ghibliMovie.Year,
           RunningTimeMinutes = ghibliMovie.RunningTimeMinutes,
           RottenTomatoScore = ghibliMovie.RottenTomatoScore,
           // The original data had prefixes like 'https://ghibliapi.herokuapp.com.../<GUID> in a few places
           // We remove these prefixes simply leaving the GUIDs in place.
           PeopleIds = ghibliMovie.PeopleIds.Select(id => id.Replace($"{Constants.GhibliApiPrefixToRemove}/people/", string.Empty)).Where(id => !string.IsNullOrWhiteSpace(id)).ToList(),
           SpeciesIds = ghibliMovie.SpeciesIds.Select(id => id.Replace($"{Constants.GhibliApiPrefixToRemove}/species/", string.Empty)).Where(id => !string.IsNullOrWhiteSpace(id)).ToList()
       };
    }
}