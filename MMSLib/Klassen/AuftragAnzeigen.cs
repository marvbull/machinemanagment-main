using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMSLib.db_models;


namespace MMSLib.Klassen
{ 
    public class AuftragAnzeigen
    {      
        public  List<Auftraege> LoadAufträgeForFacharbeiter(int facharbeiterId)
        {
            var aufträge = new List<Auftraege>();
            using (var context = new db_connect())
            {
                var zugeordneteAufträge = context.Aufgabe_Zuweisung
                    .Where(zuweisung => zuweisung.Facharbeiter_ID == facharbeiterId)
                    .Join(context.Auftraege,
                          zuweisung => zuweisung.Auftrags_ID,
                          auftrag => auftrag.Auftrags_ID,
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
