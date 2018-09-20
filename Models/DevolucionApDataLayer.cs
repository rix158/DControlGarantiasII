using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Models
{
    public class DevolucionApDataLayer
    {

        string res = string.Empty;
        DB login = new DB();

        /*Creacion de solicitud de devolucion ap*/
        //public int AddDevolucionAp(DevolucionAp devolucionap)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(login.LoginDB()))
        //        {
        //            SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_DEVOLUCION_AP", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@flag", "C");
        //            cmd.Parameters.AddWithValue("@id_det", DBNull.Value);
        //            cmd.Parameters.AddWithValue("@id_devolucion", devolucionap.id_devolucion);
        //            cmd.Parameters.AddWithValue("@cod_bl", devolucionap.cod_bl);
        //            cmd.Parameters.AddWithValue("@num_recibo", devolucionap.num_recibo);
        //            cmd.Parameters.AddWithValue("@clave_acceso_cli", devolucionap.clave_acceso_cli);
        //            cmd.Parameters.AddWithValue("@entrega_cheque", devolucionap.entrega_cheque);
        //            cmd.Parameters.AddWithValue("@fecha_entrega", devolucionap.fecha_entrega);
        //            cmd.Parameters.AddWithValue("@cliente_recibe", devolucionap.cliente_recibe);
        //            cmd.Parameters.AddWithValue("@observacion", devolucionap.observacion);
        //            /*Datos de auditoria*/
        //            cmd.Parameters.AddWithValue("@usuario", Environment.UserName);
        //            cmd.Parameters.AddWithValue("@fechaReg", DateTime.Now);
        //            cmd.Parameters.AddWithValue("@fechaAct", DateTime.Now);

        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();
        //        }
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        res = "Error al registrar una nueva devolucion" + ex;
        //        return 0;
        //    }
        //}


        /*Creacion de solicitud de devolucion ap*/
        public int UpdateDevolucionAp(DevolucionAp devolucionap)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_DEVOLUCION_AP", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "C");
                    cmd.Parameters.AddWithValue("@id_det", DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_devolucion", devolucionap.id_devolucion);
                    cmd.Parameters.AddWithValue("@cheque", devolucionap.cheque);
                    cmd.Parameters.AddWithValue("@fecha_dev", devolucionap.fecha_dev);
                    cmd.Parameters.AddWithValue("@cliente_recibe", devolucionap.cliente_recibe);
                    cmd.Parameters.AddWithValue("@identificacion", devolucionap.identificacion);
                    cmd.Parameters.AddWithValue("@observacion", devolucionap.observacion);
                    /*Datos de auditoria*/
                    cmd.Parameters.AddWithValue("@usuario", Environment.UserName);
                    cmd.Parameters.AddWithValue("@fechaReg", DateTime.Now);
                    cmd.Parameters.AddWithValue("@fechaAct", DateTime.Now);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch (Exception ex)
            {
                res = "Error de registro de devolución de garantia" + ex;
                throw;
            }
        }

        //Obtener detalles de devolucion especifico
        public DevolucionAp GetDevolucionDataAp(int id)
        {
            try
            {
                DevolucionAp devolucionap = new DevolucionAp();

                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_DEVOLUCION_AP", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "RD");
                    cmd.Parameters.AddWithValue("@id_det", DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_devolucion", id);
                    cmd.Parameters.AddWithValue("@cheque", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fecha_dev", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cliente_recibe", DBNull.Value);
                    cmd.Parameters.AddWithValue("@identificacion", DBNull.Value);
                    cmd.Parameters.AddWithValue("@observacion", DBNull.Value);
                    /*Datos de auditoria*/
                    cmd.Parameters.AddWithValue("@usuario", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaReg", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaAct", DBNull.Value);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        devolucionap.id_devolucion = Int32.Parse(rdr["id_devolucion"].ToString());
                        devolucionap.id_det = Int32.Parse(rdr["id_det"].ToString());
                        devolucionap.id_devolucion = Int32.Parse(rdr["id_devolucion"].ToString());
                        devolucionap.cheque = rdr["cheque"].ToString();
                        devolucionap.fecha_dev = rdr["fecha_dev"].ToString();
                        devolucionap.cliente_recibe = rdr["cliente_recibe"].ToString();
                        devolucionap.identificacion = rdr["identificacion"].ToString();
                        devolucionap.observacion = rdr["observacion"].ToString();
                        /*Datos de auditoria*/
                        devolucionap.usuario = rdr["usuario"].ToString();
                        devolucionap.fechaReg = rdr["fechaReg"].ToString();
                        devolucionap.fechaAct = rdr["fechaAct"].ToString();
                    }
                    con.Close();
                }
                return devolucionap;
            }
            catch (Exception ex)
            {
                res = "Error al intentar consultar la devolucion" + ex;
                throw;
            }
        }
    }
}
