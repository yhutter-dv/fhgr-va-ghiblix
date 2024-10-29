using Figgle;
using GhiblixPreprocessor;

Console.WriteLine(FiggleFonts.Standard.Render("Ghiblix Preprocessor"));
var data = Preprocessor.Preprocess(new FileInfo("./Data/data.json"));
Console.WriteLine(data == null ? "Failed to preprocess data." : "Preprocessing successfully.");