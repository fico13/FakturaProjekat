using System.Windows.Controls;
using WPFPresentation.ViewModels.Komitent;

namespace WPFPresentation.Views.UserControls.Komitent
{
    /// <summary>
    /// Interaction logic for UCDodajKomitenta.xaml
    /// </summary>
    public partial class UCDodajKomitenta : UserControl
    {
        public UCDodajKomitenta()
        {
            InitializeComponent();
            DataContext = new DodajKomitentaViewModel();
        }
    }
}
