using Application.DTOs;
using FluentValidation;
using System.Windows.Input;
using System.Windows.Media;
using WPFPresentation.Commands;
using WPFPresentation.Services;
using WPFPresentation.Validators;

namespace WPFPresentation.ViewModels.Komitent
{
    public class DodajKomitentaViewModel : BaseViewModel
    {
        private KomitentService _komitentService;
        private readonly IValidator<KomitentDTO> _validator;

        private KomitentDTO? _komitent;
        public KomitentDTO Komitent
        {
            get
            {
                return _komitent!;
            }
            set
            {
                _komitent = value;
                OnPropertyChanged(nameof(Komitent));
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

        private ICommand _dodajKomitenta;
        public ICommand DodajKomitenta => _dodajKomitenta;


        public DodajKomitentaViewModel()
        {
            _komitentService = new KomitentService();
            Komitent = new KomitentDTO();
            _validator = new KomitentValidator();
            _dodajKomitenta = new RelayCommand(async (obj) => await AddKomitent(obj));
        }

        private async Task AddKomitent(object obj)
        {
            var result = _validator.Validate(Komitent);

            if (!result.IsValid)
            {
                Validation = string.Join("\n", result.Errors.Select(error => error.ErrorMessage));
                ValidationColor = Brushes.Red;
                return;
            }
            var successfull = await _komitentService.AddKomitent(Komitent);

            if (successfull)
            {
                Validation = string.Join("\n", "Komitent uspesno dodat");
                ValidationColor = Brushes.Green;
            }
            else
            {
                Validation = string.Join("\n", "Greska prilikom dodavanja komitenta");
                ValidationColor = Brushes.Red;
            }

            Komitent = new KomitentDTO();
        }
    }
}
