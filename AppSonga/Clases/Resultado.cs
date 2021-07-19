using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AppSonga.Clases
{
    public class Resultado
    {
        public string Error { get; set; }
        public string Mensaje { get; set; }
        public DataSet DT { get; set; }
    }
}
