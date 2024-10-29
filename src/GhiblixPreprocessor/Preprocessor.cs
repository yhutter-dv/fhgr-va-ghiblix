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
    private const string PreprocessSuffix = "preprocessed";

    public static async Task DownloadGhibliImages(DirectoryInfo movieBannerDirectory, DirectoryInfo movieImageDirectory, GhibliData data)
    {
        Directory.CreateDirectory(movieBannerDirectory.FullName);
        Directory.CreateDirectory(movieImageDirectory.FullName);
        var imagePaths = data.Movies.Select(m =>
        (
            m.Title,
            m.MovieBanner,
            m.Image
        ));

        var imageDownloadTasks = imagePaths.Select(async imagePath =>
        {
            var movieBannerPath = $"{Path.Join(movieBannerDirectory.FullName, imagePath.Title)}.png";
            var movieImagePath = $"{Path.Join(movieImageDirectory.FullName, imagePath.Title)}.png";

            using var httpClient = new HttpClient();

            // Download and save movie banner
            var bannerResponse = await httpClient.GetAsync(imagePath.MovieBanner);
            bannerResponse.EnsureSuccessStatusCode();
            var bannerBytes = await bannerResponse.Content.ReadAsByteArrayAsync();
            Console.WriteLine($"Saving file {movieBannerPath}");
            await File.WriteAllBytesAsync(movieBannerPath, bannerBytes);

            // Download and save movie image
            var imageResponse = await httpClient.GetAsync(imagePath.Image);
            imageResponse.EnsureSuccessStatusCode();
            var imageBytes = await imageResponse.Content.ReadAsByteArrayAsync();
            Console.WriteLine($"Saving file {movieImagePath}");
            await File.WriteAllBytesAsync(movieImagePath, imageBytes);
        });
        await Task.WhenAll(imageDownloadTasks);
    }

    public static GhibliData? Preprocess(FileInfo dataFile)
    {
        if (!dataFile.Exists)
        {
            Console.Error.WriteLine($"File {dataFile.FullName} does not exist");
            return null;
        }
        
        var nameWithoutExtension = Path.GetFileNameWithoutExtension(dataFile.FullName);
        var preprocessedFullPathWithoutExtension = Path.Join(dataFile.DirectoryName, nameWithoutExtension);
        var preprocessedFileName = $"{preprocessedFullPathWithoutExtension}_{PreprocessSuffix}{dataFile.Extension}";
        var preprocessedDataFile = new FileInfo(preprocessedFileName);
        if (preprocessedDataFile.Exists)
        {
            // No need to do preprocessing just deserialize from existing already preprocessed file...
            Console.WriteLine($"Preprocessed File {preprocessedDataFile.FullName} already exists using that...");
            var dataContent = preprocessedDataFile.OpenRead();
            var data = JsonSerializer.Deserialize<GhibliData>(dataContent, JsonSerializerOptions);
            return data;
        }
        try
        {
            Console.WriteLine($"Preprocessed File {preprocessedFileName} does not exist staring preprocessing and creating new one...");
            var dataContent = dataFile.OpenRead();
            var data = JsonSerializer.Deserialize<GhibliData>(dataContent, JsonSerializerOptions);
            if (data == null)
            {
                Console.Error.WriteLine($"Failed to deserialize data from {dataFile.FullName}");
                return null;
            }
            File.WriteAllText(preprocessedFileName, JsonSerializer.Serialize(data, JsonSerializerOptions), encoding: Encoding.UTF8);
            return data;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"ERROR occurred while preprocessing {dataFile.FullName}: {e}");
            return null;
        }
    }
    
}