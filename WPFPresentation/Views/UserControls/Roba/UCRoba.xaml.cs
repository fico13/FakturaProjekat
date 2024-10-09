using System.Windows;
using System.Windows.Controls;
using WPFPresentation.ViewModels.Roba;
using WPFPresentation.Views.UserControls.Roba;

namespace WPFPresentation.Views.UserControls
{
    /// <summary>
    /// Interaction logic for UCRoba.xaml
    /// </summary>
    public partial class UCRoba : UserControl
    {
        private MainWindow _mainWindow;

        public UCRoba(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            DataContext = new RobaViewModel();
        }

        private void btnAddRoba_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ContentGrid.Content = new UCDodajRobu();
        }

        private void btnEditRoba_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ContentGrid.Content = new UCIzmeniRobu();
        }
    }
}
