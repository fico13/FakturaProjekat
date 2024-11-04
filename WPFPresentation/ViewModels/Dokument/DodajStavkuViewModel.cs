using Application.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using WPFPresentation.Commands;
using WPFPresentation.Services;
using WPFPresentation.Validators;

namespace WPFPresentation.ViewModels.Dokument
{
    public class DodajStavkuViewModel : BaseViewModel
    {
        private RobaService _robaService;
        private DokumentService _dokumentService;
        private DokumentDTO _selectedDokument;
        private ObservableCollection<RobaDTO>? _robaList;
        private RobaDTO? _selectedRoba;
        private int _kolicina = 1;
        private string? _selectedRobaInfo;
        private string? _validationMessage;


        public ObservableCollection<RobaDTO>? RobaList
        {
            get => _robaList;
            set
            {
                _robaList = value;
                OnPropertyChanged(nameof(RobaList));
            }
        }

        public RobaDTO? SelectedRoba
        {
            get => _selectedRoba;
            set
            {
                _selectedRoba = value;
                OnPropertyChanged(nameof(SelectedRoba));
                UpdateSelectedRobaInfo();
            }
        }

        public int Kolicina
        {
            get => _kolicina;
            set
            {
                _kolicina = value;
                OnPropertyChanged(nameof(Kolicina));
                UkupnaCena = SelectedRoba != null ? (SelectedRoba.Cena * Kolicina).ToString() : null;
            }
        }

        public string? SelectedRobaInfo
        {
            get => _selectedRobaInfo;
            set
            {
                _selectedRobaInfo = value;
                OnPropertyChanged(nameof(SelectedRobaInfo));
                UkupnaCena = SelectedRoba != null ? (SelectedRoba.Cena * Kolicina).ToString() : null;
            }
        }

        public string? ValidationMessage
        {
            get => _validationMessage;
            set
            {
                _validationMessage = value;
                OnPropertyChanged(nameof(ValidationMessage));
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


        public ICommand DodajStavkuCommand { get; }

        public DodajStavkuViewModel(DokumentDTO selectedDokument)
        {
            _robaService = new RobaService();
            _dokumentService = new DokumentService();
            _selectedDokument = selectedDokument;
            DodajStavkuCommand = new RelayCommand(DodajStavku);
            LoadData();
        }

        private async void LoadData()
        {
            var roba = await _robaService.GetRoba();
            RobaList = new ObservableCollection<RobaDTO>(roba);
        }


        private async void DodajStavku(object parameter)
        {
            if (SelectedRoba == null)
            {
                StavkaValidation = "Morate izabrati robu!";
                ValidationColor = Brushes.Red;
                return;
            }

            StavkaDokumentaDTO stavkaDokumentaDTO = new StavkaDokumentaDTO
            {
                Roba = SelectedRoba,
                Kolicina = Kolicina,
                UkupnaCenaStavke = Convert.ToInt32(UkupnaCena)
            };

            var stavkaValidator = new StavkaDokumentaValidator();
            var result = stavkaValidator.Validate(stavkaDokumentaDTO);
            if (!result.IsValid)
            {
                StavkaValidation = string.Join("\n", result.Errors.Select(error => error.ErrorMessage));
                ValidationColor = Brushes.Red;
                return;
            }

            _selectedDokument.UkupnaCena += stavkaDokumentaDTO.UkupnaCenaStavke;
            _selectedDokument.Stavke!.Add(stavkaDokumentaDTO);

            var successfull = await _dokumentService.UpdateDokument(_selectedDokument);

            if (successfull)
            {
                StavkaValidation = "Stavka uspesno dodata!";
                ValidationColor = Brushes.Green;
            }

            else
            {
                StavkaValidation = "Greska prilikom dodavanja stavke!";
                ValidationColor = Brushes.Red;
            }
        }

        private void UpdateSelectedRobaInfo()
        {
            SelectedRobaInfo = $"Sifra: {SelectedRoba!.SifraRobe}\n" +
                                  $"Naziv: {SelectedRoba!.Naziv}\n" +
                                  $"Jedinica mere: {SelectedRoba!.JedinicaMere}\n" +
                                  $"Cena: {SelectedRoba!.Cena}";
        }
    }
}
