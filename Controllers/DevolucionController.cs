﻿using DControlGarantiasII.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Controllers
{
    public class DevolucionController : Controller
    {
        DevolucionDataLayer objDevolucion = new DevolucionDataLayer();
        DevolucionApDataLayer objDevolucionAp = new DevolucionApDataLayer();

        [HttpGet("[action]")]
        [Route("api/Devolucion/Index")]
        public IEnumerable<Devolucion> Index()
        {
            return objDevolucion.GetAllDevoluciones();
        }

        [HttpGet("[action]")]
        [Route("api/DevolucionAp/Index")]
        public IEnumerable<Devolucion> IndexAp()
        {
            return objDevolucion.GetAllDevolucionesAp();
        }

        [HttpPost]
        [Route("api/Devolucion/Create")]
        public int Create([FromBody] Devolucion devolucion)
        {
            return objDevolucion.AddDevolucion(devolucion);
        }

        [HttpPost]
        [Route("api/Devolucion/Aprobar")]
        public int Aprobar([FromBody] Devolucion devolucion)
        {
            return objDevolucion.UpdateDevolucion(devolucion);
        }

        [HttpGet("[action]")]
        [Route("api/DevolucionBl/Index")]
        public IEnumerable<Garantia> IndexBl()
        {
            return objDevolucion.GetAllBlDevoluciones();
        }

        [HttpGet]
        [Route("api/DevolucionAp/Details/{id}")]
        public DevolucionAp DetailsAp(int id)
        {
            return objDevolucionAp.GetDevolucionDataAp(id);
        }

        [HttpPut]
        [Route("api/DevolucionAp/Edit")]
        public int Edit([FromBody]DevolucionAp devolucionAp)
        {
            return objDevolucionAp.UpdateDevolucionAp(devolucionAp);
        }
    }
}
