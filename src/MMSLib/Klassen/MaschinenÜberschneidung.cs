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
            // Berechnet die Endzeit des Auftrags basierend auf dem Startzeitpunkt und der Dauer
            var endzeit = start.AddMinutes(dauerInMinuten);
            // Überprüft, ob es Aufträge gibt, die sich mit dem geplanten Zeitfenster überschneiden
            var ueberschneidung = await _context.Auftraege
                .AnyAsync(a => a.MaschinenID == maschinenId &&
                               ((a.Beginn < endzeit && a.Beginn >= start) || // Überprüft, ob der Beginn des existierenden Auftrags innerhalb des geplanten Zeitfensters liegt
                                (a.Abgabe > start && a.Abgabe <= endzeit))); // Überprüft, ob das Ende des existierenden Auftrags innerhalb des geplanten Zeitfensters liegt

            return !ueberschneidung;
        }
    }
}