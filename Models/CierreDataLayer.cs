using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DControlGarantiasII.Models
{
    public class CierreDataLayer
    {

        string res;
        DB login = new DB();

        /*CONSULTA TODOS LOS CIERRES DE PAGOS*/
        public IEnumerable<Cierre> GetAllCierreI()
        {
            try
            {
                List<Cierre> lstCierreI = new List<Cierre>();

                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CS_CIERRE", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "I");
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Cierre cierrei = new Cierre();
                        cierrei.total_ingresos = Decimal.Parse(rdr["total_ingresos"].ToString());
                        cierrei.fecha_registro = rdr["fecha_registro"].ToString();
                        cierrei.tipo_pago = rdr["tipo_pago"].ToString();

                        lstCierreI.Add(cierrei);
                    }
                    con.Close();
                }
                return lstCierreI;
            }
            catch (Exception ex)
            {
                res = "Error de consulta de cierre de ingresos" + ex;
                throw;
            }
        }

        /*CONSULTA TODOS LOS CIERRES DE DEVOLUCIONES*/
        public IEnumerable<Cierre> GetAllCierreE()
        {
            try
            {
                List<Cierre> lstCierreE = new List<Cierre>();

                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CS_CIERRE", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "E");
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Cierre cierree = new Cierre();
                        cierree.total_ingresos = Decimal.Parse(rdr["total_ingresos"].ToString());
                        cierree.fecha_registro = rdr["fecha_registro"].ToString();
                        cierree.tipo_pago = rdr["tipo_pago"].ToString();

                        lstCierreE.Add(cierree);
                    }
                    con.Close();
                }
                return lstCierreE;
            }
            catch (Exception ex)
            {
                res = "Error de consulta de cierre de egresos" + ex;
                throw;
            }
        }

    }
}
