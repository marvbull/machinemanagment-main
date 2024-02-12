using System;
using System.Windows.Input;
using FontAwesome.Sharp;
using MMSLib.Model;


namespace MMS.ViewModel
{
    public class MainMMSViewModel : ViewModelBase
    {
        private ViewModelBase? _currentChildView; // Nullable, da beim Start nicht unbedingt initialisiert
        private string? _caption; // Nullable, da es keinen initialen Wert hat
        private IconChar _icon; // Non-nullable, da IconChar ein Enum ist

        public ViewModelBase? CurrentChildView
        {
            get { return _currentChildView; }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string? Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public IconChar Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public ICommand ShowCheckViewCommand { get; }
        public ICommand ShowAuftragAnlegenViewCommand { get; }

        public MainMMSViewModel()
        {
            ShowCheckViewCommand = new ViewModelCommand(ExecuteShowCheckViewCommand);
            ShowAuftragAnlegenViewCommand = new ViewModelCommand(ExecuteAuftragAnlegenViewCommand);

            // Standard-View beim Start
            ExecuteShowCheckViewCommand(null);
        }

        private void ExecuteShowCheckViewCommand(object? obj)
        {
            CurrentChildView = new CheckViewModel();
            Caption = "Check";
            Icon = IconChar.Computer;
        }

        private void ExecuteAuftragAnlegenViewCommand(object? obj)
        {
            CurrentChildView = new AuftragAnlegenViewModel();
            Caption = "Auftrag anlegen";
            Icon = IconChar.Coffee;
        }
    }
}