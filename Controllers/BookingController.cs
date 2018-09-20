using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DControlGarantiasII.Models;
using Microsoft.AspNetCore.Mvc;


namespace DControlGarantiasII.Controllers
{
    public class BookingController : Controller
    {
        BookingDataLayer objBooking = new BookingDataLayer();

        /*Traer todos los booking*/
        [HttpGet("[action]")]
        [Route("api/Booking/Index")]
        public IEnumerable<Booking> Index()
        {
            return objBooking.GetAllBookingCab();
        }

        /*Metodo para creacion*/
        //[HttpPost]
        //[Route("api/Booking/Create")]
        //public int Create([FromBody] Booking Booking)
        //{
        //    return objBooking.AddBooking(Booking);
        //}
        
    }
}