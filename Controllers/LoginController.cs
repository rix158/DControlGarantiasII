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
        [Route("api/Login/Index/{usuario}/{password}")]
        public Login IndexE(string usuario, string password)
        {
            return login.GetLogins(usuario,password);
        }
    }
}
