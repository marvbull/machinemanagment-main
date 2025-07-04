using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMSLib.Model;

namespace MMSLib.Klassen
{
    public class AuftragAnzeigen
    {
        public async Task<List<Auftraege>> LoadAufträgeForFacharbeiterAsync(int facharbeiterId)
        {
            using (var context = new DBConnect())
            {
                var zugeordneteAufträge = await context.AufgabenZuweisen
                    .Where(zuweisung => zuweisung.FacharbeiterID == facharbeiterId)
                    .Join(context.Auftraege,
                          zuweisung => zuweisung.AuftragsID,
                          auftrag => auftrag.AuftragsID,
                          (zuweisung, auftrag) => auftrag)
                    .ToListAsync();

                return zugeordneteAufträge;
            }
        }
    }
}
