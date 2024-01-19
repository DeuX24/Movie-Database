using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private List<HeaderAndText> headerAndTextList = new();

        public MovieInformationWindow(Movie movie)
        {
            InitializeComponent();

            var selectedMovie = movie;

            headerAndTextList = new();

            Type movieType = typeof(Movie);
            PropertyInfo[] properties = movieType.GetProperties();

            foreach (var property in properties)
            {
                if(property.Name == "RuntimeSpan")
                    continue;
                var header = property.Name;
                var text = property.GetValue(selectedMovie)?.ToString() ?? "N/A";

                headerAndTextList.Add(new HeaderAndText(header, text));
            }

            MovieInformationItemsControl.ItemsSource = headerAndTextList;

        }

        private void NewMovie()
        {
            Movie movie = new Movie();

            Type movieType = typeof(Movie);
            PropertyInfo[] movieProperties = movieType.GetProperties();

            foreach (var headerAndText in headerAndTextList)
            {
                PropertyInfo? matchingProperty = Array.Find(movieProperties, prop => prop.Name == headerAndText.Header);

                if (matchingProperty == null || !matchingProperty.CanWrite)
                {
                    continue;
                }

                switch(matchingProperty.PropertyType) 
                {
                    case Type t when t == typeof(string):
                        matchingProperty.SetValue(movie, headerAndText.Text);
                        break;
                    case Type t when t == typeof(float):
                        matchingProperty.SetValue(movie, float.Parse(headerAndText.Text));
                        break;
                    case Type t when t == typeof(int):
                        matchingProperty.SetValue(movie, int.Parse(headerAndText.Text));
                        break;
                    case Type t when t == typeof(TimeSpan):
                        matchingProperty.SetValue(movie, TimeSpan.Parse(headerAndText.Text));
                        break;
                    case Type t when t == typeof(DateOnly):
                        if (DateOnly.TryParse(headerAndText.Text, out DateOnly parsedValue))
                            matchingProperty.SetValue(movie, parsedValue);
                        else
                            matchingProperty.SetValue(movie, DateOnly.MinValue);
                        break;
                    default:
                        matchingProperty.SetValue(movie, Convert.ChangeType(headerAndText.Text, matchingProperty.PropertyType));
                        break;
                }
            }
            movie.AddMovie();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            NewMovie();
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
