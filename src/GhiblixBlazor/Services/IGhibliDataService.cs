using GhiblixBlazor.Models;
using GhiblixShared.Models;

namespace GhiblixBlazor.Services;

public interface IGhibliDataService
{
    public Task<GhibliData?> LoadGhibliData();
    public Task<IEnumerable<int>> GetMovieReleaseYears();
    public Task<RuntimeWithScoreData> GetRuntimeWithScores(IEnumerable<int> years, int maxRuntime);
}