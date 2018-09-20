using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Models
{
    public class Login
    {
        public int id_usuario { get; set; }
        public string usuario { get; set; }
        public string contrasenia {get;set;}
        public string cod_rol { get; set; }
        public string rol { get; set; }
    }
}
