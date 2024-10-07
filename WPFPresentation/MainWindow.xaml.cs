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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentGrid.Content = new UCKomitent();

        }
    }
}
