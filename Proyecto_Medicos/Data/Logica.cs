using Proyecto_Medicos.Models;
using System.Data;
using System.Data.SqlClient;

namespace Proyecto_Medicos.Data
{
    public class Logica
    {



        public Usuarios EncontrarUsuario(string CORREO_USUARIO, string CONTRASENIA_USUARIO)
        {

            Usuarios objeto = new Usuarios();


            using (SqlConnection conexion = new SqlConnection("Data Source=LUISRIMAC\\SQLEXPRESS ; Initial Catalog=CITAS_MEDICAS2023; Integrated Security=true"))
            {

                string query = "SELECT ID_USUARIO,NOMBRE_USUARIO, APELLIDOS_USAURIO, CORREO_USUARIO, CONTRASENIA_USUARIO, CONFIRMAR_USUARIO,ID_ROL FROM TB_USUARIO WHERE CORREO_USUARIO = @pCORREO_USUARIO and CONTRASENIA_USUARIO = @pCONTRASENIA_USUARIO";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@pCORREO_USUARIO", CORREO_USUARIO);
                cmd.Parameters.AddWithValue("@pCONTRASENIA_USUARIO", CONTRASENIA_USUARIO);
                cmd.CommandType = CommandType.Text;


                conexion.Open();


                using ( SqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {

                        objeto = new Usuarios()
                        {
                            id = dr["ID_USUARIO"].ToString(),
                            nombre = dr["NOMBRE_USUARIO"].ToString(),
                            apellido = dr["APELLIDOS_USAURIO"].ToString(),
                            correo = dr["CORREO_USUARIO"].ToString(),
                            clave = dr["CONTRASENIA_USUARIO"].ToString(),
                            confimar_clave = dr["CONFIRMAR_USUARIO"].ToString(),
                            idRol = (Rol)dr["ID_ROL"],

                        };
                    }

                }
            }
            return objeto;

        }
    }

}