using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Models
{
    public class BookingDetalle
    {
        public int ids_bl { get; set; }
        public string cod_carga { get; set; }
        public string cod_peligro { get; set; }
        public string cod_tipcont { get; set; }
        public string cod_container { get; set; }
        public decimal val_peso { get; set; }
        public string des_sello1 { get; set; }
        public string des_sello2 { get; set; }
        public string des_sello3 { get; set; }
        public string des_sello4 { get; set; }

    }
}
