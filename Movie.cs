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
        public TimeSpan TimeSpan { get; set; }

        public Movie(string title, string director, TimeSpan timeSpan)
        {
            Title = title;
            Director = director;
            TimeSpan = timeSpan;
        }

        public Movie(string title, string director, int minutes) 
        {
            Title = title;
            Director = director;
            TimeSpan = new TimeSpan(0, minutes, 0);
        }

        public override string ToString()
        {
            if(TimeSpan.Hours > 0)
                return $"{Title}, Regisserad av {Director}, {TimeSpan.Hours} timmar och {TimeSpan.Minutes} minuter lång";
            else
                return $"{Title}, Regisserad av {Director}, {TimeSpan.Minutes} minuter lång";
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
    }
}
