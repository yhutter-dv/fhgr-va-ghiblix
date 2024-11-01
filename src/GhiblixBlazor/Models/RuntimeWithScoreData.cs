namespace GhiblixBlazor.Models;

public record RuntimeWithScoreData
{
   public IEnumerable<int> Years{ get; init; } 
   public IEnumerable<decimal?> AverageScores { get; init; } 
   public int RuntimeInMinutes { get; init; } 
}