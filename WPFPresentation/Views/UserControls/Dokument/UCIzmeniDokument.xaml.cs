using Application.DTOs;
using System.Windows.Controls;
using WPFPresentation.ViewModels.Dokument;

namespace WPFPresentation.Views.UserControls.Dokument
{
    /// <summary>
    /// Interaction logic for UCIzmeniDokument.xaml
    /// </summary>
    public partial class UCIzmeniDokument : UserControl
    {
        public UCIzmeniDokument(DokumentDTO dokument)
        {
            InitializeComponent();
            DataContext = new IzmeniDokumentViewModel(dokument);
        }
    }
}
