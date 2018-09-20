using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DControlGarantiasII.Models;

namespace DControlGarantiasII.Controllers
{
    public class GarantiaController : Controller
    {
        GarantiaDataLayer objGarantia = new GarantiaDataLayer();

        [HttpGet("[action]")]
        [Route("api/Garantia/Index")]
        public IEnumerable<Garantia> Index()
        {
            return objGarantia.GetAllGarantias();
        }

        [HttpPost]
        [Route("api/Garantia/Create")]
        public int Create([FromBody] ItemGarantia itemGarantia)
        {
            return objGarantia.AddGarantia(null, itemGarantia);
        }

        [HttpGet]
        [Route("api/Garantia/Details/{id}")]
        public Garantia Details(int id)
        {
            return objGarantia.GetGarantiaData(id);
        }

        [HttpPut]
        [Route("api/Garantia/Edit")]
        public int Edit([FromBody]Garantia Garantia)
        {
            return objGarantia.UpdateGarantia(Garantia);
        }

        [HttpDelete]
        [Route("api/Garantia/Delete/{id}")]
        public int Delete(int id)
        {
            return objGarantia.DeleteGarantia(id);
        }

        //GetAllBlGarantias
        [HttpGet("[action]")]
        [Route("api/GarantiaBl/Index")]
        public IEnumerable<Garantia> IndexBl()
        {
            return objGarantia.GetAllBlGarantias();
        }

        [HttpGet]
        [Route("api/Garantia/PrevDetails/{cod_bl}")]
        public IEnumerable<Garantia> PrevDetails(string cod_bl)
        {
            return objGarantia.PrevGetGarantiaData(cod_bl);
        }

        
    }
}