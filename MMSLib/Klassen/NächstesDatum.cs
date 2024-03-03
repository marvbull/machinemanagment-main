using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMSLib.Model;

public class NaechstesDatum
{
    private readonly DBConnect _context;

    public NaechstesDatum(DBConnect context)
    {
        _context = context;
    }

    //suche nach nächsten zeitfenster mit tabelle auftraege -> beginn, dauer
    public async Task<DateTime?> FindeNächstesVerfügbaresZeitfenster(int maschinenId, TimeSpan dauer)
    {
        var buchungen = await _context.Auftraege
            .Where(a => a.MaschinenID == maschinenId && a.Beginn >= DateTime.Now)
            .OrderBy(a => a.Beginn)
            .ToListAsync();

        DateTime? nächsterStart = null;
        TimeSpan arbeitstagDauer = TimeSpan.FromHours(8);
        DateTime arbeitsBeginnHeute = DateTime.Now.Date.AddHours(8); // Angenommen, der Arbeitstag beginnt um 08:00 Uhr

        // Wenn heute keine Zeit mehr ist, fange morgen an
        if (DateTime.Now > arbeitsBeginnHeute.Add(arbeitstagDauer))
        {
            arbeitsBeginnHeute = arbeitsBeginnHeute.AddDays(1);
        }

        foreach (var buchung in buchungen)
        {
            var buchungEnde = buchung.Beginn.AddMinutes(buchung.Dauer);
            var nächsterArbeitstag = buchungEnde.Date.AddHours(8); // Nächster Arbeitstag beginnt um 08:00 Uhr

            // Prüfe, ob zwischen dieser und der nächsten Buchung genügend Zeit ist
            if (!nächsterStart.HasValue)
            {
                nächsterStart = buchungEnde;
            }
            else if (nächsterStart.Value.Date < buchung.Beginn.Date)
            {
                // Wenn der nächste Start vor dem aktuellen Buchungstag ist, setze ihn auf den Beginn des Buchungstags
                nächsterStart = nächsterArbeitstag;
            }

            // Prüfe, ob das Ende des Arbeitstages erreicht ist
            if (buchungEnde - nächsterArbeitstag >= arbeitstagDauer)
            {
                // Auf ganzen Tag aufrunden, wenn die verbleibende Zeit am Tag nicht ausreicht
                nächsterStart = nächsterArbeitstag.AddDays(1);
                break;
            }
        }

        // Wenn kein Startdatum gefunden wurde, beginne mit dem nächsten verfügbaren Arbeitstag
        if (!nächsterStart.HasValue || nächsterStart.Value < DateTime.Now)
        {
            nächsterStart = arbeitsBeginnHeute;
        }

        // Berechne die Anzahl der benötigten vollen Arbeitstage
        int volleTage = (int)Math.Ceiling(dauer.TotalHours / 8.0);
        nächsterStart = nächsterStart.Value.Date.AddHours(8); // Beginn um 08:00 Uhr am nächsten möglichen Tag
        nächsterStart = nächsterStart.Value.AddDays(volleTage - 1); // Addiere die benötigten vollen Tage

        return nächsterStart;
    }
}