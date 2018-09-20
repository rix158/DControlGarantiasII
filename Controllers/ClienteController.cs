using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DControlGarantiasII.Models;
using Microsoft.AspNetCore.Mvc;


namespace DControlGarantiasII.Controllers
{
    public class ClienteController : Controller
    {
        ClienteDataLayer objCliente = new ClienteDataLayer();

        [HttpGet("[action]")]
        [Route("api/Cliente/Index")]
        public IEnumerable<Cliente> Index()
        {
            return objCliente.GetAllClientes();
        }

        [HttpPost]
        [Route("api/Cliente/Create")]
        public int Create([FromBody] Cliente Cliente)
        {
            return objCliente.AddCliente(Cliente);
        }

        [HttpGet]
        [Route("api/Cliente/Details/{id}")]
        public Cliente Details(int id)
        {
            return objCliente.GetClienteData(id);
        }

        [HttpPut]
        [Route("api/Cliente/Edit")]
        public int Edit([FromBody]Cliente Cliente)
        {
            return objCliente.UpdateCliente(Cliente);
        }

        [HttpDelete]
        [Route("api/Cliente/Delete/{id}")]
        public int Delete(int id)
        {
            return objCliente.DeleteCliente(id);
        }

    }
}