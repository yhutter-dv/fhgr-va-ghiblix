using Figgle;
using GhiblixPreprocessor.Utils;


// Small command line app for exploring Ghibli Movies for showcasing parsing the data
Console.WriteLine(FiggleFonts.Standard.Render("Ghiblix Preprocessor"));
var data = Preprocessor.Preprocess(new FileInfo("./Data/data.json"));

if (data == null)
{
    Console.WriteLine("Failed to preprocess data.");
}
else
{
    Console.WriteLine("Preprocessing successfully.");
}