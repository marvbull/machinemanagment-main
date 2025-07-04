using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MMSLib.Model; // Stellen Sie sicher, dass dieser Namespace Ihre Modelle und DbContext enthält

namespace MMS.ViewModel
{
    internal class AuftragLoeschenViewModel : ViewModelBase
    {
        private string _auftragsID;
        public string AuftragsID
        {
            get => _auftragsID;
            set
            {
                _auftragsID = value;
                OnPropertyChanged();
            }
        }

        public ICommand OKCommand { get; private set; }
        public ICommand AbbrechenCommand { get; private set; }

        public AuftragLoeschenViewModel()
        {
            OKCommand = new ViewModelCommand(async _ => await LoescheAuftrag());
            AbbrechenCommand = new ViewModelCommand(_ => SchliessePopup());
        }

        private async Task LoescheAuftrag()
        {
            if (string.IsNullOrEmpty(AuftragsID))
            {
                MessageBox.Show("Bitte geben Sie eine gültige AuftragsID ein.");
                return;
            }

            if (!int.TryParse(AuftragsID, out int auftragsIdNum))
            {
                MessageBox.Show("AuftragsID muss eine Zahl sein.");
                return;
            }

            try
            {
                using (var context = new DBConnect()) // Ersetzen Sie DBConnect mit Ihrem DbContext
                {
                    var auftrag = await context.Auftraege.FindAsync(auftragsIdNum);
                    if (auftrag != null)
                    {
                        context.Auftraege.Remove(auftrag);
                        await context.SaveChangesAsync();
                        MessageBox.Show($"Auftrag {AuftragsID} wurde erfolgreich gelöscht.");
                    }
                    else
                    {
                        MessageBox.Show($"Auftrag mit ID {AuftragsID} wurde nicht gefunden.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ein Fehler ist aufgetreten: {ex.Message}");
            }

            SchliessePopup();
        }

        public event Action? OnPopupClosed;

        private void SchliessePopup()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    // Auslösen des Ereignisses, wenn das Fenster geschlossen wird
                    OnPopupClosed?.Invoke();
                }
            }
        }

    }
}
