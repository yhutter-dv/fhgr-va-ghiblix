using System.Text.Json.Serialization;
using Ghiblix.Models;

namespace Ghiblix.DTOs;

public record GhibliDataDto 
{
    
    [JsonPropertyName("films")]
    public List<GhibliMovieDto> Movies { get; set; } = [];
    
    [JsonPropertyName("people")]
    public List<GhibliPeopleDto> Peoples { get; set; } = [];

    public static GhibliDataDto Preprocess(GhibliData ghibliData)
    {
        List<GhibliMovieDto> movies = [];
        List<GhibliPeopleDto> peoples = [];
        foreach (var movie in ghibliData.Movies)
        {
            var movieDto = GhibliMovieDto.Preprocess(movie);
            movies.Add(movieDto);
        }
        foreach (var people in ghibliData.Peoples)
        {
            var peopleDto = GhibliPeopleDto.Preprocess(people);
            peoples.Add(peopleDto);
        }

        return new GhibliDataDto()
        {
            Movies = movies,
            Peoples = peoples
        };
    }
    
    
}