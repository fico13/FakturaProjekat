using Application.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFPresentation.Commands;
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

        private string? _serachBrojDokumenta;
        public string? SearchBrojDokumenta
        {
            get
            {
                return _serachBrojDokumenta;
            }
            set
            {
                _serachBrojDokumenta = value;
                OnPropertyChanged(nameof(SearchBrojDokumenta));
            }
        }

        private ICommand _findDokumentsCommand;
        public ICommand FindDokumentsCommand => _findDokumentsCommand;

        public DokumentViewModel()
        {
            _dokumentService = new DokumentService();
            _findDokumentsCommand = new RelayCommand(async (obj) => await FindDokuments(obj));
        }

        private async Task FindDokuments(object obj)
        {
            IEnumerable<DokumentDTO> dokumenti = new List<DokumentDTO>();
            if (string.IsNullOrWhiteSpace(SearchBrojDokumenta))
            {
                dokumenti = await _dokumentService.GetDokumenti();
                Dokumenti = new ObservableCollection<DokumentDTO>(dokumenti);
                return;
            }
            dokumenti = await _dokumentService.FindDokuments(SearchBrojDokumenta);
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
