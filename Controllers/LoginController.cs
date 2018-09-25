using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DControlGarantiasII.Models;
using Microsoft.AspNetCore.Mvc;


namespace DControlGarantiasII.Controllers
{
    public class LoginController : Controller
    {
        LoginDataLayer login = new LoginDataLayer();

        [HttpGet("[action]")]
        [Route("api/Login/Index")]
        public Login IndexE([FromQuery]string usuario, [FromQuery]string password)
        {
            return login.GetLogins(usuario, password);
        }
    }
}
