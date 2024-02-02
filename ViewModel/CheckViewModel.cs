using System;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using MMSLib.db_models;
using System.Threading.Tasks;
using MMS.View;


namespace MMS.ViewModel
{
    public class CheckViewModel : ViewModelBase
    {
        private string _inputId;
        private string _errorMessage;

        public string InputId
        {
            get { return _inputId; }
            set
            {
                _inputId = value;
                OnPropertyChanged(nameof(InputId));
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

        public ICommand ConfirmCommand { get; private set; }

        public CheckViewModel()
        {
            ConfirmCommand = new ViewModelCommand(ExecuteConfirmCommand);
        }

        private async void ExecuteConfirmCommand(object? parameter)
        {
            ErrorMessage = string.Empty; // Fehlermeldung zu Beginn löschen

            bool idExists = await IdExistsInDatabaseAsync(InputId);
            if (idExists)
            {
                OpenAuftragsUebersichtPopup();
            }
            else
            {
                ErrorMessage = "Ungültige ID.";
            }
        }

        private async Task<bool> IdExistsInDatabaseAsync(string inputId)
        {
            if (int.TryParse(inputId, out int numericId))
            {
                try
                {
                    using (var context = new db_connect())
                    {
                        return await context.Facharbeiter.AnyAsync(f => f.Facharbeiter_ID == numericId);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Fehler bei der Datenbankabfrage: {ex.Message}");
                }
            }
            return false;
        }


        private async void OpenAuftragsUebersichtPopup()
        {
            var popup = new AuftragsÜbersichtPopupView();
            var popupViewModel = new AuftragsUebersichtPopupViewModel();
            popup.DataContext = popupViewModel;

            if (int.TryParse(InputId, out int facharbeiterId))
            {
                await popupViewModel.LoadAufträgeForFacharbeiter(facharbeiterId);
                popup.ShowDialog();
            }
            else
            {
                ErrorMessage = "Eingegebene ID ist keine gültige Zahl.";
            }
        }
    }
}
  
