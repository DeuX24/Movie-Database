using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Movie_Database
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow? Instance { get; private set; }
        public MainWindow()
        {
            InitializeComponent();

            Instance = this;

            // Movie.Registry.Add(new Movie("The Shawshank Redemption", "Frank Darabont", 142));

            RegistryDataGrid.ItemsSource = Movie.Registry;
        }


        // Saves the movies to a file and closes the application.
        public static void Exit()
        {
            SaveSystem.SaveMovies();
            Application.Current.Shutdown();
        }

        // When double-clicked, open a new window with the movie information.
        private void RegistryDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (RegistryDataGrid.SelectedItem is not Movie mov) return;

            var movieDataWin = new MovieInformationWindow(mov);

            movieDataWin.Show();

            movieDataWin.Focus();
        }

        private void BtnNewMovie_OnClick(object sender, RoutedEventArgs e)
        {
            var movieDataWin = new MovieInformationWindow(new Movie());

            movieDataWin.Show();

            movieDataWin.Focus();
        }

        private void BtnExit_OnClick(object sender, RoutedEventArgs e)
        {
            Exit();
        }
    }
}
