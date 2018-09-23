using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Models
{
    public class ClienteBlDataLayer
    {

        string res;
        DB login = new DB();

        //Crear nuevo bl por cliente
        public int AddClienteBl(Cliente cliente)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_EXONERACION", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "C");
                    cmd.Parameters.AddWithValue("@id", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ruc", cliente.ruc);
                    cmd.Parameters.AddWithValue("@cliente", cliente.cliente);
                    cmd.Parameters.AddWithValue("@cod_bl", cliente.cod_bl);
                    cmd.Parameters.AddWithValue("@exoneracion", cliente.exoneracion);
                    cmd.Parameters.AddWithValue("@observacion", cliente.observacion);
                    /*Datos de auditoria*/
                    cmd.Parameters.AddWithValue("@usuario", Environment.UserName);
                    cmd.Parameters.AddWithValue("@fechaReg", DateTime.Now);
                    cmd.Parameters.AddWithValue("@fechaAct", DBNull.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch (Exception ex)
            {
                /*pruebas*/
                res = "Error de creación de usuario" + ex;
                throw;
            }
        }


        /*Consultar bl por clientes con exoneracion*/
        public IEnumerable<Cliente> GetAllClientesBl()
        {
            try
            {
                List<Cliente> lstCliente = new List<Cliente>();

                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_EXONERACION", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "R");
                    cmd.Parameters.AddWithValue("@id", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ruc", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cod_bl", DBNull.Value);
                    cmd.Parameters.AddWithValue("@exoneracion", DBNull.Value);
                    cmd.Parameters.AddWithValue("@observacion", DBNull.Value);
                    cmd.Parameters.AddWithValue("@usuario", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaReg", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaAct", DBNull.Value);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Cliente cliente = new Cliente();
                        cliente.id = Int32.Parse(rdr["id"].ToString());
                        cliente.ruc = rdr["ruc"].ToString();
                        cliente.cliente = rdr["cliente"].ToString();
                        cliente.cod_bl = rdr["cod_bl"].ToString();
                        cliente.exoneracion = rdr["exoneracion"].ToString();
                        cliente.observacion = rdr["observacion"].ToString();
                        cliente.usuario = rdr["usuario"].ToString();
                        cliente.fechaReg = rdr["fechaReg"].ToString();
                        cliente.fechaAct = rdr["fechaAct"].ToString();

                        lstCliente.Add(cliente);
                    }
                    con.Close();
                }
                return lstCliente;
            }
            catch (Exception ex)
            {
                res = "Error de consulta de clientes" + ex;
                throw;
            }
        }

        /*Actualizar bl por cliente existente*/
        public int UpdateClienteBl(Cliente cliente)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_EXONERACION", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@flag", "U");
                    cmd.Parameters.AddWithValue("@id", cliente.id);
                    cmd.Parameters.AddWithValue("@ruc", cliente.ruc);
                    cmd.Parameters.AddWithValue("@cliente", cliente.cliente);
                    cmd.Parameters.AddWithValue("@cod_bl", cliente.cod_bl);
                    cmd.Parameters.AddWithValue("@exoneracion", cliente.exoneracion);
                    cmd.Parameters.AddWithValue("@observacion", cliente.observacion);
                    /*Datos de auditoria*/
                    cmd.Parameters.AddWithValue("@usuario", Environment.UserName);
                    cmd.Parameters.AddWithValue("@fechaReg", cliente.fechaReg);
                    cmd.Parameters.AddWithValue("@fechaAct", DateTime.Now);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /*Eliminar bl por cliente existente*/
        public int DeleteClienteBl(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_EXONERACION", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "D");
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@ruc", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cod_bl", DBNull.Value);
                    cmd.Parameters.AddWithValue("@exoneracion", DBNull.Value);
                    cmd.Parameters.AddWithValue("@observacion", DBNull.Value);
                    cmd.Parameters.AddWithValue("@usuario", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaReg", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaAct", DBNull.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
                return 1;
            }
            catch (Exception ex)
            {
                res = "Error en la eleiminacion del cliente" + ex;
                throw;
            }
        }

        //Obtener detalles de bl por cliente especifico
        public Cliente GetClienteBlData(int id)
        {
            try
            {
                Cliente cliente = new Cliente();

                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_EXONERACION", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "I");
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@ruc", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cliente", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cod_bl", DBNull.Value);
                    cmd.Parameters.AddWithValue("@exoneracion", DBNull.Value);
                    cmd.Parameters.AddWithValue("@observacion", DBNull.Value);
                    cmd.Parameters.AddWithValue("@usuario", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaReg", DBNull.Value);
                    cmd.Parameters.AddWithValue("@fechaAct", DBNull.Value);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        cliente.id = Convert.ToInt32(rdr["id"]);
                        cliente.ruc = rdr["ruc"].ToString();
                        cliente.cliente = rdr["cliente"].ToString();
                        cliente.cod_bl = rdr["cod_bl"].ToString();
                        cliente.exoneracion = rdr["exoneracion"].ToString();
                        cliente.observacion = rdr["observacion"].ToString();
                        cliente.fechaReg = rdr["fechaReg"].ToString();
                    }
                    con.Close();
                }
                return cliente;
            }

            catch (Exception ex)
            {
                res = "Error al intentar consultar el cliente" + ex;
                throw;
            }
        }

    }

}