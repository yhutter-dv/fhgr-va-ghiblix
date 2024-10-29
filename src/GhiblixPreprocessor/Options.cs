using CommandLine;

namespace GhiblixPreprocessor;

public record Options
{
    [Value(0, Required = true, HelpText = "File Path to the input data.")]
    public string? InputFilePath { get; set; }
    [Option('o', "output", Required = false, HelpText = "Output File Path where the preprocessed file should be stored.")]
    public string? OutputFilePath { get; set; }
    [Option('d', "download", Required = false, HelpText = "If the images for the movies should be downloaded or not")]
    public bool ShouldDownloadImages { get; set; } = true;
}