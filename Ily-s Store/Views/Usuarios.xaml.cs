using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace Ily_s_Store.Views
{
    /// <summary>
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class Usuarios : UserControl
    {
        public Usuarios()
        {
            InitializeComponent();
            CargarDatos();
        }
        SqlConnection conn=new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionDBIlyStore"].ConnectionString);
       void CargarDatos()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select idUsuario, DNI,apellidos,nombres,telefono,email,Privilegios.nombrePrivilegio from Usuarios inner join Privilegios on Usuarios.privilegio= Privilegios.idPrivilegio order by idUsuario ASC", conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt=new DataTable();
            adapter.Fill(dt);
            GridDatos.ItemsSource=dt.DefaultView;
            if (conn.State==ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void btnCrearUsuario_Click(object sender, RoutedEventArgs e)
        {
            CRUDUsuarios ventana = new CRUDUsuarios(); //creo un objeto de la ventana qe quiero mostrar
            FrameUsuarios.Content= ventana;  // el frame recibe como su contenido la ventana
            ventana.BtnCrear.Visibility=Visibility.Visible;
            Contenido.Visibility=Visibility.Hidden;
        }

        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter; //para obtener eñl id del elemento selccionado en el formulario
            CRUDUsuarios ventana = new CRUDUsuarios
            {
                IdUsuario = id
            };
            ventana.Consultar();
          
            FrameUsuarios.Content = ventana;
            Contenido.Visibility = Visibility.Hidden;
            ventana.Titulo.Text = "Consultar Usuario";
            ventana.tbApellidos.IsEnabled=false;
            ventana.tbDNI.IsEnabled = false;
            ventana.tbEmail.IsEnabled = false;
            ventana.tbFechaNacimiento.IsEnabled = false;
            ventana.tbNombres.IsEnabled = false;
            ventana.tbPassword.IsEnabled = false;
            ventana.tbTelefono.IsEnabled = false;
            ventana.tbUsuario.IsEnabled = false;
            ventana.btnSubir.IsEnabled = false;
            ventana.cbPrivilegios.IsEnabled = false;
         


        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            int id= (int)((Button)sender).CommandParameter;

            CRUDUsuarios ventana = new CRUDUsuarios
            {
                IdUsuario = id
            };
            ventana.Consultar();
            ventana.Titulo.Text = "Actualizar Usuario";
            FrameUsuarios.Content= ventana;
            ventana.BtnUpdate.Visibility= Visibility.Visible;
            Contenido.Visibility = Visibility.Hidden;


        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
           
            conn.Open();
            SqlCommand cmd = new SqlCommand("EliminarUsuario", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("idUsuario", SqlDbType.Int).Value = id;
           // Debug.WriteLine("idUsuario: " + IdUsuario);
            cmd.ExecuteNonQuery();
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            Content = new Usuarios();

        }
    }
}
