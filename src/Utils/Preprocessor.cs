using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Ghiblix.DTOs;
using Ghiblix.Models;

namespace Ghiblix.Utils;

public static class Preprocessor
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
         WriteIndented = true,
         PropertyNameCaseInsensitive = true,
         NumberHandling = JsonNumberHandling.AllowReadingFromString
    };
    private const string PreprocessSuffix = "preprocessed";

    public static GhibliDataDto? Preprocess(FileInfo dataFile)
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
        var force = true;
        if (preprocessedDataFile.Exists && !force)
        {
            // No need to do preprocessing just deserialize from existing already preprocessed file...
            Console.WriteLine($"Preprocessed File {preprocessedDataFile.FullName} already exists using that...");
            var dataContent = preprocessedDataFile.OpenRead();
            var data = JsonSerializer.Deserialize<GhibliDataDto>(dataContent, JsonSerializerOptions);
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
            var dataDto = GhibliDataDto.Preprocess(data);
            Console.WriteLine($"Finished Preprocessing File writing file {preprocessedFileName}");
            File.WriteAllText(preprocessedFileName, JsonSerializer.Serialize(dataDto, JsonSerializerOptions), encoding: Encoding.UTF8);
            return dataDto;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine($"ERROR occurred while preprocessing {dataFile.FullName}: {e}");
            return null;
        }
    }
    
}