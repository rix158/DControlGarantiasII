using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Models
{
    public class DevolucionAp
    {
        public int id_det { get; set; }
        public int id_devolucion { get; set; }
        public string cheque { get; set; }
        public string fecha_dev { get; set; }    
        public string cliente_recibe { get; set; }
        public string identificacion { get; set; }
        public string observacion { get; set; }
        public string usuario { get; set; }
        public string fechaReg { get; set; }
        public string fechaAct { get; set; }
    }
}