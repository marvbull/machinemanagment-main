using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMSLib.Model;

namespace MMSLib.Klassen

{
    public class AbgabeCompare
    {
        private readonly DBConnect _context;

        public AbgabeCompare(DBConnect context)
        {
            _context = context;
        }

        public async Task LoescheAbgelaufeneAuftraegeAsync()
        {
            // Hole das aktuelle Datum ohne Uhrzeit
            var heute = DateTime.Today;

            // Finde alle Aufträge mit einem Abgabedatum in der Vergangenheit
            var abgelaufeneAuftraege = await _context.Auftraege
                .Where(auftrag => auftrag.Abgabe < heute)
                .ToListAsync();

            if (abgelaufeneAuftraege.Any())
            {
                // Entferne alle gefundenen Aufträge aus dem DbContext
                _context.Auftraege.RemoveRange(abgelaufeneAuftraege);

                // Speichere die Änderungen in der Datenbank
                await _context.SaveChangesAsync();
            }
        }
    }

}