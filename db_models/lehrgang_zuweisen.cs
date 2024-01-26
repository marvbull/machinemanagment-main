using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.db_models
{
    public class Lehrgang_Zuweisung
    {
        public int Lehrgang_ID { get; set; }
        public int ID_Facharbeiter { get; set; } //Fremdschlüssel
    }

}
