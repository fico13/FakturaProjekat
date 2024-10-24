using Application.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFPresentation.Commands;
using WPFPresentation.Services;

namespace WPFPresentation.ViewModels.Komitent
{
    public class KomitentViewModel : BaseViewModel
    {
        private KomitentService _komitentService;
        //private MainWindow _mainWindow;

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

        private ObservableCollection<KomitentDTO>? _komitenti;
        public ObservableCollection<KomitentDTO>? Komitenti
        {
            get
            {
                return _komitenti;
            }
            set
            {
                _komitenti = value;
                OnPropertyChanged(nameof(Komitenti));
            }
        }

        private ICommand _findKomitentsCommand;
        public ICommand FindKomitentsCommand => _findKomitentsCommand;

        private ICommand _deleteKomitentCommand;
        public ICommand DeleteKomitentCommand => _deleteKomitentCommand;

        private ICommand _updateKomitentCommand;
        public ICommand UpdateKomitentCommand => _updateKomitentCommand;

        public KomitentViewModel()
        {
            _komitentService = new KomitentService();
            //_mainWindow = mainWindow;
            _findKomitentsCommand = new RelayCommand(async (obj) => await FindKomitents(obj));
            _updateKomitentCommand = new RelayCommand((obj) => UpdateKomitent(obj));
            _deleteKomitentCommand = new RelayCommand(async (obj) => await DeleteKomitent(obj));
        }

        private void UpdateKomitent(object obj)
        {
            //if (obj is KomitentDTO komitent)
            //{
            //    _mainWindow.Dispatcher.Invoke(() =>
            //    {
            //        _mainWindow.ShowUCIzmeniKomitenta(komitent);
            //    });
            //}
        }

        private async Task DeleteKomitent(object obj)
        {
            var successfull = await _komitentService.DeleteKomitent(((KomitentDTO)obj).SifraKomitenta!);
            if (successfull)
            {
                await FindKomitents(SearchString!);
            }
        }

        private async Task FindKomitents(object obj)
        {
            IEnumerable<KomitentDTO> komitenti = new List<KomitentDTO>();
            if (string.IsNullOrWhiteSpace(SearchString))
            {
                komitenti = await _komitentService.GetKomitents();
                Komitenti = new ObservableCollection<KomitentDTO>(komitenti);
                return;
            }
            komitenti = await _komitentService.FindKomitents(SearchString);
            Komitenti = new ObservableCollection<KomitentDTO>(komitenti);
        }
    }
}
