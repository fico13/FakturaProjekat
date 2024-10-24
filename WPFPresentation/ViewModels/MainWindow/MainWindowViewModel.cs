using System.Windows;
using System.Windows.Input;
using WPFPresentation.Commands;
using WPFPresentation.Navigation;
using WPFPresentation.Views.UserControls;
using WPFPresentation.Views.UserControls.Home;

namespace WPFPresentation.ViewModels.MainWindow
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;

        public ICommand MinimizeWindowCommand { get; }
        public ICommand ExitWindowCommand { get; }
        public ICommand HamburgerCommand { get; }
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateKomitentCommand { get; }
        public ICommand NavigateRobaCommand { get; }
        public ICommand NavigateFakturaCommand { get; }

        private Visibility _menuVisibility;
        public Visibility MenuVisibility
        {
            get => _menuVisibility;
            set
            {
                _menuVisibility = value;
                OnPropertyChanged(nameof(MenuVisibility));
            }
        }

        public MainWindowViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;

            MinimizeWindowCommand = new RelayCommand(MinimizeWindow);
            ExitWindowCommand = new RelayCommand(ExitWindow);
            HamburgerCommand = new RelayCommand(HamburgerButton);
            NavigateHomeCommand = new RelayCommand(_ => _navigationService.NavigateTo("Home"));
            NavigateKomitentCommand = new RelayCommand(_ => _navigationService.NavigateTo("UCKomitent"));
            NavigateRobaCommand = new RelayCommand(_ => _navigationService.NavigateTo("UCRoba"));
            NavigateFakturaCommand = new RelayCommand(_ => _navigationService.NavigateTo("UCFaktura"));

            MenuVisibility = Visibility.Collapsed;

            RegisterViews();
        }

        private void RegisterViews()
        {
            _navigationService.RegisterView("Home", () => new UCHome());
            _navigationService.RegisterView("UCKomitent", () => new UCKomitent());
            _navigationService.RegisterView("UCRoba", () => new UCRoba());
            _navigationService.RegisterView("UCFaktura", () => new UCFaktura());
        }

        private void HamburgerButton(object obj)
        {
            MenuVisibility = MenuVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void ExitWindow(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }

        private void MinimizeWindow(object obj)
        {
            if (obj is Window window)
            {
                window.WindowState = WindowState.Minimized;
            }
        }
    }
}
