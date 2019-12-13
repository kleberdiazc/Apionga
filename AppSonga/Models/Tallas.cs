using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppSonga.Models
{
    public class Tallas
    {
        public Int64 id { get; set; }
        public int codTallaEmpaque { get; set; }
        public string tallaEmpaque { get; set; }
        public int codTallaVenta{ get; set; }
        public string tallaVenta { get; set; }

    }
}
