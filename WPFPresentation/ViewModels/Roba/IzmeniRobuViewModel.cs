using Application.DTOs;
using FluentValidation;
using System.Collections.ObjectModel;
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

        private string? _searchString;
        public string? SearchString
        {
            get
            {
                return _searchString;
            }
            set
            {
                _searchString = value;
                OnPropertyChanged(nameof(SearchString));
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

        public ICommand FindRobuCommand { get; set; }
        public ICommand UpdateRobaCommand { get; set; }
        public ICommand DeleteRobaCommand { get; set; }

        public IzmeniRobuViewModel()
        {
            _robaService = new RobaService();
            _validator = new RobaValidator();
            FindRobuCommand = new RelayCommand(async (obj) => await FindRoba(obj));
            UpdateRobaCommand = new RelayCommand(async (obj) => await UpdateRoba(obj));
            DeleteRobaCommand = new RelayCommand(async (obj) => await DeleteRoba(obj));
        }


        private async Task DeleteRoba(object obj)
        {
            if (SelectedRoba == null || SelectedRoba!.SifraRobe == null)
            {
                Validation = "Morate izabrati robu";
                ValidationColor = Brushes.Red;
                return;
            }

            var successfull = await _robaService.DeleteRoba(SelectedRoba!.SifraRobe);
            if (successfull)
            {
                Validation = "Uspesno obrisana roba";
                ValidationColor = Brushes.Green;
            }
            else
            {
                Validation = "Neuspesno obrisana roba";
                ValidationColor = Brushes.Red;
            }

            await FindRoba(obj);
        }

        private async Task UpdateRoba(object obj)
        {
            if (SelectedRoba == null || SelectedRoba!.SifraRobe == null)
            {
                Validation = "Morate izabrati robu";
                ValidationColor = Brushes.Red;
                return;
            }

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

            await FindRoba(obj);
        }

        private async Task FindRoba(object obj)
        {
            IEnumerable<RobaDTO> roba;

            if (string.IsNullOrWhiteSpace(SearchString))
            {
                roba = await _robaService.GetRoba();
                Roba = new ObservableCollection<RobaDTO>(roba);
                return;
            }

            roba = await _robaService.FindRoba(SearchString!);
            Roba = new ObservableCollection<RobaDTO>(roba);
        }
    }
}
