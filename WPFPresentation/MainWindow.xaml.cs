using System.Windows;
using WPFPresentation.Views.UserControls;

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
        }

        private void btnKomitent_Click(object sender, RoutedEventArgs e)
        {
            ContentGrid.Content = new UCKomitent(this);
        }

        private void btnRoba_Click(object sender, RoutedEventArgs e)
        {
            ContentGrid.Content = new UCRoba();
        }

        private void btnFaktura_Click(object sender, RoutedEventArgs e)
        {
            ContentGrid.Content = new UCFaktura();
        }
    }
}
