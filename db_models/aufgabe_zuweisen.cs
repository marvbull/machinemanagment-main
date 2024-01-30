using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.db_models
{
    public class Aufgabe_Zuweisung
    {
        public int Auftrags_ID { get; set; } // Fremdschlüssel
        public int Facharbeiter_ID { get; set; } // Fremdschlüssel
        public int Vorgesetzer_ID { get; set; } // Fremdschlüssel
        public int Eintrag { get; set; } //Autoincrement
    }

}
