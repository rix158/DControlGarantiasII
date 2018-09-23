using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Models
{
    public class GarantiaDataLayer
    {

        string res = string.Empty;
        DB login = new DB();

        public int AddGarantia(Garantia garantia,ItemGarantia ig)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    garantia = ig.contenedores[0];
                    int cantidadContenedor = 0;
                    foreach(Garantia gar in ig.contenedores)
                    {
                        cantidadContenedor += gar.contenedores;
                    }
                    string banco = string.Empty;
                    string numero_cuenta = string.Empty;
                    string valor = string.Empty;
                    string documento = string.Empty;
                    string tipo_pago = string.Empty;

                    string separador = ";";
                    string[] detalles = ig.detalle.Split(separador, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < detalles.Length; i++)
                    {
                        SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_GARANTIA", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@flag", "C");
                        cmd.Parameters.AddWithValue("@id_garantia", DBNull.Value);
                        cmd.Parameters.AddWithValue("@cod_bl", garantia.cod_bl);
                        cmd.Parameters.AddWithValue("@fecha_registro", DateTime.Now);
                        cmd.Parameters.AddWithValue("@nave", garantia.nave);
                        cmd.Parameters.AddWithValue("@cliente", ig.cliente);
                        cmd.Parameters.AddWithValue("@consignatario", garantia.consignatario);
                        cmd.Parameters.AddWithValue("@contenedores", cantidadContenedor);
                        cmd.Parameters.AddWithValue("@cod_container", garantia.cod_container);
                        cmd.Parameters.AddWithValue("@tipo_contenedor", garantia.tipo_contenedor);
                        cmd.Parameters.AddWithValue("@secuencial", DBNull.Value);

                        string[] row = detalles[i].Split("|", StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < 1; j++)
                        {
                            banco = row[0];
                            numero_cuenta = row[1];
                            valor = row[2];
                            documento = row[3];
                            tipo_pago = row[4];
                        }
                        cmd.Parameters.AddWithValue("@banco", banco);
                        cmd.Parameters.AddWithValue("@numero_cuenta", numero_cuenta);
                        cmd.Parameters.AddWithValue("@valor", valor);
                        cmd.Parameters.AddWithValue("@cheque", documento);
                        cmd.Parameters.AddWithValue("@tipo_pago", tipo_pago);

                        /*Datos de auditoria*/
                        cmd.Parameters.AddWithValue("@usuario", Environment.UserName);
                        cmd.Parameters.AddWithValue("@fechaReg", DateTime.Now);
                        cmd.Parameters.AddWithValue("@fechaAct", DateTime.Now);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    
                }
                return 1;
            }
            catch (Exception ex)
            {
                /*pruebas*/
                res = "Error de creación de garantia" + ex;
                return 0;
            }
        }

        /*Consultar garantias existentes de clientes*/
        public IEnumerable<Garantia> GetAllGarantias()
        {
            try
            {
                List<Garantia> lstGarantia = new List<Garantia>();

                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_GARANTIA", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "R");
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
                        garantia.id_garantia = Int32.Parse(rdr["id_garantia"].ToString());
                        garantia.cod_bl = rdr["cod_bl"].ToString();
                        garantia.nave = rdr["nave"].ToString();
                        garantia.contenedores = Int32.Parse(rdr["contenedores"].ToString());
                        garantia.tipo_contenedor = rdr["tipo_contenedor"].ToString();
                        garantia.cod_container = rdr["cod_container"].ToString();
                        garantia.consignatario = rdr["consignatario"].ToString();
                        garantia.cliente = rdr["cliente"].ToString();
                        garantia.fecha_registro = rdr["fecha_registro"].ToString();
                        //garantia.banco = rdr["banco"].ToString();
                        //garantia.numero_cuenta = rdr["numero_cuenta"].ToString();
                        //garantia.valor = Decimal.Parse(rdr["valor"].ToString());
                        //garantia.cheque = rdr["cheque"].ToString();
                        //garantia.valor_cheque = Decimal.Parse(rdr["valor_cheque"].ToString());
                        //garantia.tipo_pago = rdr["tipo_pago"].ToString();
                        garantia.usuario = rdr["usuario"].ToString();
                        garantia.fechaReg = rdr["fechaReg"].ToString();
                        garantia.fechaAct = rdr["fechaAct"].ToString();

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

        /*Edicion de garantia de cliente existente*/
        public int UpdateGarantia(Garantia garantia)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_GARANTIA", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "U");
                    cmd.Parameters.AddWithValue("@id_garantia", garantia.id_garantia);
                    cmd.Parameters.AddWithValue("@cod_bl", garantia.cod_bl);
                    cmd.Parameters.AddWithValue("@fecha_registro", garantia.fecha_registro);
                    cmd.Parameters.AddWithValue("@nave", garantia.nave);
                    cmd.Parameters.AddWithValue("@cliente", garantia.cliente);
                    cmd.Parameters.AddWithValue("@banco", garantia.banco);
                    cmd.Parameters.AddWithValue("@numero_cuenta", garantia.numero_cuenta);
                    cmd.Parameters.AddWithValue("@consignatario", garantia.consignatario);
                    cmd.Parameters.AddWithValue("@contenedores", garantia.contenedores);
                    cmd.Parameters.AddWithValue("@cod_container", DBNull.Value);
                    cmd.Parameters.AddWithValue("@tipo_contenedor", garantia.tipo_contenedor);
                    cmd.Parameters.AddWithValue("@valor", garantia.valor);
                    cmd.Parameters.AddWithValue("@cheque", garantia.cheque);
                    cmd.Parameters.AddWithValue("@tipo_pago", garantia.tipo_pago);
                    cmd.Parameters.AddWithValue("@secuencial", garantia.secuencial);
                    /*Datos de auditoria*/
                    cmd.Parameters.AddWithValue("@usuario", Environment.UserName);
                    cmd.Parameters.AddWithValue("@fechaReg", garantia.fechaReg);
                    cmd.Parameters.AddWithValue("@fechaAct", DateTime.Now);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch (Exception ex)
            {
                res = "Error de actualizacion de garantia" + ex;
                return 0;
            }
        }

        /*Borrar garantia existente*/
        public int DeleteGarantia(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_GARANTIA", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "D");
                    cmd.Parameters.AddWithValue("@id_garantia", id);
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
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
                return 1;
            }
            catch (Exception ex)
            {
                res = "Error al intentar eliminar la garantia existente" + ex;
                return 0;
            }
        }

        /*Consultar garantia existente cliente especifico*/
        public Garantia GetGarantiaData(int id)
        {
            try
            {
                Garantia garantia = new Garantia();

                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_GARANTIA", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "I");
                    cmd.Parameters.AddWithValue("@id_garantia", id);
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
                        garantia.id_garantia = Int32.Parse(rdr["id_garantia"].ToString());
                        garantia.cod_bl = rdr["cod_bl"].ToString();
                        garantia.fecha_registro = rdr["fecha_registro"].ToString();
                        garantia.nave = rdr["nave"].ToString();
                        garantia.cliente = rdr["cliente"].ToString();
                        garantia.banco = rdr["banco"].ToString();
                        garantia.numero_cuenta = rdr["numero_cuenta"].ToString();
                        garantia.consignatario = rdr["consignatario"].ToString();
                        garantia.contenedores = Int32.Parse(rdr["contenedores"].ToString());
                        garantia.tipo_contenedor = rdr["tipo_contenedor"].ToString();
                        garantia.cod_container = rdr["cod_container"].ToString();
                        garantia.valor = Decimal.Parse(rdr["valor"].ToString());
                        garantia.cheque = rdr["cheque"].ToString();
                        garantia.tipo_pago = rdr["tipo_pago"].ToString();
                        garantia.secuencial = rdr["secuencial"].ToString();
                        garantia.usuario = rdr["usuario"].ToString();
                        garantia.fechaReg = rdr["fechaReg"].ToString();
                        garantia.fechaAct = rdr["fechaAct"].ToString();

                    }
                    con.Close();
                }
                return garantia;
            }
            catch (Exception ex)
            {
                res = "Error de lectura por id" + ex;
                throw;
            }
        }

        /*Consultar garantias existentes de clientes*/
        public IEnumerable<Garantia> GetAllBlGarantias()
        {
            try
            {
                List<Garantia> lstGarantia = new List<Garantia>();

                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_GARANTIA", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "RBG");
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

        /*Consulta de datos garantias por medio del bl*/
        public IEnumerable<Garantia> PrevGetGarantiaData(string cod_bl)
        {
            try
            {
                List<Garantia> lsxtGarantia = new List<Garantia>();

                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_GARANTIA", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "GCR");
                    cmd.Parameters.AddWithValue("@id_garantia", DBNull.Value);
                    cmd.Parameters.AddWithValue("@cod_bl", cod_bl);
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
                        garantia.nave = rdr["nave"].ToString();
                        garantia.tipo_contenedor = rdr["tipo_contenedor"].ToString();
                        garantia.contenedores = Int32.Parse(rdr["contenedores"].ToString());
                        garantia.cod_container = rdr["cod_container"].ToString();
                        garantia.consignatario = rdr["consignatario"].ToString();
                        garantia.valor = Int32.Parse(rdr["valor"].ToString());
                        lsxtGarantia.Add(garantia);
                    }
                    con.Close();
                }
                return lsxtGarantia;
            }
            catch (Exception ex)
            {
                res = "Error de lectura por id" + ex;
                throw;
            }
        }
    }
}