using System.Collections.ObjectModel;
using System.Linq;
using MMS.db_models;

namespace MMS.ViewModel
{
    public class FacharbeiterViewModel : ViewModelBase
    {
        public ObservableCollection<Facharbeiter> FacharbeiterList { get; set; }

        public FacharbeiterViewModel()
        {
            FacharbeiterList = new ObservableCollection<Facharbeiter>();
            LoadFacharbeiter(); // Implementieren Sie diese Methode, um Daten aus der Datenbank zu laden
        }

        private void LoadFacharbeiter()
        {
            using (var context = new db_connect())
            {
                var facharbeiter = context.Facharbeiter.ToList();
                foreach (var fach in facharbeiter)
                {
                    FacharbeiterList.Add(fach);
                }
            }
        }
    }
}
