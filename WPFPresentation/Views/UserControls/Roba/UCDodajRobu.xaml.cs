using System.Windows.Controls;
using WPFPresentation.ViewModels.Roba;

namespace WPFPresentation.Views.UserControls.Roba
{
    /// <summary>
    /// Interaction logic for UCDodajRobu.xaml
    /// </summary>
    public partial class UCDodajRobu : UserControl
    {
        public UCDodajRobu()
        {
            InitializeComponent();
            DataContext = new DodajRobuViewModel();
        }
    }
}
