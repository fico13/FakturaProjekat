using System.Windows.Controls;
using WPFPresentation.ViewModels.Dokument;

namespace WPFPresentation.Views.UserControls.Dokument
{
    /// <summary>
    /// Interaction logic for UCDodajDokument.xaml
    /// </summary>
    public partial class UCDodajDokument : UserControl
    {
        public UCDodajDokument()
        {
            InitializeComponent();
            DataContext = new DodajDokumentViewModel();
        }
    }
}
