using Application.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using WPFPresentation.Commands;
using WPFPresentation.Services;

namespace WPFPresentation.ViewModels.Dokument
{
    public class IzmeniDokumentViewModel : BaseViewModel
    {
        private KomitentService _komitentService;
        private DokumentService _dokumentService;

        private string? _brojDokumenta;
        public string? BrojDokumenta
        {
            get
            {
                return _brojDokumenta;
            }
            set
            {
                _brojDokumenta = value;
                OnPropertyChanged(nameof(BrojDokumenta));
            }
        }

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

        private StavkaDokumentaDTO? _selectedStavka;
        public StavkaDokumentaDTO? SelectedStavka
        {
            get
            {
                return _selectedStavka;
            }
            set
            {
                _selectedStavka = value;
                OnPropertyChanged(nameof(SelectedStavka));
                ShowStavka();
            }
        }

        private int _kolicina;
        public int Kolicina
        {
            get
            {
                return _kolicina;
            }
            set
            {
                _kolicina = value;
                OnPropertyChanged(nameof(Kolicina));
            }
        }

        private string? _validationText;
        public string? ValidationText
        {
            get
            {
                return _validationText;
            }
            set
            {
                _validationText = value;
                OnPropertyChanged(nameof(ValidationText));
            }
        }

        private Brush? _validationColor;
        public Brush? ValidationColor
        {
            get
            {
                return _validationColor;
            }
            set
            {
                _validationColor = value;
                OnPropertyChanged(nameof(ValidationColor));
            }
        }

        private ICommand _findDokumentCommand;
        public ICommand FindDokumentCommand => _findDokumentCommand;

        private ICommand _updateDokumentCommand;
        public ICommand UpdateDokumentCommand => _updateDokumentCommand;

        private ICommand _deleteDokumentCommand;
        public ICommand DeleteDokumentCommand => _deleteDokumentCommand;

        private ICommand _updateStavkaCommand;
        public ICommand UpdateStavkaCommand => _updateStavkaCommand;

        public IzmeniDokumentViewModel()
        {
            _komitentService = new KomitentService();
            _dokumentService = new DokumentService();
            _updateStavkaCommand = new RelayCommand(UpdateStavka);
            _findDokumentCommand = new RelayCommand(async (obj) => await FindDokument(obj));
            _deleteDokumentCommand = new RelayCommand(async (obj) => await DeleteDokument(obj));
            _updateDokumentCommand = new RelayCommand(async (obj) => await UpdateDokument(obj));
        }

        private void UpdateStavka(object obj)
        {
            if (SelectedStavka is null)
            {
                ValidationText = "Morate izabrati stavku";
                ValidationColor = Brushes.Red;
                return;
            }
            ValidationText = "";
            SelectedDokument!.Stavke!.Remove(SelectedStavka!);
            SelectedStavka!.Kolicina = Kolicina;
            SelectedStavka.UkupnaCenaStavke = SelectedStavka.Roba!.Cena * SelectedStavka.Kolicina;
            SelectedDokument!.Stavke!.Add(SelectedStavka);
            SelectedDokument!.UkupnaCena = SelectedDokument.Stavke!.Sum(s => s.UkupnaCenaStavke);

            Stavke = new ObservableCollection<StavkaDokumentaDTO>(SelectedDokument.Stavke);
        }

        private async Task DeleteDokument(object obj)
        {
            if (SelectedDokument is null)
            {
                ValidationText = "Morate izabrati dokument";
                ValidationColor = Brushes.Red;
                return;
            }
            await _dokumentService.DeleteDokument(SelectedDokument!.BrojDokumenta!);
            SelectedDokument = new DokumentDTO();
            await FindDokument(obj);
        }

        private async Task UpdateDokument(object obj)
        {
            if (SelectedDokument is null)
            {
                ValidationText = "Morate izabrati dokument";
                ValidationColor = Brushes.Red;
                return;
            }

            var successfull = await _dokumentService.UpdateDokument(SelectedDokument!);
            if (successfull)
            {
                ValidationText = "Uspesno izmenjen dokument";
                ValidationColor = Brushes.Green;
            }
            else
            {
                ValidationText = "Greska prilikom izmene dokumenta";
                ValidationColor = Brushes.Red;
            }
            SelectedDokument = new DokumentDTO();
            Kolicina = 1;
            Dokumenti = new ObservableCollection<DokumentDTO>();
        }

        private async Task FindDokument(object obj)
        {
            IEnumerable<DokumentDTO> dokumenti;
            if (string.IsNullOrWhiteSpace(BrojDokumenta))
            {
                dokumenti = await _dokumentService.GetDokumenti();
                Dokumenti = new ObservableCollection<DokumentDTO>(dokumenti);
            }
            else
            {
                dokumenti = await _dokumentService.FindDokuments(BrojDokumenta!);
                Dokumenti = new ObservableCollection<DokumentDTO>(dokumenti);
            }
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

        private void ShowStavka()
        {
            if (SelectedStavka != null)
            {
                Kolicina = SelectedStavka.Kolicina;
            }
        }
    }
}
