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

        private DokumentDTO? _selectedDokument;
        public DokumentDTO? SelectedDokument
        {
            get
            {
                return _selectedDokument;
            }
            set
            {
                _selectedDokument = value;
                OnPropertyChanged(nameof(SelectedDokument));
                ShowStavke();
            }
        }

        private ObservableCollection<StavkaDokumentaDTO>? _stavke;
        public ObservableCollection<StavkaDokumentaDTO>? Stavke
        {
            get
            {
                return _stavke;
            }
            set
            {
                _stavke = value;
                OnPropertyChanged(nameof(Stavke));
            }
        }

        public DokumentViewModel()
        {
            _dokumentService = new DokumentService();
            LoadData();
        }

        private async void LoadData()
        {
            var dokumenti = await _dokumentService.GetDokumenti();
            Dokumenti = new ObservableCollection<DokumentDTO>(dokumenti);
        }

        private void ShowStavke()
        {
            if (SelectedDokument != null && SelectedDokument.Stavke != null)
            {
                Stavke = new ObservableCollection<StavkaDokumentaDTO>(SelectedDokument.Stavke);
            }
            else
            {
                Stavke = new ObservableCollection<StavkaDokumentaDTO>();
            }
        }
    }
}
