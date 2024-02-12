using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Angenommen, dies wird für asynchrone Datenbankoperationen benötigt.
using MMSLib.Model;
using MMSLib.Klassen;

namespace MMS.ViewModel
{
    public class AuftragsUebersichtPopupViewModel : ViewModelBase
    {
        // Direktinitialisierung, um CS8618 zu vermeiden.
        private ObservableCollection<Auftraege> _aufträge = new ObservableCollection<Auftraege>();

        public ObservableCollection<Auftraege> Aufträge
        {
            get => _aufträge;
            set
            {
                _aufträge = value;
                OnPropertyChanged(nameof(Aufträge));
            }
        }

        public AuftragsUebersichtPopupViewModel()
        {
            // Die Direktinitialisierung des _aufträge Felds macht die Initialisierung hier überflüssig.
        }

        public async Task LoadAufträgeForFacharbeiterAsync(int facharbeiterId)
        {
            // Stellen Sie sicher, dass die Methode LoadAufträgeForFacharbeiterAsync in AuftragAnzeigen auch async ist und Tasks verwendet.
            Aufträge.Clear();

            var auftragAnzeigen = new AuftragAnzeigen();
            // Beispiel für die Annahme, dass LoadAufträgeForFacharbeiterAsync eine Task-basierte asynchrone Methode ist.
            var aufträge = await auftragAnzeigen.LoadAufträgeForFacharbeiterAsync(facharbeiterId);

            foreach (var auftrag in aufträge)
            {
                Aufträge.Add(auftrag);
            }
        }
    }
}
