using System.Net.Http.Json;
using GhiblixShared.Models;

namespace GhiblixBlazor.Services;

public class GhibliDataService(HttpClient httpClient) : IGhibliDataService
{
    private GhibliData? _ghibliData;

    public async Task<GhibliData?> LoadGhibliData()
    {
        if (_ghibliData != null)
        {
            return _ghibliData;
        }
        _ghibliData = await httpClient.GetFromJsonAsync<GhibliData>("data/data_preprocessed.json");
        if (_ghibliData != null && _ghibliData.Movies.Count != 0)
        {
            // Set the correct image path. Currently, the image paths is relative to the data_preprocessed.json file
            // However we need to prepend /data/ to the path in order to make it an absolute path in the blazor app.
            // Paths are resolved from wwwroot folder.
            _ghibliData.Movies = _ghibliData.Movies.Select(m =>
            {
                m.MovieBanner = Path.Join("data", m.MovieBanner);
                return m;
            }).OrderByDescending(x => x.Year).ToList();
        }
        
        return _ghibliData;
    }

    public async Task<IEnumerable<int>> GetMovieReleaseYears()
    {
        await LoadGhibliData();
        return _ghibliData?.Movies.Select(m => m.Year).Distinct() ?? Array.Empty<int>();
    }
}