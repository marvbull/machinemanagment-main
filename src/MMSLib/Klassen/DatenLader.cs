using MMSLib.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MMSLib.Klassen
{
    public class DatenLader
    {
        private readonly DBConnect _context;

        public DatenLader(DBConnect context)
        {
            _context = context;
        }

        public async Task<ObservableCollection<Facharbeiter>> LoadFacharbeiterAsync()
        {
            var facharbeiter = await _context.Facharbeiter.ToListAsync();
            return new ObservableCollection<Facharbeiter>(facharbeiter);
        }

        public async Task<ObservableCollection<Maschine>> LoadMaschinenAsync()
        {
            var maschinen = await _context.Maschine.ToListAsync();
            return new ObservableCollection<Maschine>(maschinen);
        }

        public async Task<(string FacharbeiterNachname, string FacharbeiterVorname)> LoadFacharbeiterDetailsAsync(int facharbeiterId)
        {
            var selectedFacharbeiter = await _context.Facharbeiter.FirstOrDefaultAsync(f => f.FacharbeiterID == facharbeiterId);
            return (selectedFacharbeiter?.FacharbeiterName, selectedFacharbeiter?.FacharbeiterVorname);
        }

        public async Task<string> LoadMaschinenDetailsAsync(int maschinenId)
        {
            var selectedMaschine = await _context.Maschine.FirstOrDefaultAsync(m => m.MaschinenID == maschinenId);
            return selectedMaschine?.MaschinenName;
        }
    }
}
