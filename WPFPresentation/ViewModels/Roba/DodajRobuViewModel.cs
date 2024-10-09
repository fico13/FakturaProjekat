using Application.DTOs;
using System.Windows.Input;
using WPFPresentation.Commands;
using WPFPresentation.Services;

namespace WPFPresentation.ViewModels.Roba
{
    public class DodajRobuViewModel : BaseViewModel
    {
        private RobaDTO? _roba;
        public RobaDTO? Roba
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

        private RobaService _robaService;

        private ICommand _dodajRobuCommand;
        public ICommand DodajRobuCommand => _dodajRobuCommand;

        public DodajRobuViewModel()
        {
            _robaService = new RobaService();
            Roba = new RobaDTO();
            _dodajRobuCommand = new RelayCommand(async (obj) => await DodajRobuAsync(obj));
        }

        private async Task DodajRobuAsync(object obj)
        {
            await _robaService.DodajRobu(Roba);
            Roba = new RobaDTO();
        }
    }
}
