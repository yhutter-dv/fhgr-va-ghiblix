using Figgle;
using Ghiblix.Services;
using Ghiblix.Utils;

// Small command line app for exploring Ghibli Movies for showcasing parsing the data
Console.WriteLine(FiggleFonts.Standard.Render("Ghiblix Data Explorer"));
var data = Preprocessor.Preprocess(new FileInfo("./Data/data.json"));

if (data == null)
{
    Console.WriteLine("No data found.");
}
else
{
    var ghibliService = new GhibliService(data);
    var movies = ghibliService.GetMovieTitles();
    Console.WriteLine($"Got the following movie titles\n{string.Join(Environment.NewLine, movies)}");
    Console.Write("Enter movie title (supports partial name matching, enter 0 to quit): ");

    string? movieTitle;
    while ((movieTitle = Console.ReadLine()) != "0")
    {
        if (movieTitle == null)
        {
            Console.WriteLine("Please enter a valid movie title.");
            Console.Write("Enter movie title (supports partial name matching, enter 0 to quit): ");
            continue;
        }

        var foundMovies = ghibliService.GetMoviesByTitle(movieTitle).ToList();
        if (foundMovies.Count() > 1)
        {
            Console.WriteLine($"Found multiple results for {movieTitle}:\n{string.Join(Environment.NewLine, foundMovies.Select(m => m.Title))}");
        }
        else if (foundMovies.Count() == 1)
        {
            var movie = foundMovies.FirstOrDefault();
            Console.WriteLine($"Found exactly one match for {movieTitle}");
            Console.WriteLine(movie);
        }
        else
        {
            Console.WriteLine($"No results found for {movieTitle}");
        }
        Console.Write("Enter movie title (supports partial name matching, enter 0 to quit): ");
    }
}