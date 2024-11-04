using Application.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using WPFPresentation.Commands;
using WPFPresentation.Services;

namespace WPFPresentation.ViewModels.Dokument
{
    public class DokumentViewModel : BaseViewModel
    {
        private DokumentService _dokumentService;

        private ObservableCollection<DokumentDTO>? _dokumenti;
        public ObservableCollection<DokumentDTO>? Dokumenti
        {
            get
            {
                return _dokumenti;
            }
            set
            {
                _dokumenti = value;
                OnPropertyChanged(nameof(Dokumenti));
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
                ShowStavke();
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

        private string? _serachBrojDokumenta;
        public string? SearchBrojDokumenta
        {
            get
            {
                return _serachBrojDokumenta;
            }
            set
            {
                _serachBrojDokumenta = value;
                OnPropertyChanged(nameof(SearchBrojDokumenta));
            }
        }

        private string? _stavkaValidation;
        public string? StavkaValidation
        {
            get
            {
                return _stavkaValidation;
            }
            set
            {
                _stavkaValidation = value;
                OnPropertyChanged(nameof(StavkaValidation));
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

        private ICommand _findDokumentsCommand;
        public ICommand FindDokumentsCommand => _findDokumentsCommand;

        public ICommand _deleteDokumentCommand;
        public ICommand DeleteDokumentCommand => _deleteDokumentCommand;

        public ICommand _updateDokumentCommand;
        public ICommand UpdateDokumentCommand => _updateDokumentCommand;

        public ICommand _addStavkaCommand;
        public ICommand AddStavkaCommand => _addStavkaCommand;

        private readonly MainWindow _mainWindow;

        public DokumentViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _dokumentService = new DokumentService();
            _findDokumentsCommand = new RelayCommand(async (obj) => await FindDokuments(obj));
            _deleteDokumentCommand = new RelayCommand(async (obj) => await DeleteDokument(obj));
            _updateDokumentCommand = new RelayCommand((obj) => UpdateDokument(obj));
            _addStavkaCommand = new RelayCommand((obj) => AddStavka(obj));
        }

        private void AddStavka(object obj)
        {
            if (SelectedDokument == null)
            {
                StavkaValidation = "Morate izabrati dokument!";
                ValidationColor = Brushes.Red;
                return;
            }
            _mainWindow.Dispatcher.Invoke(() =>
            {
                _mainWindow.ShowUCDodajStavku(SelectedDokument);
            });
        }

        private void UpdateDokument(object obj)
        {
            if (obj is DokumentDTO dokument)
            {
                _mainWindow.Dispatcher.Invoke(() =>
                {
                    _mainWindow.ShowUCIzmeniDokument(dokument);
                });
            }
        }

        private async Task DeleteDokument(object obj)
        {
            var successfull = await _dokumentService.DeleteDokument(SelectedDokument!.BrojDokumenta!);
            if (successfull)
            {
                await FindDokuments(SearchBrojDokumenta!);
            }
        }

        private async Task FindDokuments(object obj)
        {
            IEnumerable<DokumentDTO> dokumenti = new List<DokumentDTO>();
            if (string.IsNullOrWhiteSpace(SearchBrojDokumenta))
            {
                dokumenti = await _dokumentService.GetDokumenti();
                Dokumenti = new ObservableCollection<DokumentDTO>(dokumenti);
                return;
            }
            dokumenti = await _dokumentService.FindDokuments(SearchBrojDokumenta);
            Dokumenti = new ObservableCollection<DokumentDTO>(dokumenti);
        }

        private void ShowStavke()
        {
            if (SelectedDokument != null && SelectedDokument.Stavke != null)
            {
                Stavke = new ObservableCollection<StavkaDokumentaDTO>(SelectedDokument.Stavke);
            }
            else
            {
                Stavke = new ObservableCollection<StavkaDokumentaDTO>();
            }
        }
    }
}
