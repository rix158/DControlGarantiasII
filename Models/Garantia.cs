using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Models
{
    public class Garantia
    {
        public int id_garantia { get; set; }
        public string cod_bl { get; set;}
        public string fecha_registro { get; set; }
        public string nave { get; set; }
        public string cliente { get; set; }
        public string banco { get; set; }
        public string numero_cuenta { get; set; }
        public string consignatario { get; set; }
        public int contenedores { get; set; }
        public string cod_container { get; set; }
        public string tipo_contenedor { get; set; }
        public decimal valor { get; set; }
        public string cheque { get; set; }
        public string tipo_pago { get; set; }
        public string secuencial { get; set; }
        public string usuario { get; set; }
        public string fechaReg { get; set; }
        public string fechaAct { get; set; }
    }
}
