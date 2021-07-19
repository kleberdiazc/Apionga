using ApiSonga.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSonga.Models
{
    public class ParamModel
    {
        public string sp { get; set; }
        public List<Parameters> param { get; set; }
        public string conexion { get; set; }
    }
}
