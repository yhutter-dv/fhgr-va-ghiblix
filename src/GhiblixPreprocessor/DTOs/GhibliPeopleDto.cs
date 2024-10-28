using System.Text.Json.Serialization;
using GhiblixShared.Models;

namespace GhiblixPreprocessor.DTOs;

public record GhibliPeopleDto
{
    [JsonPropertyName("id")] public string Id { get; init; } = "";
    [JsonPropertyName("name")] public string Name { get; init; } = "";
    [JsonPropertyName("gender")] public string Gender { get; init; } = "";
    [JsonPropertyName("age")] public int Age { get; init; } = -1; 
    [JsonPropertyName("eye_color")] public string EyeColor { get; init; } = "";
    [JsonPropertyName("hair_color")] public string HairColor { get; init; } = "";


    public override string ToString()
    {
        return $"Name: {Name}, Gender: {Gender}, Age: {Age}, EyeColor: {EyeColor}, HairColor: {HairColor}";
    }
    
    public static GhibliPeopleDto Preprocess(GhibliPeople ghibliPeople)
    {
        var age = -1;
        // Parsing the age can fail, it sometimes contains things like "Unspecified" etc.
        if (!int.TryParse(ghibliPeople.Age, out var parsedAge))
        {
            Console.Error.WriteLine($"Failed to parse Age {ghibliPeople.Age} of Person {ghibliPeople.Name}, assuming default age");
        }
        else
        {
            age = parsedAge;
        }

        return new GhibliPeopleDto
        {
            Id = ghibliPeople.Id,
            Name = ghibliPeople.Name,
            Gender = ghibliPeople.Gender,
            Age = age,
            EyeColor = ghibliPeople.EyeColor,
            HairColor = ghibliPeople.HairColor
        };
    }
}