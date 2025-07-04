﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MMSLib.Model;
using MMS.Helper;
using System.Collections.Generic;
using System.Diagnostics;

namespace MMS.ViewModel
{
        public class VorgesetzterIDEingabeViewModel : ViewModelBase
        {
            private string _vorgesetztenID = string.Empty; // Initialisiert, um CS8618 zu beheben
            private string _errorMessage = string.Empty; // Initialisiert, um CS8618 zu beheben

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

            public event EventHandler<DialogResultChangedEventArgs>? DialogResultChanged; // Erlaubt null, keine Initialisierung erforderlich

        public VorgesetzterIDEingabeViewModel()
        {
            OKCommand = new ViewModelCommand(ExecuteOKCommand);
            AbbrechenCommand = new ViewModelCommand(ExecuteAbbrechenCommand);
        }

        private void ExecuteOKCommand(object? obj) // Parameter als nullable deklariert, um CS8622 zu beheben
        {
            // Führe hier die Validierung der Eingabe durch, falls erforderlich
            if (string.IsNullOrWhiteSpace(VorgesetztenID))
            {
                ErrorMessage = "Vorgesetzten-ID darf nicht leer sein.";
                return;
            }

            

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

        private void ExecuteAbbrechenCommand(object? obj)
        {

            DialogResult = false;
            CloseDialog();
        }

        private void CloseDialog()
        {
           
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.DataContext == this);
            window?.Close();
        }
    }
}
