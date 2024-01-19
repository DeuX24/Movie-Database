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

        // Denna funktion läser in en fil och använder JsonSerializer för att avkoda textinnehållet till Movie klassen.
        public static List<Movie> LoadMovies()
        {
            string directory = AppContext.BaseDirectory;
            const string fileName = "movies.json";
            string filePath = Path.Combine(directory, fileName);
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var movies = JsonSerializer.Deserialize<List<Movie>>(json);
                return movies ?? new List<Movie>();
            }
            else
            {
                return new List<Movie>();
            }
        }
    }
}
