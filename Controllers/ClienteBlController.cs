using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DControlGarantiasII.Models;
using Microsoft.AspNetCore.Mvc;


namespace DControlGarantiasII.Controllers
{
    public class ClienteBlController : Controller
    {
        ClienteBlDataLayer objClientebl = new ClienteBlDataLayer();

        [HttpGet("[action]")]
        [Route("api/ClienteBl/Index")]
        public IEnumerable<Cliente> Index()
        {
            return objClientebl.GetAllClientesBl();
        }

        [HttpPost]
        [Route("api/ClienteBl/Create")]
        public int Create([FromBody] Cliente Cliente)
        {
            return objClientebl.AddClienteBl(Cliente);
        }

        [HttpGet]
        [Route("api/ClienteBl/Details/{id}")]
        public Cliente Details(int id)
        {
            return objClientebl.GetClienteBlData(id);
        }

        [HttpPut]
        [Route("api/ClienteBl/Edit")]
        public int Edit([FromBody]Cliente Cliente)
        {
             return objClientebl.UpdateClienteBl(Cliente);   
        }

        [HttpDelete]
        [Route("api/ClienteBl/Delete/{id}")]
        public int Delete(int id)
        {
            return objClientebl.DeleteClienteBl(id);
        }

    }
}