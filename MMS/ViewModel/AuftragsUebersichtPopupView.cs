using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MMSLib.Model;
using MMSLib.Klassen;


namespace MMS.ViewModel
{
    public class AuftragsUebersichtPopupViewModel : ViewModelBase
    {
        private ObservableCollection<Auftraege> _aufträge;

        public ObservableCollection<Auftraege> Aufträge
        {
            get { return _aufträge; }
            set
            {
                _aufträge = value;
                OnPropertyChanged(nameof(Aufträge));
            }
        }

        public AuftragsUebersichtPopupViewModel()
        {
            Aufträge = new ObservableCollection<Auftraege>();
        }

        public async Task LoadAufträgeForFacharbeiter(int facharbeiterId)
        {
            //using (var context = new db_connect())
            //{
            //    var zugeordneteAufträge = await context.AufgabenZuweisen
            //        .Where(zuweisung => zuweisung.FacharbeiterID == facharbeiterId)
            //        .Join(context.Auftraege,
            //              zuweisung => zuweisung.AuftragsID,
            //              auftrag => auftrag.AuftragsID,
            //              (zuweisung, auftrag) => auftrag)
            //        .ToListAsync();

            Aufträge.Clear();


            foreach (var auftrag in new AuftragAnzeigen().LoadAufträgeForFacharbeiter(facharbeiterId))
            {
                Aufträge.Add(auftrag);
            }

        }

    }
}


