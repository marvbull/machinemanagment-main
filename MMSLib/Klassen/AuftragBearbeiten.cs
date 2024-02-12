using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMSLib.Model;

namespace MMSLib.Klassen
{
    public class AuftragBearbeiten
    {
        public List<dynamic> GetAuftraegeMitFacharbeitern()
        {
            using (var context = new DBConnect())
            {
                var query = (from auftrag in context.Auftraege
                             join zuweisung in context.AufgabenZuweisen on auftrag.AuftragsID equals zuweisung.AuftragsID
                             join facharbeiter in context.Facharbeiter on zuweisung.FacharbeiterID equals facharbeiter.FacharbeiterID
                             select new
                             {
                                 auftrag.AuftragsID,
                                 facharbeiter.FacharbeiterID,
                                 auftrag.Beschreibung,
                                 auftrag.Dauer,
                                 FacharbeiterVorname = facharbeiter.FacharbeiterVorname,
                                 FacharbeiterName = facharbeiter.FacharbeiterName
                             }).ToList<dynamic>();

                return query;
            }
        }

        //private dynamic _selectedAuftrag;
        //public dynamic SelectedAuftrag
        //{
        //    get => _selectedAuftrag;
        //    set
        //    {
        //        _selectedAuftrag = value;
        //        OnPropertyChanged();
        //    }
        //}
    }
}
