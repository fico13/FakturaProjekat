using System.Windows.Controls;
using WPFPresentation.ViewModels.Komitent;
using WPFPresentation.Views.UserControls.Komitent;

namespace WPFPresentation.Views.UserControls
{
    /// <summary>
    /// Interaction logic for UCKomitent.xaml
    /// </summary>
    public partial class UCKomitent : UserControl
    {
        private MainWindow _mainWindow;
        public UCKomitent(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            DataContext = new KomitentViewModel(_mainWindow);
        }

        private void btnAddKomitent_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _mainWindow.ContentGrid.Content = new UCDodajKomitenta();
        }

    }
}
