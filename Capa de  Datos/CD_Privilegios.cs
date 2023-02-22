using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de__Datos
{
    public class CD_Privilegios
    {
        private readonly CD_Conexion conexion=new CD_Conexion();
        private CE_Privilegios privilegios=new CE_Privilegios();

        #region Id Privilegio

        public int IdPrivilegio(string NombrePrivilegio)
        {
            SqlCommand cmd = new SqlCommand()
            {
                CommandText = "Mostrar_Privi",
                CommandType = CommandType.StoredProcedure,
                Connection= conexion.AbrirConnexion()
            };

            cmd.Parameters.AddWithValue("@privilegio", NombrePrivilegio);

            object resultado=  cmd.ExecuteScalar();
          
            return (int)resultado;

        }
        #endregion

        #region Nombre Privilegio

        public CE_Privilegios NombrePrivilegio(int IdPrivilegio)
        {
            //SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand()
            {
                CommandText = "Mostrar_Id_Privi",
                CommandType = CommandType.StoredProcedure,
                Connection = conexion.AbrirConnexion()
            };

            cmd.Parameters.AddWithValue("@privilegio", IdPrivilegio);

            object resultado = cmd.ExecuteScalar();
            privilegios.NombrePrivilegio = (string)resultado;
            return privilegios;

        }
        #endregion

        #region Mostrar Privilegios
         
        public SqlDataReader MostrarPrivilegios()
        {
            SqlCommand cmd = new SqlCommand()
            {
                CommandText= "SP_P_MostrarPrivi",
                CommandType= CommandType.StoredProcedure,
                Connection=conexion.AbrirConnexion()
            };

            SqlDataReader resultado = cmd.ExecuteReader();
            return resultado;   
        } 

        #endregion
    }
}
