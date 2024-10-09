using System.Windows.Controls;
using WPFPresentation.ViewModels.Roba;

namespace WPFPresentation.Views.UserControls.Roba
{
    /// <summary>
    /// Interaction logic for UCIzmeniRobu.xaml
    /// </summary>
    public partial class UCIzmeniRobu : UserControl
    {
        public UCIzmeniRobu()
        {
            InitializeComponent();
            DataContext = new IzmeniRobuViewModel();
        }
    }
}
