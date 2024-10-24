using Application.DTOs;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WPFPresentation.Views.UserControls;
using WPFPresentation.Views.UserControls.Komitent;

namespace WPFPresentation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MenuContent.Visibility = MenuContent.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            var image = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/faktura.jpg"))
            };
            ContentGrid.Content = image;
        }

        private void btnKomitent_Click(object sender, RoutedEventArgs e)
        {
            ContentGrid.Content = new UCKomitent(this);
        }

        private void btnRoba_Click(object sender, RoutedEventArgs e)
        {
            ContentGrid.Content = new UCRoba(this);
        }

        private void btnFaktura_Click(object sender, RoutedEventArgs e)
        {
            ContentGrid.Content = new UCFaktura(this);
        }

        internal void ShowUCIzmeniKomitenta(KomitentDTO komitent)
        {
            ContentGrid.Content = new UCIzmeniKomitenta(komitent);
        }
    }
}
