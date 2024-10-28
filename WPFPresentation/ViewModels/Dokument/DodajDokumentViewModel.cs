using Application.DTOs;
using FluentValidation;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
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

        private Brush? _infoColor;
        public Brush? InfoColor
        {
            get
            {
                return _infoColor;
            }
            set
            {
                _infoColor = value;
                OnPropertyChanged(nameof(InfoColor));
            }
        }

        private string? _dokumentInfoText;
        public string? DokumentInfoText
        {
            get
            {
                return _dokumentInfoText;
            }
            set
            {
                _dokumentInfoText = value;
                OnPropertyChanged(nameof(DokumentInfoText));
            }
        }

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
                UpdateSelectedRobaDetails();
            }
        }

        private void UpdateSelectedRobaDetails()
        {
            SelectedRobaDetails = $"Sifra: {SelectedRoba!.SifraRobe}\n" +
                                  $"Naziv: {SelectedRoba!.Naziv}\n" +
                                  $"Jedinica mere: {SelectedRoba!.JedinicaMere}\n" +
                                  $"Cena: {SelectedRoba!.Cena}";
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
                UpdateSelectedKomitentDetails();
            }
        }

        private void UpdateSelectedKomitentDetails()
        {
            SelectedKomitentDetails = $"Sifra: {SelectedKomitent!.SifraKomitenta}\n" +
                                      $"Naziv: {SelectedKomitent!.Naziv}\n" +
                                      $"Adresa: {SelectedKomitent!.Adresa}\n" +
                                      $"Grad: {SelectedKomitent!.Grad}";
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

        private string? _selectedKomitentDetails;
        public string? SelectedKomitentDetails
        {
            get
            {
                return _selectedKomitentDetails;
            }
            set
            {
                _selectedKomitentDetails = value;
                OnPropertyChanged(nameof(SelectedKomitentDetails));
            }
        }

        private string? _selectedRobaDetails;
        public string? SelectedRobaDetails
        {
            get
            {
                return _selectedRobaDetails;
            }
            set
            {
                _selectedRobaDetails = value;
                OnPropertyChanged(nameof(SelectedRobaDetails));
            }
        }

        private string? _datumIzdavanjaRacuna;
        public string? DatumIzdavanjaRacuna
        {
            get
            {
                return _datumIzdavanjaRacuna;
            }
            set
            {
                _datumIzdavanjaRacuna = value;
                OnPropertyChanged(nameof(DatumIzdavanjaRacuna));
            }
        }

        private DateTime? _datumDospeca;
        public DateTime? DatumDospeca
        {
            get
            {
                return _datumDospeca;
            }
            set
            {
                _datumDospeca = value;
                OnPropertyChanged(nameof(DatumDospeca));
            }
        }

        private Visibility _robaGroupVisibility = Visibility.Collapsed;
        public Visibility RobaGroupVisibility
        {
            get { return _robaGroupVisibility; }
            set
            {
                _robaGroupVisibility = value;
                OnPropertyChanged(nameof(RobaGroupVisibility));
            }
        }

        private ICommand _dodajStavkuCommand;
        public ICommand DodajStavkuCommand => _dodajStavkuCommand;

        private ICommand _dodajStavkeCommand;
        public ICommand DodajStavkeCommand => _dodajStavkeCommand;

        private ICommand _dodajDokumentCommand;
        public ICommand DodajDokumentCommand => _dodajDokumentCommand;

        private ICommand _deleteStavkaCommand;
        public ICommand DeleteStavkaCommand => _deleteStavkaCommand;

        public DodajDokumentViewModel()
        {
            _komitentService = new KomitentService();
            _robaService = new RobaService();
            _dokumentService = new DokumentService();
            _dokumentValidator = new DokumentValidator();
            _stavkaValidator = new StavkaDokumentaValidator();
            _dodajStavkeCommand = new RelayCommand(ShowRobaGroup);
            _dodajStavkuCommand = new RelayCommand(DodajStavku);
            _dodajDokumentCommand = new RelayCommand(DodajDokument);
            _deleteStavkaCommand = new RelayCommand(DeleteStavka);
            DatumIzdavanjaRacuna = DateTime.Now.Date.ToString("d");
            LoadData();
        }

        private void DeleteStavka(object obj)
        {
            if (obj is StavkaDokumentaDTO stavka)
            {
                Stavke!.Remove(stavka);
                UkupnaCena = Stavke.Sum(x => x.UkupnaCenaStavke).ToString();
            }
        }

        private void ShowRobaGroup(object obj)
        {

            if (!ValidateDokument()) return;

            DokumentValidation = string.Empty;
            RobaGroupVisibility = Visibility.Visible;
            Stavke = new ObservableCollection<StavkaDokumentaDTO>();
        }

        private bool ValidateDokument()
        {
            if (!DatumDospeca.HasValue)
            {
                DokumentValidation = string.Join("\n", "Niste odabrali datum dospeca");
                return false;
            }

            SelectedDokument = new DokumentDTO
            {
                BrojDokumenta = BrojDokumenta,
                DatumDospeca = DateOnly.FromDateTime(DatumDospeca.Value),
                DatumIzdavanja = DateOnly.FromDateTime(DateTime.Now),
                Komitent = SelectedKomitent
            };

            var result = _dokumentValidator.Validate(SelectedDokument);
            if (!result.IsValid)
            {
                DokumentValidation = string.Join("\n", result.Errors.Select(error => error.ErrorMessage));
                return false;
            }
            return true;
        }

        private void DodajStavku(object obj)
        {
            if (!ValidateRoba()) return;


            StavkaDokumentaDTO stavka = new StavkaDokumentaDTO
            {
                Roba = SelectedRoba,
                Kolicina = int.Parse(Kolicina!),
                UkupnaCenaStavke = int.Parse(Kolicina!) * SelectedRoba!.Cena
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

        private bool ValidateRoba()
        {
            if (SelectedRoba == null)
            {
                StavkaValidation = string.Join("\n", "Morate odabrati robu");
                return false;
            }

            if (Kolicina == null)
            {
                StavkaValidation = string.Join("\n", "Morate odabrati kolicinu");
                return false;
            }

            return true;
        }

        private async void DodajDokument(object obj)
        {
            if (UkupnaCena == null || UkupnaCena == "0")
            {
                DokumentInfoText = string.Join("\n", "Morate uneti stavke");
                InfoColor = Brushes.Red;
                return;
            }

            SelectedDokument!.Stavke = new List<StavkaDokumentaDTO>(Stavke!);
            SelectedDokument.UkupnaCena = int.Parse(UkupnaCena!);

            var successfull = await _dokumentService.AddDokument(SelectedDokument!);
            if (successfull)
            {
                DokumentInfoText = "Uspesno dodat dokument";
                InfoColor = Brushes.Green;
                ResetData();
            }
            else
            {
                DokumentInfoText = "Doslo je do greske prilikom dodavanja dokumenta";
                InfoColor = Brushes.Red;
            }
        }

        private void ResetData()
        {
            Stavke = new ObservableCollection<StavkaDokumentaDTO>();
            SelectedKomitent = new KomitentDTO();
            SelectedKomitentDetails = string.Empty;
            SelectedRoba = new RobaDTO();
            SelectedRobaDetails = string.Empty;
            SelectedDokument = new DokumentDTO();
            UkupnaCena = string.Empty;
            BrojDokumenta = string.Empty;
            DatumDospeca = null;
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
