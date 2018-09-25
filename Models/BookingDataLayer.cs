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
                        if (booking.bookingDetalleList == null)
                        {
                            booking.bookingDetalleList = new List<BookingDetalle>();
                        }
                        List<BookingDetalle> detalleBookings = new List<BookingDetalle>();
                        String[] detalles = (rdr["detalles"] != null ? rdr["detalles"].ToString() : "").Split(':');
                        foreach (String d in detalles)
                        {
                            String[] detalle = d.Split('|');
                            if (detalle[0].Trim().Length > 0)
                            {
                                BookingDetalle dbook = new BookingDetalle();
                                dbook.cod_carga = detalle[1];
                                dbook.cod_container = detalle[4];
                                dbook.cod_peligro = detalle[2];
                                dbook.cod_tipcont = detalle[3];
                                dbook.des_sello1 = detalle[6];
                                dbook.des_sello2 = detalle[7];
                                dbook.des_sello3 = detalle[8];
                                dbook.des_sello4 = detalle[9];
                                dbook.estado = detalle[10];
                                dbook.ids_bl = booking.ids_bl;

                                booking.bookingDetalleList.Add(dbook);
                            }
                        }
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

        public int ProcessBooking(ItemBooking booking)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    //BUSCAR EL BOOKING POR COD_BL PARA OBTENER LA INFORMACION PRINCIPAL 

                    //AQUI DEBE GENERAR EL ARCHIVO EDI

                    //AQUI DEBE ENVIARSE POR FTP EL ARCHIVO EDI

                    /*AQUI DEBE GUARDAR EN LA BASE DE DATOS EL REGISTRO PERO
                    SI ESTE NO SE PUEDE ENVIAR POR FTP NO DEBE MARCARSE COMO ENVIADO*/

                    con.Close();
                }
                
            }
            catch (Exception ex)
            {
                res = "Error de conexion: Mo se ha ódido procesar el Booking" + ex;
                throw;
            }
            return 0;
        }

        private String generarEDI(ItemBooking item, Booking b)
        {
            String secuencial = "000000001";//autonumerico
            String fecha = "180815";// fecha con formato yyMMdd
            String edi = "UNB+UNOA:1+COSCONA+FTS+" + fecha + ":1740+" + secuencial + "'";
            edi += "UNH+" + fecha + secuencial + "+COPARN:D:95B:UN'";
            edi += "BGM+11+" + secuencial + "+" + (item.tipo == "SEND" ? "9" : (item.tipo == "DROP" ? "1" : item.tipo == "EDIT" ? "5" :"")) + "'";
            edi += "RFF+BN:" + b.cod_bl + "'";
            edi += "TDT+20+0271-134W+1++COS:172:20+++9168843:146:11:EVER UNIFIC'";
            edi += "RFF+VON:0271-134W'";
            edi += "LOC+88+ECGYE:139:6'";
            edi += "LOC+9+ECGYE:139:6'";
            edi += "DTM+133::203'";
            edi += "NAD+CZ+999999+JAS FORWARDING DE COLOMBIA S.A.S. '";
            edi += "NAD+CF+COS:172:20'";
            edi += "EQD+CN++" + item.booking.cod_tipcont + ":102:5++2+5'";
            edi += "EQN+1'";
            edi += "FTX+AAA+++" + item.booking.cod_carga + "'";
            edi += "RFF+BN:" + b.cod_bl + "'";
            edi += "LOC+8+" + b.pto_destino + ":139:6'"; // -> este es el puerto destino
            edi += "LOC+98+" + b.pto_origen + ":139:6'"; // -> este es el puerto de embarque";
            edi += "LOC+11+" + b.pto_destino + ":139:6'"; // -> este el puerto de descarga";
            edi += " CNT+16:1'";
            edi += "UNT+19+" + fecha + secuencial + "'";
            edi += "UNZ+1+" + secuencial + "'";
            return edi;
        }
    }
}
