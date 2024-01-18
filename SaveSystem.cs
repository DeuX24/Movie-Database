using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Movie_Database
{
    internal static class SaveSystem
    {

        // Denna funktion använder JsonSerializer för att spara en lista med filmer till en fil.
        public static void SaveMovies()
        {
            string json = JsonSerializer.Serialize(Movie.Registry);
            string path = AppContext.BaseDirectory;
            File.WriteAllText(path, json);
        }

        // Denna funktion använder JsonSerializer för att läsa en lista med filmer från en fil.
        public static List<Movie> LoadMovies()
        {
            string path = AppContext.BaseDirectory;
            string json = File.ReadAllText(path);
            var movies = JsonSerializer.Deserialize<List<Movie>>(json);
            return movies ?? new List<Movie>();
        }
    }
}
