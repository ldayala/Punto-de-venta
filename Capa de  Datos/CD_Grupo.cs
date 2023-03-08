using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de__Datos
{
    public class CD_Grupo
    {
        readonly CD_Conexion conexion=new CD_Conexion();
        public int IdGrupo(string nombre)
        {
            SqlCommand cmd = new SqlCommand()
            {
                CommandText = "SP_Grupo_Id",
                CommandType = CommandType.StoredProcedure,
                Connection = conexion.AbrirConnexion()
            };
            cmd.Parameters.AddWithValue("@nombre", nombre);
            object id= cmd.ExecuteScalar();
            conexion.CerrarConexion();

            return (int) id;
        }

        public string NombreGrupo(int id) {

            SqlCommand cmd = new SqlCommand()
            {
                CommandText = "SP_Grupo_Nombre",
                CommandType = CommandType.StoredProcedure,
                Connection = conexion.AbrirConnexion()
            };
            cmd.Parameters.Add("@id",SqlDbType.Int).Value=id;
            object nombre = cmd.ExecuteScalar();
            conexion.CerrarConexion();

            return (string)nombre;
        }

        public List<string> GetGrupos()
        {
            SqlCommand cmd = new SqlCommand()
            {
                CommandText = "SP_Grupo_select",
                CommandType = CommandType.StoredProcedure,
                Connection = conexion.AbrirConnexion()
            };
         
            SqlDataReader nombre = cmd.ExecuteReader();
            List<string> grupos = new List<string>();
            while (nombre.Read())
            {
                grupos.Add((string)nombre["nombre"]);
            }

            return grupos;
        }
    }
}
