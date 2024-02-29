using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSLib.Model
{
    public class Maschine
    {
        public int MaschinenID { get; set; }
        public string? MaschinenName { get; set; }
        public string? Material { get; set; }
        public bool MaschinenStatus { get; set; }
        public int? Auftragsnummer { get; set; }
        //public int Maschinenstunde { get; set; }
    }

}
