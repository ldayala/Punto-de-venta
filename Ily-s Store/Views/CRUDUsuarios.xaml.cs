
using Capa_de__Datos;
using Capa_Entidad;
using Capa_Negocio;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ily_s_Store.Views
{
    /// <summary>
    /// Lógica de interacción para CRUDUsuarios.xaml
    /// </summary>
    public partial class CRUDUsuarios : Page 
    {
        #region Propiedades
        readonly CN_Usuarios obj_CN_Usuarios = new CN_Usuarios();
        readonly CE_Usuarios obj_CE_Usuarios = new CE_Usuarios();
        readonly CN_Privilegio obj_CN_Privilegio = new CN_Privilegio();
        byte[] data;
        private bool imagenSubida = false;
        public int IdUsuario;
        // readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionDBIlyStore"].ConnectionString);
        private static string patron="patron pass";
        #endregion

        #region Inicial
        public CRUDUsuarios()
        {
            InitializeComponent();
            CargarCB();
        }
        #endregion

        #region Cargar Privilegio
        void CargarCB() // cargar datos en el combox
        {
          List<string> privilegios=  obj_CN_Privilegio.MostrarPrivilegios();
            foreach (string privilegio in privilegios)
            {
                cbPrivilegios.Items.Add(privilegio);
            }
        }
        #endregion
        #region Regresar
        private void BtnRegresa_Click(object sender, RoutedEventArgs e)
        {
            Content = new Usuarios();
        }
        #endregion
        #region Validar Campos
        public bool CamposLlenos()
        {
            if (tbApellidos.Text == "" || tbDNI.Text == "" || tbEmail.Text == "" || tbFechaNacimiento.Text == "" || tbNombres.Text == ""||tbUsuario.Text=="")
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
        #endregion


        #region Crear
        private void BtnCrear_Click(object sender, RoutedEventArgs e)
        {
            if (CamposLlenos()&&tbPassword.Password !=""&&imagenSubida==true)
            {
                int privilegio= obj_CN_Privilegio.IdPrivilegio(cbPrivilegios.Text);

                obj_CE_Usuarios.Nombres=tbNombres.Text;
                obj_CE_Usuarios.Apellidos = tbApellidos.Text;
                obj_CE_Usuarios.Dni = tbDNI.Text;
                obj_CE_Usuarios.Email = tbEmail.Text;
                obj_CE_Usuarios.Usuario = tbUsuario.Text;
                obj_CE_Usuarios.FechaNacimiento = Convert.ToDateTime(tbFechaNacimiento.Text);
                obj_CE_Usuarios.Contrasenia=tbPassword.Password;
                obj_CE_Usuarios.Patron = patron;
                obj_CE_Usuarios.Telefono = Convert.ToInt32(tbTelefono.Text); 
                obj_CE_Usuarios.Imagen=data;
                obj_CE_Usuarios.Privilegio=privilegio;

                obj_CN_Usuarios.Insertar(obj_CE_Usuarios);

                Content = new Usuarios();

            }           
            else
            {
                MessageBox.Show("Los campos no pueden quedar vacios!");

            }

        }

        #endregion

        #region READ

        public void Consultar()
        {
           var a =obj_CN_Usuarios.Consultar(IdUsuario);

            tbNombres.Text=a.Nombres;
            tbApellidos.Text=a.Apellidos;
            tbDNI.Text=a.Dni;
            tbEmail.Text=a.Email;
            tbFechaNacimiento.Text=a.FechaNacimiento.ToString();
            tbTelefono.Text=a.Telefono.ToString();
            tbUsuario.Text=a.Usuario.ToString();

            var b = obj_CN_Privilegio.MostrarNombrePrivi(a.Privilegio);
            cbPrivilegios.Text=b.NombrePrivilegio;

             ImageSourceConverter imgs= new ImageSourceConverter();
             imagen.Source=(ImageSource)imgs.ConvertFrom(a.Imagen);


        }
        #endregion


       #region Actualizar 

          private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CamposLlenos()==true)
	        {
                int privilegio = obj_CN_Privilegio.IdPrivilegio(cbPrivilegios.Text);

                obj_CE_Usuarios.IdUsuario=IdUsuario;
                obj_CE_Usuarios.Nombres=tbNombres.Text;
                obj_CE_Usuarios.Apellidos=tbApellidos.Text;
                obj_CE_Usuarios.Dni=tbDNI.Text;
                obj_CE_Usuarios.Email=tbEmail.Text;
                obj_CE_Usuarios.Telefono=int.Parse(tbTelefono.Text);
                obj_CE_Usuarios.Usuario=tbUsuario.Text;
                obj_CE_Usuarios.Privilegio=privilegio;
                obj_CE_Usuarios.FechaNacimiento = Convert.ToDateTime(tbFechaNacimiento.Text);

                obj_CN_Usuarios.ActualizarUsuario(obj_CE_Usuarios);
                
	        }else
	        {
                MessageBox.Show("Los campos no pueden quedar vacios");
        	}

            if (tbPassword.Password != "") { 
                obj_CE_Usuarios.IdUsuario=IdUsuario;
                obj_CE_Usuarios.Contrasenia=tbPassword.Password;
                obj_CE_Usuarios.Patron=patron;

                obj_CN_Usuarios.ActualizarPass(obj_CE_Usuarios);
                            
            }
            if (imagenSubida == true)
            { 
                obj_CE_Usuarios.IdUsuario = IdUsuario;
                obj_CE_Usuarios.Imagen=data;

                obj_CN_Usuarios.ActualizarImagen(obj_CE_Usuarios);

            }
           
             Content= new Usuarios();

        }
	#endregion

             
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
            if (dialog.ShowDialog() == true)
            {

                FileStream fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read);
                data = new byte[fs.Length];
                fs.Read(data, 0, Convert.ToInt32(fs.Length));
                fs.Close();
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
