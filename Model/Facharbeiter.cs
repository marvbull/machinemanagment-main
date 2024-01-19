using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Model
{
    internal class Facharbeiter
    {
        [Key]
        public int Angestellten_ID { get; set; }
        public string Angestellten_Name{ get; set; } 

    }
}
