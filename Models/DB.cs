using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Models
{
    public class DB
    {
        public String LoginDB()
        {
            //string cx = "Server=192.168.1.15;Database=cs; user id=sa; password=losinkas;";
            //return cx;

            string cx = "Server=127.0.0.1;Database=cs;Integrated Security=True;";
            return cx;
        }
    }
}
