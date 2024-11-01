using Application.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFPresentation.Commands;

namespace WPFPresentation.ViewModels.Dokument
{
    public class DodajStavkuViewModel : BaseViewModel
    {
        private DokumentDTO _selectedDokument;
        private ObservableCollection<RobaDTO> _robaList;
        private RobaDTO _selectedRoba;
        private int _kolicina;
        private string _selectedRobaInfo;
        private string _validationMessage;

        public DodajStavkuViewModel(DokumentDTO selectedDokument)
        {
            _selectedDokument = selectedDokument;
            _robaList = new ObservableCollection<RobaDTO>(); // Populate this list as needed
            DodajStavkuCommand = new RelayCommand(DodajStavku);
        }

        public ObservableCollection<RobaDTO> RobaList
        {
            get => _robaList;
            set
            {
                _robaList = value;
                OnPropertyChanged(nameof(RobaList));
            }
        }

        public RobaDTO SelectedRoba
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
            }
        }

        public string SelectedRobaInfo
        {
            get => _selectedRobaInfo;
            set
            {
                _selectedRobaInfo = value;
                OnPropertyChanged(nameof(SelectedRobaInfo));
            }
        }

        public string ValidationMessage
        {
            get => _validationMessage;
            set
            {
                _validationMessage = value;
                OnPropertyChanged(nameof(ValidationMessage));
            }
        }

        public ICommand DodajStavkuCommand { get; }

        private void DodajStavku(object parameter)
        {
            // Add logic to add the item to the document
        }

        private void UpdateSelectedRobaInfo()
        {
            if (_selectedRoba != null)
            {
                SelectedRobaInfo = $"Naziv: {_selectedRoba.Naziv}\nCena: {_selectedRoba.Cena}";
            }
            else
            {
                SelectedRobaInfo = string.Empty;
            }
        }
    }
}
