using Application.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFPresentation.Commands;
using WPFPresentation.Services;

namespace WPFPresentation.ViewModels.Komitent
{
    public class IzmeniKomitentaViewModel : BaseViewModel
    {
        private KomitentService _komitentService;

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

        private KomitentDTO? _selectedKomitent;
        public KomitentDTO? SelectedKomitent
        {
            get
            {
                return _selectedKomitent;
            }
            set
            {
                _selectedKomitent = value;
                OnPropertyChanged(nameof(SelectedKomitent));
            }
        }


        public ICommand FindKomitentCommand { get; set; }
        public ICommand UpdateKomitentCommand { get; set; }
        public ICommand DeleteKomitentCommand { get; set; }

        public IzmeniKomitentaViewModel()
        {
            _komitentService = new KomitentService();
            SelectedKomitent = new KomitentDTO();
            Komitenti = new ObservableCollection<KomitentDTO>();
            FindKomitentCommand = new RelayCommand(FindKomitents);
            UpdateKomitentCommand = new RelayCommand(UpdateKomitent);
            DeleteKomitentCommand = new RelayCommand(DeleteKomitent);
        }

        private async void DeleteKomitent(object obj)
        {
            await _komitentService.DeleteKomitent(SelectedKomitent!);
            SelectedKomitent = new KomitentDTO();
            FindKomitents(obj);
        }

        private async void UpdateKomitent(object obj)
        {
            await _komitentService.UpdateKomitent(SelectedKomitent!);
            SelectedKomitent = new KomitentDTO();
        }

        private async void FindKomitents(object obj)
        {
            var komitenti = await _komitentService.FindKomitents(SearchString!);
            Komitenti = new ObservableCollection<KomitentDTO>(komitenti);
        }
    }
}
