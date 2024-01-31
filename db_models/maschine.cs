using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.db_models
{
    public class Maschine
    {
        public int Maschinen_ID { get; set; }
        public string? Maschinen_Name { get; set; } // Ich nehme an, dass "bit" als bool interpretiert wird
        public string? Material { get; set; }
        public bool Maschinen_Status { get; set; }
        //public int Auftragsnummer { get; set; } // Fremdschlüssel
        //public int Maschinenstunde { get; set; }
    }

}
