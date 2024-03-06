using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using MMSLib.Model;
using MMS.View;

namespace MMS.ViewModel
{
    public class CheckViewModel : ViewModelBase
    {
        private string _inputId = string.Empty; // Initialisiert, um CS8618 zu beheben
        private string _errorMessage = string.Empty; // Initialisiert, um CS8618 zu beheben
        private bool _isBusy;

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

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public ICommand ConfirmCommand { get; private set; }
        public CheckViewModel()
        {
            ConfirmCommand = new ViewModelCommand(async (parameter) => await ExecuteConfirmCommandAsync());
        }

        private async Task ExecuteConfirmCommandAsync()
        {
            ErrorMessage = string.Empty;
            IsBusy = true;

            try
            {
                bool idExists = await IdExistsInDatabaseAsync(InputId);
                if (idExists)
                {
                    await OpenAuftragsUebersichtPopupAsync();
                }
                else
                {
                    ErrorMessage = "Ungültige ID.";
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task<bool> IdExistsInDatabaseAsync(string inputId)
        {
            if (int.TryParse(inputId, out int numericId))
            {
                try
                {
                    using (var context = new DBConnect())
                    {
                        return await context.Facharbeiter.AnyAsync(f => f.FacharbeiterID == numericId);
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Fehler bei der Datenbankabfrage: {ex.Message}";
                }
            }
            else
            {
                ErrorMessage = "Eingegebene ID ist keine gültige Zahl.";
            }

            return false;
        }

        private async Task OpenAuftragsUebersichtPopupAsync()
        {
            try
            {
                var popup = new AuftragsÜbersichtPopupView();
                var popupViewModel = new AuftragsUebersichtPopupViewModel();
                popup.DataContext = popupViewModel;

                if (int.TryParse(InputId, out int facharbeiterId))
                {
                    await popupViewModel.LoadAufträgeForFacharbeiterAsync(facharbeiterId);
                    popup.ShowDialog();
                }
                else
                {
                    ErrorMessage = "Eingegebene ID ist keine gültige Zahl.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Fehler beim Öffnen des Popup-Fensters:  {ex.Message}";
            }
        }
    }
}
