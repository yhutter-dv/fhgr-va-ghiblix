using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using GhiblixShared.Models;

namespace GhiblixPreprocessor;

public static class Preprocessor
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
         WriteIndented = true,
         PropertyNameCaseInsensitive = true,
         NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    private static async Task DownloadImages(DirectoryInfo downloadDirectory, GhibliData data)
    {
        Directory.CreateDirectory(downloadDirectory.FullName);
        var imageDownloadTasks = data.Movies.Select(async movie =>
        {
            var imagePath = $"{Path.Join(downloadDirectory.FullName, movie.Title)}.png";
            using var httpClient = new HttpClient();
            // Download and save image
            var imageResponse = await httpClient.GetAsync(movie.Image);
            imageResponse.EnsureSuccessStatusCode();
            var imageBytes = await imageResponse.Content.ReadAsByteArrayAsync();
            Console.WriteLine($"Saving file {imagePath}");
            await File.WriteAllBytesAsync(imagePath, imageBytes);
            // Correct path to the image so that it points to the downloaded image path.
            // Important: We use relative paths here so that we do not have hardcoded absolute paths when we deploy to a site hosting service such
            // as GitHub Pages.
            movie.MovieBanner = Path.GetRelativePath(downloadDirectory.Parent!.FullName, imagePath);
        });
        // Download Images in parallel
        await Task.WhenAll(imageDownloadTasks);
    }

    public static async Task<GhibliData?> Preprocess(FileInfo inputFile, FileInfo outputFile, bool downloadImages)
    {
        if (!inputFile.Exists)
        {
            await Console.Error.WriteLineAsync($"Input File {inputFile.FullName} does not exist");
            return null;
        }

        if (outputFile.Exists)
        {
            // No need to do preprocessing just deserialize from existing already preprocessed file...
            Console.WriteLine($"Preprocessed File {outputFile.FullName} already exists using that...");
            var dataContent = outputFile.OpenRead();
            var data = JsonSerializer.Deserialize<GhibliData>(dataContent, JsonSerializerOptions);
            if (data == null)
            {
                await Console.Error.WriteLineAsync($"Failed to deserialize data from {outputFile.FullName}");
                return null;
            }

            if (!downloadImages) return data;
            
            // Download images inside a MovieImages Directory which is on the same level as the output file. 
            var downloadImageDirectory = new DirectoryInfo(Path.Join(outputFile.DirectoryName, "MovieImages"));
            Console.WriteLine("Downloading movie images...");
            await DownloadImages(downloadImageDirectory, data);
            return data;
        }
        try
        {
            Console.WriteLine($"Preprocessed File {outputFile.FullName} does not exist staring preprocessing and creating new one...");
            var dataContent = inputFile.OpenRead();
            var data = JsonSerializer.Deserialize<GhibliData>(dataContent, JsonSerializerOptions);
            if (data == null)
            {
                await Console.Error.WriteLineAsync($"Failed to deserialize data from {inputFile.FullName}");
                return null;
            }

            if (downloadImages)
            {
                // Download images inside a MovieImages Directory which is on the same level as the output file
                var downloadImageDirectory = new DirectoryInfo(Path.Join(outputFile.DirectoryName, "MovieImages"));
                Console.WriteLine("Downloading movie images...");
                await DownloadImages(downloadImageDirectory, data);
            }
            await File.WriteAllTextAsync(outputFile.FullName, JsonSerializer.Serialize(data, JsonSerializerOptions), encoding: Encoding.UTF8);
            return data;
        }
        catch (Exception e)
        {
            await Console.Error.WriteLineAsync($"ERROR occurred while preprocessing {inputFile.FullName}: {e}");
            return null;
        }
    }
    
}