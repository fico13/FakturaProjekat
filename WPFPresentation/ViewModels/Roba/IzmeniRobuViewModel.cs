using Application.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using WPFPresentation.Commands;
using WPFPresentation.Services;

namespace WPFPresentation.ViewModels.Roba
{
    public class IzmeniRobuViewModel : BaseViewModel
    {
        private RobaService _robaService;

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


        public ICommand FindRobuCommand { get; set; }
        public ICommand UpdateRobaCommand { get; set; }
        public ICommand DeleteRobaCommand { get; set; }

        public IzmeniRobuViewModel()
        {
            _robaService = new RobaService();
            SelectedRoba = new RobaDTO();
            Roba = new ObservableCollection<RobaDTO>();
            Errors = new TextBox();
            FindRobuCommand = new RelayCommand(async (obj) => await FindRoba(obj));
            UpdateRobaCommand = new RelayCommand(async (obj) => await UpdateRoba(obj));
            DeleteRobaCommand = new RelayCommand(async (obj) => await DeleteRoba(obj));
        }

        private async Task DeleteRoba(object obj)
        {
            await _robaService.ObrisiRobu(SelectedRoba!);
            SelectedRoba = new RobaDTO();
            await FindRoba(obj);
        }

        private async Task UpdateRoba(object obj)
        {
            await _robaService.UpdateRoba(SelectedRoba!);
            SelectedRoba = new RobaDTO();
        }

        private async Task FindRoba(object obj)
        {
            var roba = await _robaService.FindRoba(SearchString!);
            Roba = new ObservableCollection<RobaDTO>(roba);
        }

        private TextBox? _errors;
        public TextBox? Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
                OnPropertyChanged(nameof(Errors));
            }
        }
    }
}
