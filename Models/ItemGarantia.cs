using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Models
{
    public class ItemGarantia
    {
        public string bl { get; set; }
        public List<Garantia> contenedores { get; set; }
        public string cliente { get; set; }
        public string detalle { get; set; }
    }
}
