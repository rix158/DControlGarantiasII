using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DControlGarantiasII.Models
{
    public class LoginDataLayer
    {
        DB login = new DB();
        string res = string.Empty;

        public Login GetLogins(string usuario, string password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(login.LoginDB()))
                {
                    SqlCommand cmd = new SqlCommand("PRO_CG_CONSULTAR_LOGIN", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "R");
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@contrasenia", password);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Login ulogin = new Login();
                        ulogin.id_usuario = Int32.Parse(rdr["id_usuario"].ToString());
                        ulogin.usuario = rdr["usuario"].ToString();
                        ulogin.cod_rol = rdr["cod_rol"].ToString();
                        ulogin.rol = rdr["rol"].ToString();

                        return ulogin;
                    }
                    con.Close();
                }
                return null;
            }
            catch (Exception ex)
            {
                res = "Error de consulta de clientes" + ex;
                throw;
            }
        }
    }
}
