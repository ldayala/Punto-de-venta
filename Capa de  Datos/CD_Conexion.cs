using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de__Datos
{
    public class CD_Conexion
    {
        private readonly SqlConnection con = new SqlConnection("Data Source=LAPTOP-VF51BTIM;Initial Catalog=IlyStore;Integrated Security=True");


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
