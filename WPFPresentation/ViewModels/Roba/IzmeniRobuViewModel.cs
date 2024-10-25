using Application.DTOs;
using FluentValidation;
using System.Windows.Input;
using System.Windows.Media;
using WPFPresentation.Commands;
using WPFPresentation.Services;
using WPFPresentation.Validators;

namespace WPFPresentation.ViewModels.Roba
{
    public class IzmeniRobuViewModel : BaseViewModel
    {
        private RobaService _robaService;
        private IValidator<RobaDTO> _validator;

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

        public ICommand UpdateRobaCommand { get; set; }

        public IzmeniRobuViewModel(RobaDTO roba)
        {
            _robaService = new RobaService();
            _validator = new RobaValidator();
            SelectedRoba = roba;
            UpdateRobaCommand = new RelayCommand(async (obj) => await UpdateRoba(obj));
        }

        private async Task UpdateRoba(object obj)
        {
            var result = _validator.Validate(SelectedRoba!);
            if (!result.IsValid)
            {
                Validation = string.Join("\n", result.Errors.Select(error => error.ErrorMessage));
                ValidationColor = Brushes.Red;
                return;
            }

            var successfull = await _robaService.UpdateRoba(SelectedRoba!);
            if (successfull)
            {
                Validation = "Uspesno izmenjena roba";
                ValidationColor = Brushes.Green;
            }
            else
            {
                Validation = "Neuspesno izmenjena roba";
                ValidationColor = Brushes.Red;
            }
        }
    }
}
