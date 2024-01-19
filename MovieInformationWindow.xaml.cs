using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Movie_Database
{
    /// <summary>
    /// Interaction logic for MovieInformationWindow.xaml
    /// </summary>
    public partial class MovieInformationWindow : Window
    {
        public MovieInformationWindow(Movie movie)
        {
            InitializeComponent();

            var selectedMovie = movie;

            MovieInformationItemsControl.ItemsSource = new List<HeaderAndText>
            {
                new ("Title", selectedMovie.Title),
                new ("Director", selectedMovie.Director),
                new ("Runtime", selectedMovie.RuntimeString),
                new ("Rating", selectedMovie.Rating.ToString()),
                new ("Genre", selectedMovie.Genre),
                new ("Description", selectedMovie.Description),
                new ("ReleaseDate", selectedMovie.ReleaseDate.ToString()),
                new ("Language", selectedMovie.Language),
                new ("Budget", selectedMovie.Budget)
            };
        }
    }

    internal struct HeaderAndText
    {
        public string Header { get; set; }
        public string Text { get; set; }

        public HeaderAndText(string header, string text)
        {
            Header = header;
            Text = text;
        }
    }
}
