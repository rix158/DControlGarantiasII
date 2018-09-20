using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Models
{
    public class Devolucion
    {
        public int id_devolucion { get; set; }
        public string cod_bl { get; set; }
        public string cliente { get; set; }
        public string consignatario { get; set; }
        public string fecha_dev { get; set; }
        public string email { get; set; }
        public string doc_recibo_cheque { get; set; }
        public string doc_EIR { get; set; }
        public string motivo_multa { get; set; }
        public string tipo_cliente { get; set; }
        public string estado_apr { get; set; }
        public string cheque { get; set; }
        public string usuario { get; set; }
        public string fechaReg { get; set; }
        public string fechaAct { get; set; }
    }
}   