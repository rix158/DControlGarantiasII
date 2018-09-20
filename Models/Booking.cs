using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Models
{
    public class Booking
    {
        public int ids_bl { get; set; }
        public string cod_bl { get; set; }
        public string ciu_origen { get; set; }
        public string ciu_destino { get; set; }
        public string pto_origen { get; set; }
        public string pto_destino { get; set; }
        public string cod_linea { get; set; }
        public string fec_embarque { get; set; }

    }
}