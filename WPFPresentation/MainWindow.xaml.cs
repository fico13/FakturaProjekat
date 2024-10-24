using Application.DTOs;
using System.Windows;
using WPFPresentation.Navigation;
using WPFPresentation.ViewModels.Komitent;
using WPFPresentation.ViewModels.MainWindow;
using WPFPresentation.Views.UserControls;
using WPFPresentation.Views.UserControls.Home;
using WPFPresentation.Views.UserControls.Komitent;

namespace WPFPresentation
{
    public partial class MainWindow : Window
    {
        private readonly NavigationService _navigationService;

        public MainWindow()
        {
            InitializeComponent();
            _navigationService = new NavigationService(ContentGrid);
            DataContext = new MainWindowViewModel(_navigationService);
        }

        private void RegisterViews()
        {
            _navigationService.RegisterView("Home", () => new UCHome());
            _navigationService.RegisterView("UCKomitent", () => new UCKomitent());
            _navigationService.RegisterView("UCRoba", () => new UCRoba());
            _navigationService.RegisterView("UCFaktura", () => new UCFaktura());
        }

        public void ShowUCIzmeniKomitenta(KomitentDTO komitent)
        {
            var editControl = new UCIzmeniKomitenta();
            var editViewModel = new IzmeniKomitentaViewModel(komitent, _navigationService);
            editControl.DataContext = editViewModel;
            ContentGrid.Content = editControl;
        }
    }
}
