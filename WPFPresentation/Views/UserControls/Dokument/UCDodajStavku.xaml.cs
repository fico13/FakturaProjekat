using Application.DTOs;
using System.Windows.Controls;
using WPFPresentation.ViewModels.Dokument;

namespace WPFPresentation.Views.UserControls.Dokument
{
    /// <summary>
    /// Interaction logic for UCDodajStavku.xaml
    /// </summary>
    public partial class UCDodajStavku : UserControl
    {

        public UCDodajStavku(DokumentDTO selectedDokument)
        {
            DataContext = new DodajStavkuViewModel(selectedDokument);
        }
    }
}
