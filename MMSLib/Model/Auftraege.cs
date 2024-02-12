using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSLib.Model
{
    public class Auftraege
    {
        public int AuftragsID { get; set; } // Auto-Increment
        public string? Beschreibung { get; set; }
        public int MaschinenID { get; set; } // Fremdschlüssel
        public string? Material { get; set; }
        public DateTime Abgabe { get; set; }
        
        public int Dauer { get; set; }

    }

}
