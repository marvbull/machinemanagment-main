using System;
using System.Windows;
using System.Windows.Input;
using FontAwesome.Sharp;
using MMS.View;
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
        public ICommand ShowAuftragBearbeitenViewCommand { get; }

        public MainMMSViewModel()
        {
            ShowCheckViewCommand = new ViewModelCommand(ExecuteShowCheckViewCommand);
            ShowAuftragAnlegenViewCommand = new ViewModelCommand(ExecuteAuftragAnlegenViewCommand);
            ShowAuftragBearbeitenViewCommand = new ViewModelCommand(ExecuteShowAuftragBearbeitenViewCommand);

            // Standard-View beim Start
            ExecuteShowCheckViewCommand(null);
        }

        private void ExecuteShowCheckViewCommand(object? obj)
        {
            CurrentChildView = new CheckViewModel() ?? throw new InvalidOperationException("CheckViewModel konnte nicht erstellt werden.");
            Caption = "Check";
            Icon = IconChar.Computer;
        }

        private void ExecuteShowAuftragBearbeitenViewCommand(object? obj)
        {
            CurrentChildView = new AuftragBearbeitenViewModel() ?? throw new InvalidOperationException("AuftragBearbeitenViewModel konnte nicht erstellt werden.");
            Caption = "Vorgang bearbeiten";
            Icon = IconChar.Pen;
        }

        private void ExecuteAuftragAnlegenViewCommand(object? obj)
        {
            var vorgesetzterEingabeViewModel = new VorgesetzterIDEingabeViewModel() ?? throw new InvalidOperationException("VorgesetzterIDEingabeViewModel konnte nicht erstellt werden.");
            var vorgesetzterEingabeView = new VorgesetzterIDEingabeView
            {
                DataContext = vorgesetzterEingabeViewModel
            };

            vorgesetzterEingabeViewModel.DialogResultChanged += (sender, e) =>
            {
                if (e.DialogResult == true) // Stellen Sie sicher, dass das Ergebnis tatsächlich true ist.
                {
                    // Überprüfen Sie, ob sender vom Typ VorgesetzterIDEingabeViewModel ist.
                    if (sender is VorgesetzterIDEingabeViewModel vm)
                    {
                        string vorgesetztenID = vm.VorgesetztenID ?? throw new InvalidOperationException("VorgesetztenID darf nicht null sein.");
                        var auftragAnlegenViewModel = new AuftragAnlegenViewModel();
                        CurrentChildView = auftragAnlegenViewModel; // Kein Null-Check erforderlich, da new immer eine Instanz liefert.
                        auftragAnlegenViewModel.VorgesetztenID = vorgesetztenID;
                        Caption = "Vorgang anlegen";
                        Icon = IconChar.Coffee;
                    }
                    else
                    {
                        throw new InvalidOperationException("Sender ist nicht vom Typ VorgesetzterIDEingabeViewModel.");
                    }
                }
            };


            vorgesetzterEingabeView.ShowDialog();
        }
    }
}



