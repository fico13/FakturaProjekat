using Application.DTOs;
using FluentValidation;
using System.Windows.Input;
using System.Windows.Media;
using WPFPresentation.Commands;
using WPFPresentation.Services;
using WPFPresentation.Validators;

namespace WPFPresentation.ViewModels.Roba
{
    public class DodajRobuViewModel : BaseViewModel
    {
        private RobaService _robaService;

        private readonly IValidator<RobaDTO> _validator;


        private RobaDTO? _roba;
        public RobaDTO? Roba
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

        private ICommand _dodajRobuCommand;
        public ICommand DodajRobuCommand => _dodajRobuCommand;

        public DodajRobuViewModel()
        {
            Roba = new RobaDTO();
            _robaService = new RobaService();
            _validator = new RobaValidator();
            _dodajRobuCommand = new RelayCommand(async (obj) => await DodajRobuAsync(obj));
        }

        private async Task DodajRobuAsync(object obj)
        {
            var result = _validator.Validate(Roba!);
            if (!result.IsValid)
            {
                Validation = string.Join("\n", result.Errors.Select(error => error.ErrorMessage));
                ValidationColor = Brushes.Red;
                return;
            }

            var successfull = await _robaService.DodajRobu(Roba);
            if (successfull)
            {
                Validation = "Uspesno dodata roba";
                ValidationColor = Brushes.Green;
            }
            else
            {
                Validation = "Doslo je do greske prilikom dodavanja robe";
                ValidationColor = Brushes.Red;
            }
            Roba = new RobaDTO();
        }
    }
}
