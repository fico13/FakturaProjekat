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
    }
}
