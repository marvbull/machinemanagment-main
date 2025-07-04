using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSLib.Model
{
    public class LehrgangZuweisung
    {
        public int LehrgangID { get; set; }
        public int FacharbeiterID { get; set; } //Fremdschlüssel
        public int EintragLehrgang { get; set; } //Auto-Increment & Primary key
    }

}
