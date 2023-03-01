using Capa_de__Datos;
using Capa_Entidad;
using Capa_Negocio;
using System.Data;
using System.Data.SqlClient;
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
            Buscar("");
            
        }

        private readonly CN_Usuarios us = new CN_Usuarios();
        private readonly CE_Usuarios cD_Usuarios = new CE_Usuarios();


        #region Crear usuario
        private void btnCrearUsuario_Click(object sender, RoutedEventArgs e)
        {
            CRUDUsuarios ventana = new CRUDUsuarios(); //creo un objeto de la ventana qe quiero mostrar
            FrameUsuarios.Content = ventana;  // el frame recibe como su contenido la ventana
            ventana.BtnCrear.Visibility = Visibility.Visible;
            Contenido.Visibility = Visibility.Hidden;
        }
        #endregion

        #region Consultar
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
            ventana.tbApellidos.IsEnabled = false;
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
        #endregion

        #region Modificar 
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;

            CRUDUsuarios ventana = new CRUDUsuarios
            {
                IdUsuario = id
            };
            ventana.Consultar();
            ventana.Titulo.Text = "Actualizar Usuario";
            FrameUsuarios.Content = ventana;
            ventana.BtnUpdate.Visibility = Visibility.Visible;
            Contenido.Visibility = Visibility.Hidden;


        }
        #endregion

        #region Eliminar
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).CommandParameter;
            cD_Usuarios.IdUsuario = id;
            us.Eliminar(cD_Usuarios);
            Content = new Usuarios();

        }
        #endregion


        #region Buscar Usuarios
        void Buscar(string buscar)
        {
            DataTable dt = us.Buscador(buscar);
            GridDatos.ItemsSource = dt.DefaultView;

        }
       
        private void Buscando(object sender, RoutedEventArgs e)
        {
            string texto= tbBuscando.Text;  
            GridDatos.ItemsSource = us.Buscador(texto).DefaultView;

        }
        #endregion

    }
}
