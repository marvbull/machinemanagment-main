using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MMSLib.Klassen;

namespace MMS.ViewModel
{
    public class AuftragBearbeitenViewModel : ViewModelBase
    {
        public ObservableCollection<dynamic> AuftraegeMitFacharbeitern { get; private set; } = new ObservableCollection<dynamic>();

        public AuftragBearbeitenViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            var auftragBearbeiten = new AuftragBearbeiten(); // Stellen Sie sicher, dass AuftragBearbeiten Zugriff auf die DB hat
            var data = auftragBearbeiten.GetAuftraegeMitFacharbeitern(); // Diese Methode muss angepasst werden

            foreach (var item in data)
            {
                AuftraegeMitFacharbeitern.Add(item);
            }
        }


        //public ICommand LoeschenCommand { get; }
        //public ICommand BearbeitenCommand { get; }

        //public AuftragBearbeitenViewModel()
        //{
        //    LoeschenCommand = new ViewModelCommand(_ => LoescheAuftrag());
        //    BearbeitenCommand = new ViewModelCommand(_ => BearbeiteAuftrag());
        //    LoadData();
        //}

        //private void BearbeiteAuftrag()
        //{
        //    // Logik zum Bearbeiten
        //}

        //private void LoescheAuftrag()
        //{
        //    if (SelectedAuftrag != null)
        //    {
        //        // Logik zum Löschen des Auftrags, z.B. aus der Datenbank und aus AuftraegeMitFacharbeitern
        //        AuftraegeMitFacharbeitern.Remove(SelectedAuftrag);
        //        OnPropertyChanged(nameof(AuftraegeMitFacharbeitern));

        //        // Eventuell muss hier die Datenbankaktualisierung hinzugefügt werden
        //    }
        //}


    }
}