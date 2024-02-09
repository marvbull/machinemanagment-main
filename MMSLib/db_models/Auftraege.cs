using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSLib.db_models
{
    public class Auftraege
    {
        public int Auftrags_ID { get; set; } // Auto-Increment
        public string? Beschreibung { get; set; }
        public int Maschinen_ID { get; set; } // Fremdschlüssel
        public string? Material { get; set; }
        public DateTime Abgabe { get; set; }
        
        public int Dauer { get; set; }
    }

}
