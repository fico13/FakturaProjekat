using Application.DTOs;
using FluentValidation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using WPFPresentation.Commands;
using WPFPresentation.Services;
using WPFPresentation.Validators;

namespace WPFPresentation.ViewModels.Komitent
{
    public class IzmeniKomitentaViewModel : BaseViewModel
    {
        private KomitentService _komitentService;
        private IValidator<KomitentDTO> _validator;

        private string? _sifraKomitentaSearch;
        public string? SifraKomitentaSearch
        {
            get
            {
                return _sifraKomitentaSearch;
            }
            set
            {
                _sifraKomitentaSearch = value;
                OnPropertyChanged(nameof(SifraKomitentaSearch));
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

        private string? _validation;
        public string? Validation
        {
            get
            {
                return _validation;
            }
            set
            {
                _validation = value;
                OnPropertyChanged(nameof(Validation));
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

        public ICommand FindKomitentCommand { get; set; }
        public ICommand UpdateKomitentCommand { get; set; }
        public ICommand DeleteKomitentCommand { get; set; }

        public IzmeniKomitentaViewModel()
        {
            _komitentService = new KomitentService();
            _validator = new KomitentValidator();
            FindKomitentCommand = new RelayCommand(async (obj) => await FindKomitents(obj));
            UpdateKomitentCommand = new RelayCommand(async (obj) => await UpdateKomitent(obj));
            DeleteKomitentCommand = new RelayCommand(async (obj) => await DeleteKomitent(obj));
        }

        private async Task DeleteKomitent(object obj)
        {
            if (SelectedKomitent == null || SelectedKomitent!.SifraKomitenta == null)
            {
                Validation = "Morate izabrati komitenta";
                ValidationColor = Brushes.Red;
                return;
            }

            var successfull = await _komitentService.DeleteKomitent(SelectedKomitent!.SifraKomitenta);

            if (successfull)
            {
                Validation = "Uspesno obrisan komitent";
                ValidationColor = Brushes.Green;
            }
            else
            {
                Validation = "Greska prilikom brisanja komitenta";
                ValidationColor = Brushes.Red;
            }

            await FindKomitents(obj);
        }

        private async Task UpdateKomitent(object obj)
        {
            if (SelectedKomitent == null || SelectedKomitent!.SifraKomitenta == null)
            {
                Validation = "Morate izabrati komitenta";
                ValidationColor = Brushes.Red;
                return;
            }

            var result = _validator.Validate(SelectedKomitent!);
            if (!result.IsValid)
            {
                Validation = string.Join("\n", result.Errors.Select(error => error.ErrorMessage));
                ValidationColor = Brushes.Red;
                return;
            }

            var successfull = await _komitentService.UpdateKomitent(SelectedKomitent!);
            if (successfull)
            {
                Validation = "Uspesno izmenjen komitent";
                ValidationColor = Brushes.Green;
            }
            else
            {
                Validation = "Greska prilikom izmene komitenta";
                ValidationColor = Brushes.Red;
            }

            await FindKomitents(obj);
        }

        private async Task FindKomitents(object obj)
        {
            IEnumerable<KomitentDTO> komitenti = new List<KomitentDTO>();

            if (string.IsNullOrWhiteSpace(SifraKomitentaSearch))
            {
                komitenti = await _komitentService.GetKomitents();
                Komitenti = new ObservableCollection<KomitentDTO>(komitenti);
                return;
            }

            komitenti = await _komitentService.FindKomitents(SifraKomitentaSearch);
            Komitenti = new ObservableCollection<KomitentDTO>(komitenti);
        }
    }
}
