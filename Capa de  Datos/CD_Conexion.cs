using Microsoft.SqlServer.Server;
using System.Data;
using System.Data.SqlClient;

namespace Capa_de__Datos
{
    
    public class CD_Conexion
    {
       // public string cadena = "Data Source=bardos;Initial Catalog=IlyStore;Integrated Security=True";
       // private string cadena1 = "Data Source=LAPTOP-VF51BTIM;Initial Catalog=ilystore;Integrated Security=True";
        private readonly SqlConnection con = new SqlConnection("Data Source=bardos;Initial Catalog=IlyStore;Integrated Security=True");
        

    public SqlConnection AbrirConnexion()
        {
            if (con.State==ConnectionState.Closed)
            {
                con.Open();
               
            }
            return con;
        }

        public  SqlConnection CerrarConexion()
        {
            if (con.State==ConnectionState.Open)
            {
                con.Close();
            }
            return con;
        }
         
    }

}
