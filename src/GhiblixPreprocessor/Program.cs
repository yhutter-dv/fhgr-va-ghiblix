using CommandLine;
using CommandLine.Text;
using Figgle;
using GhiblixPreprocessor;

var result = await Parser.Default.ParseArguments<Options>(args).WithParsedAsync(async options =>
{
    Console.WriteLine(FiggleFonts.Standard.Render("Ghiblix Preprocessor"));
    var inputFile = new FileInfo(options.InputFilePath!);
    FileInfo outputFile;
    if (options.OutputFilePath != null)
    {
       outputFile = new FileInfo(options.OutputFilePath); 
    }
    else
    {
        // Construct output file path from input file path and append _preprocessed prefix.
        var nameWithoutExtension = Path.GetFileNameWithoutExtension(inputFile.FullName);
        var outputFilePathWithoutExtension = Path.Join(inputFile.DirectoryName, nameWithoutExtension);
        var outputFilePath = $"{outputFilePathWithoutExtension}_preprocessed{inputFile.Extension}";
        outputFile = new FileInfo(outputFilePath);
    }
    var defaultOutputFilePath = inputFile.FullName;
    var data = await Preprocessor.Preprocess(inputFile, outputFile, options.ShouldDownloadImages);
    Console.WriteLine(data == null ? "Failed to preprocess data." : "Preprocessing successfully.");
});
result.WithNotParsed(_ => HelpText.RenderUsageText(result));
