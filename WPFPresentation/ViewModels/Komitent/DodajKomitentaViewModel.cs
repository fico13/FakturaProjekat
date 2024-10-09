using Application.DTOs;
using System.Windows.Input;
using WPFPresentation.Commands;
using WPFPresentation.Services;

namespace WPFPresentation.ViewModels.Komitent
{
    public class DodajKomitentaViewModel : BaseViewModel
    {
        private KomitentDTO? _komitent;
        public KomitentDTO Komitent
        {
            get
            {
                return _komitent!;
            }
            set
            {
                _komitent = value;
                OnPropertyChanged(nameof(Komitent));
            }
        }

        private ICommand _dodajKomitenta;
        public ICommand DodajKomitenta => _dodajKomitenta;

        private KomitentService _komitentService;

        public DodajKomitentaViewModel()
        {
            _komitentService = new KomitentService();
            Komitent = new KomitentDTO();
            _dodajKomitenta = new RelayCommand(async (obj) => await AddKomitent(obj));
        }

        private async Task AddKomitent(object obj)
        {
            await _komitentService.AddKomitent(Komitent);
            Komitent = new KomitentDTO();
        }
    }
}
