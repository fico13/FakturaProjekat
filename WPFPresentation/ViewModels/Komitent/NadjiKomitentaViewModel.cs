using Application.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFPresentation.Commands;
using WPFPresentation.Services;

namespace WPFPresentation.ViewModels.Komitent
{
    public class NadjiKomitentaViewModel : BaseViewModel
    {
        private KomitentService _komitentService;
        private string? _searchString;
        public string? SearchString
        {
            get
            {
                return _searchString!;
            }
            set
            {
                _searchString = value;
                OnPropertyChanged(nameof(SearchString));
            }
        }

        private ICommand _findKomitentCommand;
        public ICommand FindKomitentCommand => _findKomitentCommand;

        private ObservableCollection<KomitentDTO>? _komitenti;
        public ObservableCollection<KomitentDTO> Komitenti
        {
            get
            {
                return _komitenti!;
            }
            set
            {
                _komitenti = value;
                OnPropertyChanged(nameof(Komitenti));
            }
        }

        public NadjiKomitentaViewModel()
        {
            _komitentService = new KomitentService();
            Komitenti = new ObservableCollection<KomitentDTO>();
            _findKomitentCommand = new RelayCommand(FindKomitent);
        }

        private async void FindKomitent(object obj)
        {
            IEnumerable<KomitentDTO> komitenti = await _komitentService.FindKomitents(SearchString!);
            Komitenti = new ObservableCollection<KomitentDTO>(komitenti);
        }
    }
}
