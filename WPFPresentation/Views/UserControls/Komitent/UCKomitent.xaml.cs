using System.Windows.Controls;
using WPFPresentation.ViewModels.Komitent;

namespace WPFPresentation.Views.UserControls
{
    /// <summary>
    /// Interaction logic for UCKomitent.xaml
    /// </summary>
    public partial class UCKomitent : UserControl
    {
        //private MainWindow _mainWindow;
        public UCKomitent()
        {
            InitializeComponent();
            //_mainWindow = mainWindow;
            DataContext = new KomitentViewModel();
        }

        /*private void btnAddKomitent_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _mainWindow.ContentGrid.Content = new UCDodajKomitenta();
        }

        private void btnEditKomitent_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _mainWindow.ContentGrid.Content = new UCIzmeniKomitenta();
        }*/
    }
}
