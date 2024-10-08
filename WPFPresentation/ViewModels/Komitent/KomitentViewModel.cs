using Application.DTOs;
using System.Collections.ObjectModel;
using WPFPresentation.Services;

namespace WPFPresentation.ViewModels.Komitent
{
    public class KomitentViewModel : BaseViewModel
    {
        private KomitentService _komitentService;
        public KomitentViewModel()
        {
            _komitentService = new KomitentService();
            LoadKomitents();
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

        public async void LoadKomitents()
        {
            IEnumerable<KomitentDTO> komitenti = await _komitentService.GetKomitents();
            Komitenti = new ObservableCollection<KomitentDTO>(komitenti);
        }
    }
}