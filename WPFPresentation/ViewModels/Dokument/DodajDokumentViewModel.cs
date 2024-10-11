using Application.DTOs;
using FluentValidation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFPresentation.Commands;
using WPFPresentation.Services;
using WPFPresentation.Validators;

namespace WPFPresentation.ViewModels.Dokument
{
    public class DodajDokumentViewModel : BaseViewModel
    {
        private KomitentService _komitentService;
        private RobaService _robaService;
        private DokumentService _dokumentService;
        private IValidator<DokumentDTO> _dokumentValidator;
        private IValidator<StavkaDokumentaDTO> _stavkaValidator;

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

        private RobaDTO? _selectedRoba;
        public RobaDTO? SelectedRoba
        {
            get
            {
                return _selectedRoba;
            }
            set
            {
                _selectedRoba = value;
                OnPropertyChanged(nameof(SelectedRoba));
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

        private string? _ukupnaCena;
        public string? UkupnaCena
        {
            get
            {
                return _ukupnaCena;
            }
            set
            {
                _ukupnaCena = value;
                OnPropertyChanged(nameof(UkupnaCena));
            }
        }

        private string? _stavkaValidation;
        public string? StavkaValidation
        {
            get
            {
                return _stavkaValidation;
            }
            set
            {
                _stavkaValidation = value;
                OnPropertyChanged(nameof(StavkaValidation));
            }
        }

        private string? _dokumentValidation;
        public string? DokumentValidation
        {
            get
            {
                return _dokumentValidation;
            }
            set
            {
                _dokumentValidation = value;
                OnPropertyChanged(nameof(DokumentValidation));
            }
        }

        private ICommand _dodajStavkuCommand;
        public ICommand DodajStavkuCommand => _dodajStavkuCommand;

        private ICommand _dodajFakturuCommand;
        public ICommand DodajFakturuCommand => _dodajFakturuCommand;

        public DodajDokumentViewModel()
        {
            _komitentService = new KomitentService();
            _robaService = new RobaService();
            _dokumentService = new DokumentService();
            _dokumentValidator = new DokumentValidator();
            _stavkaValidator = new StavkaDokumentaValidator();
            Roba = new ObservableCollection<RobaDTO>();
            Komitenti = new ObservableCollection<KomitentDTO>();
            Stavke = new ObservableCollection<StavkaDokumentaDTO>();
            SelectedRoba = new RobaDTO();
            SelectedKomitent = new KomitentDTO();
            SelectedDokument = new DokumentDTO();
            _dodajStavkuCommand = new RelayCommand(DodajStavku);
            _dodajFakturuCommand = new RelayCommand(DodajFakturu);
            LoadData();
        }

        private async void DodajFakturu(object obj)
        {
            if (UkupnaCena == null)
            {
                DokumentValidation = "Morate uneti stavke";
                return;
            }
            var dokument = new DokumentDTO
            {
                DatumIzdavanja = DateTime.UtcNow,
                UkupnaCena = decimal.Parse(UkupnaCena!),
                Komitent = SelectedKomitent,
                Stavke = Stavke!.ToList()
            };
            var result = _dokumentValidator.Validate(dokument);
            if (!result.IsValid)
            {
                DokumentValidation = string.Join("\n", result.Errors.Select(error => error.ErrorMessage));
                return;
            }
            await _dokumentService.AddDokument(dokument);
            Stavke = new ObservableCollection<StavkaDokumentaDTO>();
            SelectedKomitent = new KomitentDTO();
            SelectedRoba = new RobaDTO();
            UkupnaCena = string.Empty;
        }

        private void DodajStavku(object obj)
        {
            if (Kolicina == null)
            {
                StavkaValidation = "Morate uneti kolicinu";
                return;
            }
            StavkaDokumentaDTO stavka = new StavkaDokumentaDTO
            {
                Roba = SelectedRoba,
                Kolicina = int.Parse(Kolicina!),
                CenaStavkeKom = SelectedRoba!.Cena,
                UkupnaCenaStavke = int.Parse(Kolicina!) * SelectedRoba.Cena
            };
            var result = _stavkaValidator.Validate(stavka);
            if (!result.IsValid)
            {
                StavkaValidation = string.Join("\n", result.Errors.Select(error => error.ErrorMessage));
                return;
            }
            Stavke!.Add(stavka);
            UkupnaCena = Stavke.Sum(x => x.UkupnaCenaStavke).ToString();
        }

        private async void LoadData()
        {
            var roba = await _robaService.GetRoba();
            Roba = new ObservableCollection<RobaDTO>(roba);
            var komitenti = await _komitentService.GetKomitents();
            Komitenti = new ObservableCollection<KomitentDTO>(komitenti);
        }
    }
}
