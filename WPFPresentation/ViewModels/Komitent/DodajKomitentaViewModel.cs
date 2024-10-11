using Application.DTOs;
using FluentValidation;
using System.Windows.Input;
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
                return;
            }
            await _komitentService.AddKomitent(Komitent);
            Komitent = new KomitentDTO();
        }
    }
}
