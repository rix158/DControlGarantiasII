using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Models
{
    public class BookingDataLayer
    {
        string res = string.Empty;
        DB login = new DB();

        /*Consulta de cabecera booking*/
        public IEnumerable<Booking> GetAllBookingCab()
        {
            try
            {
                List<Booking> lstBooking = new List<Booking>();

                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_BK_CONSULTAR_BOOKING", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "C");
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Booking booking = new Booking();

                        booking.ids_bl = Int32.Parse(rdr["ids_bl"].ToString());
                        booking.cod_bl = rdr["cod_bl"].ToString();
                        booking.ciu_origen = rdr["ciu_origen"].ToString();
                        booking.ciu_destino = rdr["ciu_destino"].ToString();
                        booking.pto_origen = rdr["pto_origen"].ToString();
                        booking.pto_destino = rdr["pto_destino"].ToString();
                        booking.cod_linea = rdr["cod_linea"].ToString();
                        booking.fec_embarque = rdr["fec_embarque"].ToString();
                        /*agregar campo [array] donde vienen todos los detalles por booking
                         * se debe agrupar en un campo concatenado los detalles (contenedore)
                         por booking*/
                        lstBooking.Add(booking);
                    }
                    con.Close();
                }
                return lstBooking;
            }
            catch (Exception ex)
            {
                /*pruebas*/
                res = "Error de conexion y consulta de cabecera de booking" + ex;
                throw;
            }
        }

        /*Consulta de detalles booking*/
        //public IEnumerable<BookingDetalle> GetAllBookingDet()
        //{
        //    try
        //    {
        //        List<BookingDetalle> lstBookingDet = new List<BookingDetalle>();

        //        using (SqlConnection con = new SqlConnection(login.LoginDB()))
        //        {
        //            SqlCommand cmd = new SqlCommand("PRO_BK_CONSULTAR_BOOKING", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@flag", "D");
        //            con.Open();
        //            SqlDataReader rdr = cmd.ExecuteReader();

        //            while (rdr.Read())
        //            {
        //                BookingDetalle bookingDet = new BookingDetalle();

        //                bookingDet.ids_bl = Int32.Parse(rdr["ids_bl"].ToString());
        //                bookingDet.cod_carga= rdr["cod_carga"].ToString();
        //                bookingDet.cod_peligro = rdr["cod_peligo"].ToString();
        //                bookingDet.cod_tipcont = rdr["cod_tipocont"].ToString();
        //                bookingDet.val_peso =  Decimal.Parse(rdr["val_peso"].ToString());
        //                bookingDet.des_sello1 = rdr["des_sello1"].ToString();
        //                bookingDet.des_sello1 = rdr["des_sello2"].ToString();
        //                bookingDet.des_sello1 = rdr["des_sello3"].ToString();
                       
        //                lstBookingDet.Add(bookingDet);
        //            }
        //            con.Close();
        //        }
        //        return lstBookingDet;
        //    }
        //    catch (Exception ex)
        //    {
        //        /*pruebas*/
        //        res = "Error de conexion y consulta de detalle de booking" + ex;
        //        throw;
        //    }
        //}

    }
}
