using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMSLib.Model;


namespace MMSLib.Klassen
{ 
    public class AuftragAnzeigen
    {      
        public  List<Auftraege> LoadAufträgeForFacharbeiter(int facharbeiterId)
        {
            var aufträge = new List<Auftraege>();
            using (var context = new DBConnect())
            {
                var zugeordneteAufträge = context.AufgabenZuweisen
                    .Where(zuweisung => zuweisung.FacharbeiterID == facharbeiterId)
                    .Join(context.Auftraege,
                          zuweisung => zuweisung.AuftragsID,
                          auftrag => auftrag.AuftragsID,
                          (zuweisung, auftrag) => auftrag);
                   

                
                foreach (var auftrag in zugeordneteAufträge)
                {
                    aufträge.Add(auftrag);

                }
            }
            return aufträge;
        }
    }
}
