using Application.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFPresentation.Commands;
using WPFPresentation.Services;

namespace WPFPresentation.ViewModels.Roba
{
    public class RobaViewModel : BaseViewModel
    {
        private ObservableCollection<RobaDTO>? _roba;
        public ObservableCollection<RobaDTO>? Roba
        {
            get
            {
                return _roba;
            }
            set
            {
                _roba = value;
                OnPropertyChanged(nameof(Roba));
            }
        }

        private string? _searchString;
        public string? SearchString
        {
            get
            {
                return _searchString;
            }
            set
            {
                _searchString = value;
                OnPropertyChanged(nameof(SearchString));
            }
        }

        private RobaService _robaService;
        private readonly MainWindow _mainWindow;


        private ICommand _findRobaCommand;
        public ICommand FindRobaCommand => _findRobaCommand;

        private ICommand _deleteRobaCommand;
        public ICommand DeleteRobaCommand => _deleteRobaCommand;

        private ICommand _updateRobaCommand;
        public ICommand UpdateRobaCommand => _updateRobaCommand;

        public RobaViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _robaService = new RobaService();
            _findRobaCommand = new RelayCommand(async (obj) => await FindRoba(obj));
            _updateRobaCommand = new RelayCommand((obj) => UpdateRoba(obj));
            _deleteRobaCommand = new RelayCommand(async (obj) => await DeleteRoba(obj));
        }

        private async Task DeleteRoba(object obj)
        {
            var successfull = await _robaService.DeleteRoba(((RobaDTO)obj).SifraRobe!);
            if (successfull)
            {
                await FindRoba(SearchString!);
            }
        }

        private void UpdateRoba(object obj)
        {
            if (obj is RobaDTO roba)
            {
                _mainWindow.Dispatcher.Invoke(() =>
                {
                    _mainWindow.ShowUCIzmeniRobu(roba);
                });
            }
        }

        private async Task FindRoba(object obj)
        {
            IEnumerable<RobaDTO> roba = new List<RobaDTO>();
            if (string.IsNullOrWhiteSpace(SearchString))
            {
                roba = await _robaService.GetRoba();
                Roba = new ObservableCollection<RobaDTO>(roba);
                return;
            }
            roba = await _robaService.FindRoba(SearchString);
            Roba = new ObservableCollection<RobaDTO>(roba);
        }
    }
}
