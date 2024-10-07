using System.Windows.Input;
using WPFPresentation.Commands;

namespace WPFPresentation.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel? _currentViewModel;
        public BaseViewModel? CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand KomitentCommand { get; set; }

        public MainWindowViewModel()
        {
            KomitentCommand = new RelayCommand(o => CurrentViewModel = new UCKomitentViewModel());
        }
    }
}
