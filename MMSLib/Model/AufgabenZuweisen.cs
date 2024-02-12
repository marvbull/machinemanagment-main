using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSLib.Model
{
    public class AufgabenZuweisen
    {
        public int AuftragsID { get; set; } // Fremdschlüssel
        public int FacharbeiterID { get; set; } // Fremdschlüssel
        public int VorgesetzerID { get; set; } // Fremdschlüssel
        public int Eintrag { get; set; } //Autoincrement


    }

}
