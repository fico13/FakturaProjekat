using Application.DTOs;
using FluentValidation;
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

        public ICommand UpdateKomitentCommand { get; set; }

        public IzmeniKomitentaViewModel(KomitentDTO komitent)
        {
            SelectedKomitent = komitent;
            _komitentService = new KomitentService();
            _validator = new KomitentValidator();
            UpdateKomitentCommand = new RelayCommand(async (obj) => await UpdateKomitent(obj));
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

        }
    }
}
