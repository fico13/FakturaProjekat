using System.Windows.Controls;
using WPFPresentation.ViewModels.Dokument;
using WPFPresentation.Views.UserControls.Dokument;

namespace WPFPresentation.Views.UserControls
{
    /// <summary>
    /// Interaction logic for UCFaktura.xaml
    /// </summary>
    public partial class UCFaktura : UserControl
    {
        private MainWindow _mainWindow;

        public UCFaktura(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            DataContext = new DokumentViewModel();
        }

        public UCFaktura()
        {

        }

        private void btnAddDokument_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _mainWindow.ContentGrid.Content = new UCDodajDokument();
        }

        private void btnEditDokument_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _mainWindow.ContentGrid.Content = new UCIzmeniDokument();
        }
    }
}
