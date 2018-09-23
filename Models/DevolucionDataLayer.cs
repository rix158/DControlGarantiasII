using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Models
{
    public class DevolucionDataLayer
    {
        string res = string.Empty;
        DB login = new DB();

        /*Creacion de solicitud de devolucion*/
        public int AddDevolucion(Devolucion devolucion)
        {
            string drc = string.Empty;
            string eir = string.Empty;

            if (devolucion.doc_recibo_cheque == "true")
            {
                drc = "SI";
            }
            else{
                drc = "NO";
            }
            if (devolucion.doc_EIR == "true")
            {
                eir = "SI";
            }
            else
            {
                eir = "NO";
            }
            
            try
            {
                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_DEVOLUCION", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "C");
                    cmd.Parameters.AddWithValue("@id_devolucion", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cod_bl", devolucion.cod_bl);
                    cmd.Parameters.AddWithValue("@fecha_dev", DateTime.Now);
                    cmd.Parameters.AddWithValue("@consignatario", devolucion.consignatario);
                    cmd.Parameters.AddWithValue("@cliente", devolucion.cliente);
                    cmd.Parameters.AddWithValue("@email", devolucion.email);
                    cmd.Parameters.AddWithValue("@doc_recibo_cheque", drc);
                    cmd.Parameters.AddWithValue("@doc_EIR", eir);
                    cmd.Parameters.AddWithValue("@tipo_cliente", devolucion.tipo_cliente);
                    cmd.Parameters.AddWithValue("@motivo_multa", devolucion.motivo_multa);
                    cmd.Parameters.AddWithValue("@estado_apr", "AE"); //aprobacion en espera
                    cmd.Parameters.AddWithValue("@cheque", DBNull.Value);
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
                /*pruebas*/
                res = "Error de creación de solicitud de devolucion" + ex;
                return 0;
            }
        }

        /*Consulta estado de devoluciones*/
        public IEnumerable<Devolucion> GetAllDevoluciones()
        {
            try
            {
                List<Devolucion> lstDevolucion = new List<Devolucion>();

                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_DEVOLUCION", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "R");
                    cmd.Parameters.AddWithValue("@id_devolucion", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cod_bl", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("@consignatario", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fecha_dev", DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", DBNull.Value);
                    cmd.Parameters.AddWithValue("@doc_recibo_cheque", DBNull.Value);
                    cmd.Parameters.AddWithValue("@doc_EIR", DBNull.Value);
                    cmd.Parameters.AddWithValue("@motivo_multa", DBNull.Value);
                    cmd.Parameters.AddWithValue("@tipo_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("@estado_apr", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cheque", DBNull.Value);
                    /*Datos de auditoria*/
                    cmd.Parameters.AddWithValue("@usuario", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaReg", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaAct", DBNull.Value);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Devolucion devolucion = new Devolucion();

                        devolucion.id_devolucion = Int32.Parse(rdr["id_devolucion"].ToString());
                        devolucion.cod_bl = rdr["cod_bl"].ToString();
                        devolucion.cliente = rdr["cliente"].ToString();
                        devolucion.consignatario = rdr["consignatario"].ToString();
                        devolucion.fecha_dev = rdr["fecha_dev"].ToString();
                        devolucion.email = rdr["email"].ToString();
                        devolucion.doc_recibo_cheque = rdr["doc_recibo_cheque"].ToString();
                        devolucion.doc_EIR = rdr["doc_EIR"].ToString();
                        devolucion.motivo_multa = rdr["motivo_multa"].ToString();
                        devolucion.tipo_cliente = rdr["tipo_cliente"].ToString();
                        devolucion.estado_apr = rdr["estado_apr"].ToString();
                        devolucion.cheque = rdr["cheque"].ToString();
                        /*Datos de auditoria*/
                        devolucion.usuario = rdr["usuario"].ToString();
                        devolucion.fechaReg = rdr["fechaReg"].ToString();
                        devolucion.fechaAct = rdr["fechaAct"].ToString();

                        lstDevolucion.Add(devolucion);
                    }
                    con.Close();
                }
                return lstDevolucion;
            }
            catch (Exception ex)
            {
                /*pruebas*/
                res = "Error de conexion y consulta de devoluciones" + ex;
                throw;
            }
        }

        /*Consulta estado de devoluciones aprobadas*/
        public IEnumerable<Devolucion> GetAllDevolucionesAp()
        {
            try
            {
                List<Devolucion> lstDevolucion = new List<Devolucion>();

                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_DEVOLUCION", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "RA");
                    cmd.Parameters.AddWithValue("@id_devolucion", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cod_bl", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("@consignatario", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fecha_dev", DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", DBNull.Value);
                    cmd.Parameters.AddWithValue("@doc_recibo_cheque", DBNull.Value);
                    cmd.Parameters.AddWithValue("@doc_EIR", DBNull.Value);
                    cmd.Parameters.AddWithValue("@motivo_multa", DBNull.Value);
                    cmd.Parameters.AddWithValue("@tipo_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("@estado_apr", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cheque", DBNull.Value);
                    /*Datos de auditoria*/
                    cmd.Parameters.AddWithValue("@usuario", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaReg", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaAct", DBNull.Value);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Devolucion devolucion = new Devolucion();

                        devolucion.id_devolucion = Int32.Parse(rdr["id_devolucion"].ToString());
                        devolucion.cod_bl = rdr["cod_bl"].ToString();
                        devolucion.cliente = rdr["cliente"].ToString();
                        devolucion.consignatario = rdr["consignatario"].ToString();
                        devolucion.fecha_dev = rdr["fecha_dev"].ToString();
                        devolucion.email = rdr["email"].ToString();
                        devolucion.doc_recibo_cheque = rdr["doc_recibo_cheque"].ToString();
                        devolucion.doc_EIR = rdr["doc_EIR"].ToString();
                        devolucion.motivo_multa = rdr["motivo_multa"].ToString();
                        devolucion.tipo_cliente = rdr["tipo_cliente"].ToString();
                        devolucion.estado_apr = rdr["estado_apr"].ToString();
                        devolucion.cheque = rdr["cheque"].ToString();
                        /*Datos de auditoria*/
                        devolucion.usuario = rdr["usuario"].ToString();
                        devolucion.fechaReg = rdr["fechaReg"].ToString();
                        devolucion.fechaAct = rdr["fechaAct"].ToString();

                        lstDevolucion.Add(devolucion);
                    }
                    con.Close();
                }
                return lstDevolucion;
            }
            catch (Exception ex)
            {
                /*pruebas*/
                res = "Error de conexion y consulta de devoluciones" + ex;
                throw;
            }
        }

        /*Consultar garantias existentes de clientes*/
        public IEnumerable<Garantia> GetAllBlDevoluciones()
        {
            try
            {
                List<Garantia> lstGarantia = new List<Garantia>();

                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_GARANTIA", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "RBD");
                    cmd.Parameters.AddWithValue("@id_garantia", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cod_bl", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fecha_registro", DBNull.Value);
                    cmd.Parameters.AddWithValue("@nave", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("@banco", DBNull.Value);
                    cmd.Parameters.AddWithValue("@numero_cuenta", DBNull.Value);
                    cmd.Parameters.AddWithValue("@consignatario", DBNull.Value);
                    cmd.Parameters.AddWithValue("@contenedores", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cod_container", DBNull.Value);
                    cmd.Parameters.AddWithValue("@tipo_contenedor", DBNull.Value);
                    cmd.Parameters.AddWithValue("@valor", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cheque", DBNull.Value);
                    cmd.Parameters.AddWithValue("@tipo_pago", DBNull.Value);
                    cmd.Parameters.AddWithValue("@secuencial", DBNull.Value);
                    /*Datos de auditoria*/
                    cmd.Parameters.AddWithValue("@usuario", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaReg", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaAct", DBNull.Value);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Garantia garantia = new Garantia();
                        garantia.cod_bl = rdr["cod_bl"].ToString();
                        lstGarantia.Add(garantia);
                    }
                    con.Close();
                }
                return lstGarantia;
            }
            catch (Exception ex)
            {
                /*pruebas*/
                res = "Error de conexion y consulta de garantias" + ex;
                throw;
            }

        }


        /*Aprobacion de DEVOLUCION (cambio de estado)*/
        public Devolucion UpdateDevolucion(int id)
        {
            try
            {
                Devolucion devolucion = new Devolucion();
                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_DEVOLUCION", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "UE");
                    cmd.Parameters.AddWithValue("@id_devolucion", id);
                    cmd.Parameters.AddWithValue("@cod_bl", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("@consignatario", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fecha_dev", DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", DBNull.Value);
                    cmd.Parameters.AddWithValue("@doc_recibo_cheque", DBNull.Value);
                    cmd.Parameters.AddWithValue("@doc_EIR", DBNull.Value);
                    cmd.Parameters.AddWithValue("@motivo_multa", DBNull.Value);
                    cmd.Parameters.AddWithValue("@tipo_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("@estado_apr", "AC");
                    cmd.Parameters.AddWithValue("@cheque", devolucion.cheque);

                    /*Datos de auditoria*/
                    cmd.Parameters.AddWithValue("@usuario", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaReg", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaAct", DBNull.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return devolucion;
            }
            catch (Exception ex)
            {
                res = "Error de aprobació de garantia" + ex;
                throw;
            }
        }

        //Obtener detalles de devolucion especifico
        public Devolucion GetDevolucionData(int id)
        {
            try
            {
                Devolucion devolucion = new Devolucion();

                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_DEVOLUCION", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "RD");
                    cmd.Parameters.AddWithValue("@id_devolucion", id);
                    cmd.Parameters.AddWithValue("@cod_bl", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("@consignatario", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fecha_dev", DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", DBNull.Value);
                    cmd.Parameters.AddWithValue("@doc_recibo_cheque", DBNull.Value);
                    cmd.Parameters.AddWithValue("@doc_EIR", DBNull.Value);
                    cmd.Parameters.AddWithValue("@motivo_multa", DBNull.Value);
                    cmd.Parameters.AddWithValue("@tipo_cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("@estado_apr", DBNull.Value);
                    /*Datos de auditoria*/
                    cmd.Parameters.AddWithValue("@usuario", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaReg", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaAct", DBNull.Value);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        devolucion.id_devolucion = Int32.Parse(rdr["id_devolucion"].ToString());
                        devolucion.cod_bl = rdr["cod_bl"].ToString();
                        devolucion.cliente = rdr["cliente"].ToString();
                        devolucion.consignatario = rdr["consignatario"].ToString();
                        devolucion.fecha_dev = rdr["fecha_dev"].ToString();
                        devolucion.email = rdr["email"].ToString();
                        devolucion.doc_recibo_cheque = rdr["doc_recibo_cheque"].ToString();
                        devolucion.doc_EIR = rdr["doc_EIR"].ToString();
                        devolucion.motivo_multa = rdr["motivo_multa"].ToString();
                        devolucion.tipo_cliente = rdr["tipo_cliente"].ToString();
                        devolucion.estado_apr = rdr["estado_apr"].ToString();
                        /*Datos de auditoria*/
                        devolucion.usuario = rdr["usuario"].ToString();
                        devolucion.fechaReg = rdr["fechaReg"].ToString();
                        devolucion.fechaAct = rdr["fechaAct"].ToString();
                    }
                    con.Close();
                }
                return devolucion;
            }

            catch (Exception ex)
            {
                res = "Error al intentar consultar la devolucion" + ex;
                throw;
            }
        }
    }
}