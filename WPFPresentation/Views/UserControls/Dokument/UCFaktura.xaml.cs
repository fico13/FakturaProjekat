using System.Windows.Controls;
using WPFPresentation.ViewModels.Dokument;

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
    }
}
