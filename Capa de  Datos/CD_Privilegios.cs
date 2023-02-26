using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Capa_de__Datos
{
    public class CD_Privilegios
    {
        readonly CD_Conexion conexion=new CD_Conexion();
        private CE_Privilegios privilegios=new CE_Privilegios();

        #region Id Privilegio

        public int IdPrivilegio(string NombrePrivilegio)
        {
            SqlCommand cmd = new SqlCommand()
            {
                CommandText = "Mostrar_Id_Privi",
                CommandType = CommandType.StoredProcedure,
                Connection= conexion.AbrirConnexion()
            };

            cmd.Parameters.AddWithValue("@nombrePrivi", NombrePrivilegio);

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
                CommandText = "Mostrar_Privi",
                CommandType = CommandType.StoredProcedure,
                Connection = conexion.AbrirConnexion()
            };

            cmd.Parameters.AddWithValue("@privilegio", IdPrivilegio);

            object resultado = cmd.ExecuteScalar();
            Debug.WriteLine("resultado"+resultado);
            privilegios.NombrePrivilegio = (string)resultado;
            return privilegios;

        }
        #endregion

        #region Mostrar Privilegios
         
        public List<String> MostrarPrivilegios()
        {
            SqlCommand cmd = new SqlCommand()
            {
                CommandText= "SP_P_MostrarPrivi",
                CommandType= CommandType.StoredProcedure,
                Connection=conexion.AbrirConnexion()
            };

            SqlDataReader resultado = cmd.ExecuteReader();
            

            List<String> list = new List<String>();
            while (resultado.Read())
            {
                list.Add((string)resultado["nombrePrivilegio"])
;            }
            conexion.CerrarConexion();
            return list;

         
        } 

        #endregion
    }
}
