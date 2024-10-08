using System.Windows.Controls;
using WPFPresentation.ViewModels.Komitent;

namespace WPFPresentation.Views.UserControls.Komitent
{
    /// <summary>
    /// Interaction logic for UCNadjiKomitenta.xaml
    /// </summary>
    public partial class UCNadjiKomitenta : UserControl
    {
        public UCNadjiKomitenta()
        {
            InitializeComponent();
            DataContext = new NadjiKomitentaViewModel();
        }
    }
}
