using Capa_Entidad;
using Capa_Negocio;
using System.Data;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows;

namespace Ily_s_Store.Views
{
    /// <summary>
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : UserControl
    {
        readonly CN_Productos prod=new CN_Productos();
        readonly CE_Productos prod_CE=new CE_Productos();
        public Productos()
        {
            InitializeComponent();
            Buscar("");

        }
        #region Buscar
        private void tbBuscando(object sender, TextChangedEventArgs e)
        {
            GridDatosProducto.ItemsSource=prod.Buscar(tbxBuscando.Text).DefaultView;
        }
        #endregion

        #region CRUD
        #region Crear
        private void btnCrearProducto(object sender, System.Windows.RoutedEventArgs e)
        {
          CRUDProductos cRUDProductos=new CRUDProductos();
          FrameProductos.Content = cRUDProductos;
          Contenido.Visibility = Visibility.Hidden;
          cRUDProductos.BtnCrear.Visibility=Visibility.Visible;
        }
        #endregion

        #region Consultar
        private void btnConsultar_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        #endregion

        #region Eliminar
        private void btnEliminar_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }


        #endregion

        #region Modificar
        private void btnModificar_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        #endregion
        #endregion

        public void Buscar(string text)
        {
            //prod.Buscar(text);
            GridDatosProducto.ItemsSource = prod.Buscar(text).DefaultView;
        }

       
    }
}
