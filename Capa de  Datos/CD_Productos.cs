using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de__Datos
{
    public class CD_Productos
    {
        readonly CD_Conexion conexion=new CD_Conexion();
        //readonly CE_Productos cE_Productos=new CE_Productos();

        #region Buscar
        public DataTable Buscar(string text)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SP_Prod_Buscar", conexion.AbrirConnexion());
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@text", text);

            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet);

            DataTable dt = dataSet.Tables[0];
            return dt;

        }
        #endregion

        #region Insertar
        public void Insertar(CE_Productos obj)
        {
            SqlCommand command = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "SP_Prod_Insert",
                Connection = conexion.AbrirConnexion()
            };

            command.Parameters.AddWithValue("@codigo", obj.Codigo);
            command.Parameters.AddWithValue("@nombre", obj.Nombre);
            command.Parameters.Add("@cantidad", SqlDbType.Int).Value = obj.Cant;
            command.Parameters.Add("@precio", SqlDbType.Money).Value = obj.Precio;
            command.Parameters.AddWithValue("@descripcion", obj.Descripcion);
            command.Parameters.Add("@grupo",SqlDbType.Int).Value=obj.Grupo;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            command.Connection.Close();
        }
        #endregion

        #region Insertar Imagen
        public void InsertarImagen(CE_Productos obj)
        {
            SqlCommand command = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "SP_Prod_InsertarImagen",
                Connection = conexion.AbrirConnexion()             
            };

            command.Parameters.Add("@idProducto", SqlDbType.Int).Value = obj.IdProducto;
            command.Parameters.Add("@imagen", SqlDbType.Image).Value = obj.Imagen;
            command.ExecuteNonQuery();
        }
        #endregion

        #region Actualizar
        public void Update(CE_Productos obj)
        {
            SqlCommand command = new SqlCommand() { 
            CommandText= "SP_Prod_Updat",
            CommandType= CommandType.StoredProcedure,
            Connection=conexion.AbrirConnexion()
            };

            command.Parameters.Add("@idProducto", SqlDbType.Int).Value = obj.IdProducto;
            command.Parameters.AddWithValue("@codigo", obj.Codigo);
            command.Parameters.AddWithValue("@nombre", obj.Nombre);
            command.Parameters.Add("@cantidad", SqlDbType.Int).Value = obj.Cant;
            command.Parameters.Add("@precio", SqlDbType.Money).Value = obj.Precio;
            command.Parameters.AddWithValue("@descripcion", obj.Descripcion);

            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.CerrarConexion();
        }


        #endregion

        #region Eliminar

        public void Eliminar(int id)
        {
            SqlCommand command = new SqlCommand()
            {
                CommandText = "SP_Prod_Dele",
                CommandType = CommandType.StoredProcedure,
                Connection = conexion.AbrirConnexion()
            };
            command.Parameters.Add("@idProducto",SqlDbType.Int).Value=id;
            command.ExecuteNonQuery();
        }
        #endregion


    }
}
