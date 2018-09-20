using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Models
{
    public class Cierre
    {
        public decimal total_ingresos { get; set; }
        public string fecha_registro { get; set; }
        public string tipo_pago { get; set; }
    }
}
