using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MMSLib.Model;
using MMS.Helper;

namespace MMS.ViewModel
{
    public class VorgesetzterIDEingabeViewModel : ViewModelBase
    {
        private string _vorgesetztenID;
        private string _errorMessage;

        public string VorgesetztenID
        {
            get { return _vorgesetztenID; }
            set
            {
                _vorgesetztenID = value;
                OnPropertyChanged(nameof(VorgesetztenID));
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand OKCommand { get; }
        public ICommand AbbrechenCommand { get; }

        public bool DialogResult { get; private set; }

        public event EventHandler<DialogResultChangedEventArgs> DialogResultChanged;

        public VorgesetzterIDEingabeViewModel()
        {
            OKCommand = new ViewModelCommand(ExecuteOKCommand);
            AbbrechenCommand = new ViewModelCommand(ExecuteAbbrechenCommand);
        }

        private void ExecuteOKCommand(object obj)
        {
            // Führe hier die Validierung der Eingabe durch, falls erforderlich
            if (string.IsNullOrWhiteSpace(VorgesetztenID))
            {
                ErrorMessage = "Vorgesetzten-ID darf nicht leer sein.";
                return;
            }

            // Hier kommt deine Logik zur Überprüfung der Vorgesetzten-ID mit der Tabelle Vorgesetzer
            // Angenommen, du hast eine Datenbankverbindung und eine entsprechende Methode in einem Repository oder Service

            bool vorgesetzerExists = CheckVorgesetzerExists(VorgesetztenID);

            if (vorgesetzerExists)
            {
                // Setze DialogResult auf true, um anzuzeigen, dass die Eingabe erfolgreich war
                DialogResult = true;
                CloseDialog();

                // Benachrichtige das ViewModel, das auf dieses Ereignis gehört
                DialogResultChanged?.Invoke(this, new DialogResultChangedEventArgs(true));
            }
            else
            {
                // Fehlermeldung anzeigen, wenn die Vorgesetzten-ID nicht in der Tabelle gefunden wurde
                ErrorMessage = "Vorgesetzten-ID nicht gefunden.";
            }
        }

        private bool CheckVorgesetzerExists(string vorgesetztenID)
        {
            using (var dbContext = new DBConnect())
            {
                // Überprüfen, ob ein Vorgesetzter mit der angegebenen ID in der Datenbank existiert
                var vorgesetzer = dbContext.Vorgesetzter.FirstOrDefault(v => v.VorgesetzerID == vorgesetztenID);

                // Rückgabe true, wenn der Vorgesetzer gefunden wurde, andernfalls false
                return vorgesetzer != null;
            }
        }

        private void ExecuteAbbrechenCommand(object obj)
        {
            // Setze DialogResult auf false, um anzuzeigen, dass die Eingabe abgebrochen wurde
            DialogResult = false;
            CloseDialog();
        }

        private void CloseDialog()
        {
            // Schließe das Fenster, in dem dieses ViewModel verwendet wird
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
            window?.Close();
        }
    }
}