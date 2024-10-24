using Application.DTOs;
using System.Windows.Controls;
using WPFPresentation.ViewModels.Komitent;

namespace WPFPresentation.Views.UserControls.Komitent
{
    /// <summary>
    /// Interaction logic for UCIzmeniKomitenta.xaml
    /// </summary>
    public partial class UCIzmeniKomitenta : UserControl
    {
        public UCIzmeniKomitenta(KomitentDTO komitent)
        {
            InitializeComponent();
            DataContext = new IzmeniKomitentaViewModel(komitent);
        }
    }
}
