using Application.DTOs;
using FluentValidation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using WPFPresentation.Commands;
using WPFPresentation.Services;
using WPFPresentation.Validators;

namespace WPFPresentation.ViewModels.Dokument
{
    public class IzmeniDokumentViewModel : BaseViewModel
    {
        private DokumentService _dokumentService;
        private IValidator<DokumentDTO> _dokumentValidator;

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
                ShowKolicina();
            }
        }

        private void ShowKolicina()
        {
            Kolicina = SelectedStavka!.Kolicina;
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

        private ICommand _updateDokumentCommand;
        public ICommand UpdateDokumentCommand => _updateDokumentCommand;

        private ICommand _updateStavkaCommand;
        public ICommand UpdateStavkaCommand => _updateStavkaCommand;


        public IzmeniDokumentViewModel(DokumentDTO dokument)
        {
            SelectedDokument = dokument;
            BrojDokumenta = dokument.BrojDokumenta!.ToString();
            DatumIzdavanjaRacuna = SelectedDokument.DatumIzdavanja.ToString();
            DatumDospeca = SelectedDokument.DatumDospeca.ToDateTime(TimeOnly.MinValue);
            UkupnaCena = SelectedDokument.UkupnaCena.ToString();
            Stavke = new ObservableCollection<StavkaDokumentaDTO>(SelectedDokument.Stavke!);
            _dokumentService = new DokumentService();
            _dokumentValidator = new DokumentValidator();
            _updateStavkaCommand = new RelayCommand(UpdateStavka);
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
            UkupnaCena = SelectedDokument!.UkupnaCena.ToString();

            Stavke = new ObservableCollection<StavkaDokumentaDTO>(SelectedDokument.Stavke);
        }

        private async Task UpdateDokument(object obj)
        {
            if (DatumDospeca is null)
            {
                ValidationText = "Morate uneti datum dospeca";
                ValidationColor = Brushes.Red;
                return;
            }
            SelectedDokument!.DatumDospeca = DateOnly.FromDateTime(DatumDospeca!.Value);
            var result = _dokumentValidator.Validate(SelectedDokument!);
            if (!result.IsValid)
            {
                ValidationText = string.Join("\n", result.Errors.Select(error => error.ErrorMessage));
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
        }
    }
}
