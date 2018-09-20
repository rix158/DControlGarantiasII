
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DControlGarantiasII.Models;
using Microsoft.AspNetCore.Mvc;


namespace DControlGarantiasII.Controllers
{
    public class CierreController : Controller
    {
        CierreDataLayer cierrei = new CierreDataLayer();
        CierreDataLayer cierree = new CierreDataLayer();

        /*Peticion de reporte de ingresos - pagos*/
        [HttpGet("[action]")]
        [Route("api/CierreI/Index")]
        public IEnumerable<Cierre> IndexI()
        {
            return cierrei.GetAllCierreI();
        }

        /*Peticion de reporte de egresos - devoluciones*/
        [HttpGet("[action]")]
        [Route("api/CierreE/Index")]
        public IEnumerable<Cierre> IndexE()
        {
            return cierree.GetAllCierreE();
        }
    }
}