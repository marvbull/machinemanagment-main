using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMSLib.Model;

namespace MMSLib.Klassen
{

    public class MaschinenÜberschneidung
    {
        private readonly DBConnect _context;

        public MaschinenÜberschneidung(DBConnect context)
        {
            _context = context;
        }

        public async Task<bool> IstMaschineVerfuegbar(DateTime start, int dauerInMinuten, int maschinenId)
        {
            var endzeit = start.AddMinutes(dauerInMinuten);
            var ueberschneidung = await _context.Auftraege
                .AnyAsync(a => a.MaschinenID == maschinenId &&
                               ((a.Beginn < endzeit && a.Beginn >= start) ||
                                (a.Abgabe > start && a.Abgabe <= endzeit)));
            return !ueberschneidung;
        }
    }
}