using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Database
{
    public class Movie
    {
        public static List<Movie> Registry = SaveSystem.LoadMovies();

        public string Title { get; set; }
        public string Director { get; set; }
        public TimeSpan Runtime { get; set; }
        public string RuntimeString => RuntimeFormat();

        public Movie(string title, string director, TimeSpan timeSpan)
        {
            Title = title;
            Director = director;
            Runtime = timeSpan;
        }

        public Movie(string title, string director, int minutes)
        {
            Title = title;
            Director = director;
            Runtime = new TimeSpan(0, minutes, 0);
        }

        public Movie() { }

        public override string ToString()
        {
            if (Runtime.Hours > 0)
                return $"{Title}, Regisserad av {Director}, {Runtime.Hours} timmar och {Runtime.Minutes} minuter lång";
            else
                return $"{Title}, Regisserad av {Director}, {Runtime.Minutes} minuter lång";
        }

        public void AddMovie()
        {
            Registry.Add(this);
            SaveSystem.SaveMovies();
        }

        public void RemoveMovie()
        {
            Registry.Remove(this);
            SaveSystem.SaveMovies();
        }

        private string RuntimeFormat()
        {
            int hours = Runtime.Hours;
            int minutes = Runtime.Minutes;

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
    }
}
