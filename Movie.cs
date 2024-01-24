using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Database
{
    public class Movie
    {
        public static List<Movie> Registry = SaveSystem.LoadMovies();

        public string Title { get; set; }
        public string Director { get; set; } = string.Empty;
        public TimeSpan RuntimeSpan { get; set; }
        public float Rating { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly ReleaseDate { get; set; } = DateOnly.MinValue;
        public string Language { get; set; } = string.Empty;
        public string Budget { get; set; } = string.Empty;
        public string Runtime
        {
            get => RuntimeFormat();
            set => RuntimeSpan = ParseRuntimeString(value);
        }

        public Movie(string title, string director, TimeSpan timeSpan)
        {
            Title = title;
            Director = director;
            RuntimeSpan = timeSpan;
        }

        public Movie(string title, string director, int minutes)
        {
            Title = title;
            Director = director;
            RuntimeSpan = new TimeSpan(0, minutes, 0);
        }

        public Movie() { }

        public override string ToString()
        {
            if (RuntimeSpan.Hours > 0)
                return $"{Title}, Regisserad av {Director}, {RuntimeSpan.Hours} timmar och {RuntimeSpan.Minutes} minuter lång";
            else
                return $"{Title}, Regisserad av {Director}, {RuntimeSpan.Minutes} minuter lång";
        }

        public void AddMovie()
        {
            var movieIndex = Registry.FindIndex(movie => movie.Title == Title);

            if (movieIndex != -1)
                Registry[movieIndex] = this;
            else
                Registry.Add(this);

            MainWindow.Instance?.RegistryDataGrid.Items.Refresh();

            SaveSystem.SaveMovies();
        }

        public void RemoveMovie()
        {
            Registry.Remove(this);
            SaveSystem.SaveMovies();
        }

        private string RuntimeFormat()
        {
            int hours = RuntimeSpan.Hours;
            int minutes = RuntimeSpan.Minutes;

            if (hours > 0)
            {
                if (minutes > 0)
                {
                    return $"{hours} hours and {minutes} minutes";
                }
                else
                {
                    return $"{hours} hours";
                }
            }
            else
            {
                return $"{minutes} minutes";
            }
        }

        private static TimeSpan ParseRuntimeString(string formattedString)
        {
            string[] parts = formattedString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int hours = 0;
            int minutes = 0;

            for (int i = 0; i < parts.Length; i++)
            {
                if (i + 1 < parts.Length)
                {
                    if (parts[i + 1].Equals("hours", StringComparison.OrdinalIgnoreCase))
                    {
                        int.TryParse(parts[i], out hours);
                    }
                    else if (parts[i + 1].Equals("minutes", StringComparison.OrdinalIgnoreCase))
                    {
                        int.TryParse(parts[i], out minutes);
                    }
                }
            }

            return new TimeSpan(hours, minutes, 0);
        }
    }
}
