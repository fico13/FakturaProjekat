using Application.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFPresentation.Commands;
using WPFPresentation.Services;

namespace WPFPresentation.ViewModels.Dokument
{
    public class IzmeniDokumentViewModel : BaseViewModel
    {
        private KomitentService _komitentService;
        private DokumentService _dokumentService;

        private ObservableCollection<KomitentDTO>? _komitent;
        public ObservableCollection<KomitentDTO>? Komitenti
        {
            get
            {
                return _komitent;
            }
            set
            {
                _komitent = value;
                OnPropertyChanged(nameof(Komitenti));
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

        private string? _kolicina;
        public string? Kolicina
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
            Komitenti = new ObservableCollection<KomitentDTO>();
            Dokumenti = new ObservableCollection<DokumentDTO>();
            Stavke = new ObservableCollection<StavkaDokumentaDTO>();
            SelectedKomitent = new KomitentDTO();
            SelectedDokument = new DokumentDTO();
            SelectedStavka = new StavkaDokumentaDTO();
            _updateStavkaCommand = new RelayCommand(UpdateStavka);
            _findDokumentCommand = new RelayCommand(async (obj) => await FindDokument(obj));
            _deleteDokumentCommand = new RelayCommand(async (obj) => await DeleteDokument(obj));
            _updateDokumentCommand = new RelayCommand(async (obj) => await UpdateDokument(obj));
            LoadKomitents();
        }

        private void UpdateStavka(object obj)
        {
            //if (SelectedStavka!.Roba)
            //{
            //    ValidationText = "Morate izabrati stavku";
            //    return;
            //}
            try
            {
                SelectedDokument!.Stavke!.Remove(SelectedStavka!);
                SelectedStavka!.Kolicina = int.Parse(Kolicina!);
                //SelectedStavka.UkupnaCenaStavke = SelectedStavka.ce * SelectedStavka.Kolicina;
                SelectedDokument!.Stavke!.Add(SelectedStavka);
                SelectedDokument!.UkupnaCena = SelectedDokument.Stavke!.Sum(s => s.UkupnaCenaStavke);
                Stavke = new ObservableCollection<StavkaDokumentaDTO>(SelectedDokument.Stavke);
            }
            catch (FormatException)
            {
                ValidationText = "Količina mora biti broj veći od 0";
                return;
            }
        }

        private async Task DeleteDokument(object obj)
        {
            //if (SelectedDokument!.Id == 0)
            //{
            //    ValidationText = "Morate izabrati dokument";
            //    return;
            //}
            //await _dokumentService.DeleteDokument(SelectedDokument!.BrojDokumenta);
            SelectedDokument = new DokumentDTO();
            await FindDokument(obj);
        }

        private async Task UpdateDokument(object obj)
        {
            //if (SelectedDokument!.Id == 0)
            //{
            //    ValidationText = "Morate izabrati dokument";
            //    return;
            //}
            await _dokumentService.UpdateDokument(SelectedDokument!);
            SelectedDokument = new DokumentDTO();
            Dokumenti = new ObservableCollection<DokumentDTO>();
        }

        private async Task FindDokument(object obj)
        {
            //if (SelectedKomitent!.Id == 0)
            //{
            //    ValidationText = "Morate izabrati komitenta";
            //    return;
            //}
            var dokumenti = await _dokumentService.FindDokuments(SelectedKomitent!.Naziv!);
            Dokumenti = new ObservableCollection<DokumentDTO>(dokumenti);

        }

        private async void LoadKomitents()
        {
            var komitenti = await _komitentService.GetKomitents();
            Komitenti = new ObservableCollection<KomitentDTO>(komitenti);
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
                Kolicina = SelectedStavka.Kolicina.ToString();
            }
        }
    }
}
