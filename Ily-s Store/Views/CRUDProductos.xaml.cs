using Capa_de__Datos;
using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
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
        readonly CE_Productos cE_Productos=new CE_Productos();
        readonly CN_Productos cN_Productos=new CN_Productos();
        private byte[] data;
        private bool imagenSubida=false;
        private int _idUsuario;

        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }

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
            if(ValidarCampos()){
                cE_Productos.Nombre = tbNombre.Text;
                cE_Productos.Codigo = tbCodigo.Text;
                cE_Productos.Descripcion = tbDescripcion.Text;
                cE_Productos.Cant = Convert.ToInt32(tbcantidad.Text);
                cE_Productos.Grupo = cN_Grupo.GetIdGrupo(cbGrupo.Text);
                cE_Productos.UnidadMedida = tbUnidadMe.Text;
                cE_Productos.Activa = (bool)checkBActiva.IsChecked;
                cE_Productos.Precio = Convert.ToDecimal(tbPrecio.Text);
                cE_Productos.Imagen = data;
                cN_Productos.Insertar(cE_Productos);
               

                Content = new Productos();
            }
            else
            {
                System.Windows.MessageBox.Show("No debe dejar campos vacios");
            }
           
        }

        public bool ValidarCampos()
        {
            if (tbcantidad.Text == "" || tbCodigo.Text == "" || tbDescripcion.Text == "" || tbcantidad.Text == "" || cbGrupo.SelectedIndex == -1 || tbUnidadMe.Text == "" || tbPrecio.Text == ""||imagenSubida==false)
                return false;

            return true;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
        private void CambiarImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog img = CargarImagen();


            if (imagen.Source != null)
            {
                ConvertirImagenABit(img);
            }
        }
       


        private OpenFileDialog CargarImagen()
        {

            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //caragamos la imagen seleccionada en la etiqueta image
                ImageSourceConverter img = new ImageSourceConverter();
                imagen.SetValue(Image.SourceProperty, img.ConvertFromString(dialog.FileName.ToString()));
                return dialog;
            }
            return null;


        }

        public void ConvertirImagenABit(OpenFileDialog ofd)
        {
            try
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                data = new byte[fs.Length];
                fs.Read(data, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                imagenSubida = true;
            }
            catch (Exception)
            {

                imagenSubida = false;
            }

        }

        public BitmapImage ConvertirCadenaBitAImagen(DataSet ds)
        {

            byte[] data = (byte[])ds.Tables[0].Rows[0][0];
            MemoryStream ms = new MemoryStream();
            ms.Write(data, 0, data.Length);
            ms.Position = 0;
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            MemoryStream ms2 = new MemoryStream();
            img.Save(ms2, System.Drawing.Imaging.ImageFormat.Bmp);
            ms2.Seek(0, SeekOrigin.Begin);
            bi.StreamSource = ms2;
            bi.EndInit();

            return bi;

        }

       
    }
}
