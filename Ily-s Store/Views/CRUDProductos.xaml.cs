using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ily_s_Store.Views
{
    /// <summary>
    /// Lógica de interacción para CRUDProductos.xaml
    /// </summary>
    public partial class CRUDProductos : Page
    {
        readonly CN_Grupo cN_Grupo=new CN_Grupo();
        public CRUDProductos()
        {
            InitializeComponent();
            CargarGrupo();
        }

        private void CargarGrupo()
        {
            /*  foreach (var item in cN_Grupo.GetGrupos())
              {
                  cbGrupo.Item = item;
              }*/
            cbGrupo.ItemsSource = cN_Grupo.GetGrupos();
        }
        private void BtnRegresa_Click(object sender, RoutedEventArgs e)
        {
            Content = new Productos();
        }

        private void BtnCrear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CambiarImagen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
