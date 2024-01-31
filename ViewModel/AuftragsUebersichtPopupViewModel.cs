using System.Collections.ObjectModel;
using MMS.db_models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            using (var context = new db_connect())
            {
                var zugeordneteAufträge = await context.Aufgabe_Zuweisung
                    .Where(zuweisung => zuweisung.Facharbeiter_ID == facharbeiterId)
                    .Join(context.Auftraege,
                          zuweisung => zuweisung.Auftrags_ID,
                          auftrag => auftrag.Auftrags_ID,
                          (zuweisung, auftrag) => auftrag)
                    .ToListAsync();

                Aufträge.Clear();
                foreach (var auftrag in zugeordneteAufträge)
                {
                    Aufträge.Add(auftrag);
                }
            }
        }

    }

}
