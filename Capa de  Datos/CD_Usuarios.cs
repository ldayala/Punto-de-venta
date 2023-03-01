using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
namespace Capa_de__Datos
{
    public class CD_Usuarios
    {
        private readonly CD_Conexion con=new CD_Conexion();
        private readonly Capa_Entidad.CE_Usuarios ce = new Capa_Entidad.CE_Usuarios();

        //CRUD

        #region Insertar
        public void CD_Insertar(Capa_Entidad.CE_Usuarios Usuarios)
        {
            SqlCommand comand = new SqlCommand()
            {
                Connection=con.AbrirConnexion(),
                CommandType=CommandType.StoredProcedure,
                CommandText= "SP_U_Crear"
            };
            comand.Parameters.AddWithValue("@nombres", Usuarios.Nombres);
            comand.Parameters.AddWithValue("@apellidos", Usuarios.Apellidos);
            comand.Parameters.AddWithValue("@telefono", Usuarios.Telefono);
            comand.Parameters.AddWithValue("@DNI", Usuarios.Dni);
            comand.Parameters.AddWithValue("@email", Usuarios.Email);
            comand.Parameters.Add("@fechaNacimiento",SqlDbType.Date).Value= Usuarios.FechaNacimiento;
            comand.Parameters.AddWithValue("@img", Usuarios.Imagen);
            comand.Parameters.AddWithValue("@privilegio", Usuarios.Privilegio);
            comand.Parameters.AddWithValue("@patron",Usuarios.Patron);
            comand.Parameters.AddWithValue("@contrasenia", Usuarios.Contrasenia);
            comand.ExecuteNonQuery();
            comand.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #region Consultar
        public Capa_Entidad.CE_Usuarios Consulta(int IdUsuario)
        {
            SqlDataAdapter da = new SqlDataAdapter("SP_U_SelexXId", con.AbrirConnexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@IdUsuario",SqlDbType.Int).Value = IdUsuario;
            DataSet ds =new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt;
            dt=ds.Tables[0];
            DataRow row=dt.Rows[0];
            ce.Nombres = Convert.ToString(row[1]);
            ce.Apellidos = Convert.ToString(row[2]);
            ce.Dni=Convert.ToString(row[3]);
            ce.Email= Convert.ToString(row[4]);
            ce.Telefono= Convert.ToInt32(row[5]);
            ce.FechaNacimiento = Convert.ToDateTime(row[6]);
            ce.Privilegio=Convert.ToInt32(row[7]);
            ce.Imagen = (byte[])row[8];
            ce.Usuario= Convert.ToString(row[9]);
            ce.Contrasenia = Convert.ToString(row[10]);

            return ce;
        }

        #endregion

        #region Eliminar
        public void Eliminar(Capa_Entidad.CE_Usuarios Usuario)
        {
            SqlCommand command = new SqlCommand()
            {
                Connection = con.AbrirConnexion(),
                CommandType = CommandType.StoredProcedure,
                CommandText= "EliminarUsuario"
            };
            command.Parameters.AddWithValue("@idUsuario", Usuario.IdUsuario);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            con.CerrarConexion();
        }

        #endregion

        #region Actualizar Datos
        public void ActualizarUsuario(Capa_Entidad.CE_Usuarios Usuario)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandText = "UPD_Usuario",
                CommandType = CommandType.StoredProcedure,
                Connection = con.AbrirConnexion(),
            };
            sqlCommand.Parameters.AddWithValue("@idUsuario", Usuario.IdUsuario);
            sqlCommand.Parameters.AddWithValue("@usuario", Usuario.Usuario);
            sqlCommand.Parameters.AddWithValue("@nombres", Usuario.Nombres);
            sqlCommand.Parameters.AddWithValue("@apellidos", Usuario.Apellidos);
            sqlCommand.Parameters.AddWithValue("@dni",Usuario.Dni);
            sqlCommand.Parameters.AddWithValue("@email", Usuario.Email);
            sqlCommand.Parameters.Add("@telefono", SqlDbType.BigInt).Value = Usuario.Telefono;
            sqlCommand.Parameters.Add("@fechaNacimiento", SqlDbType.Date).Value = Usuario.FechaNacimiento;
            sqlCommand.Parameters.Add("@privilegio", SqlDbType.Int).Value = Usuario.Privilegio;
            
            sqlCommand.ExecuteNonQuery();
            sqlCommand.Parameters.Clear();

            con.CerrarConexion();

        }

        #endregion

        #region Actualizar Pass
        public void ActualizarPass(Capa_Entidad.CE_Usuarios Usuario)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText= "SP_U_ActPass",
                Connection= con.AbrirConnexion()
            };
            sqlCommand.Parameters.AddWithValue("@contrasenia", Usuario.Contrasenia);
            sqlCommand.Parameters.AddWithValue("@patron", Usuario.Patron);

            sqlCommand.ExecuteNonQuery();
            sqlCommand.Parameters.Clear();
            con.CerrarConexion();
        }
       
        #endregion

        #region Actualizar Imagen

        public void ActualizarImagen(Capa_Entidad.CE_Usuarios Usuario)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandText = "UPD_Imagen",
                CommandType = CommandType.StoredProcedure,
                Connection = con.AbrirConnexion()
            };
            sqlCommand.Parameters.AddWithValue("@idUsuario",Usuario.Imagen);
            sqlCommand.Parameters.Add("@imagen",SqlDbType.VarBinary).Value= Usuario.Imagen;

            sqlCommand.ExecuteNonQuery();
            sqlCommand.Parameters.Clear();
            con.CerrarConexion();

        }

        #endregion
        
        #region Buscar Usuarios
        public DataTable Buscador(string buscar)
        {
            SqlDataAdapter da = new SqlDataAdapter("SP_U_Buscar", con.AbrirConnexion());
            da.SelectCommand.CommandType= CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@buscar", buscar);
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            DataTable dt= ds.Tables[0];
            con.CerrarConexion();
            return dt;

        }
        #endregion
    }
}
