using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSonga.Clases
{
    public class Result
    {
        public bool Codigo { get; set; }
        public string Description { get; set; }
        public DataSet Dt { get; set; }

    }
}
