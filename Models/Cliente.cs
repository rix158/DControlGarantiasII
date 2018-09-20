using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Models
{
    public class Cliente
    {
        public int id { get; set; }
        public string ruc { get; set; }
        public string cliente { get; set; }
        public string cod_bl { get; set; } /*relativa dependiendo de uso*/
        public string exoneracion { get; set; }
        public string observacion { get; set; }
        public string usuario { get; set; }
        public string fechaReg { get; set; }
        public string fechaAct { get; set; }

    }
}
