using Application.DTOs;
using System.Collections.ObjectModel;
using WPFPresentation.Services;

namespace WPFPresentation.ViewModels.Dokument
{
    public class DokumentViewModel : BaseViewModel
    {
        private DokumentService _dokumentService;

        private ObservableCollection<DokumentDTO>? _dokumenti;
        public ObservableCollection<DokumentDTO>? Dokumenti
        {
            get
            {
                return _dokumenti;
            }
            set
            {
                _dokumenti = value;
                OnPropertyChanged(nameof(Dokumenti));
            }
        }

        public DokumentViewModel()
        {
            Dokumenti = new ObservableCollection<DokumentDTO>();
            _dokumentService = new DokumentService();
            LoadData();
        }

        private async void LoadData()
        {
            var dokumenti = await _dokumentService.GetDokumenti();
            Dokumenti = new ObservableCollection<DokumentDTO>(dokumenti);
        }
    }
}
