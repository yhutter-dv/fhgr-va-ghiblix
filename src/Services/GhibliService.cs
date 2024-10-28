using Ghiblix.DTOs;

namespace Ghiblix.Services;

public class GhibliService(GhibliDataDto data)
{
    public IEnumerable<string> GetMovieTitles() => data.Movies.Select(m => m.Title);
    public IEnumerable<GhibliMovieDto> GetMoviesByTitle(string title) => data.Movies.Where(m => m.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
}