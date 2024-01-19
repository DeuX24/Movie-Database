using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;

namespace Movie_Database
{
    internal static class SaveSystem
    {

        // Denna funktion använder JsonSerializer för att spara en lista med filmer till en fil.
        public static void SaveMovies()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateOnlyConverter());
            string json = JsonSerializer.Serialize(Movie.Registry, options);
            string directory = AppContext.BaseDirectory;
            const string fileName = "movies.json";
            string filePath = Path.Combine(directory, fileName);
            File.WriteAllText(filePath, json);
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
                var options = new JsonSerializerOptions();
                options.Converters.Add(new DateOnlyConverter());
                var movies = JsonSerializer.Deserialize<List<Movie>>(json, options);
                return movies ?? new List<Movie>();
            }
            else
            {
                return new List<Movie>();
            }
        }

        // Denna klass används för att konvertera DateOnly till och från JSON eftersom den inbyggda JsonSerializer inte stödjer DateOnly formatet.
        public class DateOnlyConverter : JsonConverter<DateOnly>
        {
            public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateOnly.FromDateTime(reader.GetDateTime());
            }

            public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
