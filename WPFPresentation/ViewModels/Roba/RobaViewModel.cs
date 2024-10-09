using Application.DTOs;
using System.Collections.ObjectModel;
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

        private RobaService _robaService;

        public RobaViewModel()
        {
            _robaService = new RobaService();
            LoadRoba();
        }

        private async void LoadRoba()
        {
            var roba = await _robaService.GetRoba();
            Roba = new ObservableCollection<RobaDTO>(roba);
        }
    }
}
