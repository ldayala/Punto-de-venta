
using Capa_Entidad;
using Capa_Negocio;
using Microsoft.Win32;
using System;
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
        byte[] data;
        private bool imagenSubida = false;
        public int IdUsuario;
        readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionDBIlyStore"].ConnectionString);
        #endregion

        #region Inicial
        public CRUDUsuarios()
        {
            InitializeComponent();
            CargarCB();
        }
        #endregion
        #region Cargar Privilegio
        void CargarCB() // caragr datos en el combox
        {
            con.Open();  //abro la conexion a la db
            SqlCommand cmd = new SqlCommand("select nombrePrivilegio from Privilegios", con); //creo el comando a ejecutar
            SqlDataReader dr = cmd.ExecuteReader();  //creao el reader para ejecutar el comando
            while (dr.Read()) //mientra haya un registro se va a estar ejecuatndo el while
            {
                cbPrivilegios.Items.Add(dr["nombrePrivilegio"].ToString());
            }

            con.Close();
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
            if (CamposLlenos()&&tbPassword.Password !="" )
            {
                obj_CE_Usuarios.Nombres=tbNombres.Text;
                obj_CE_Usuarios.Apellidos = tbApellidos.Text;
                obj_CE_Usuarios.Dni = tbDNI.Text;
                obj_CE_Usuarios.Email = tbEmail.Text;
                obj_CE_Usuarios.Usuario = tbUsuario.Text;
                obj_CE_Usuarios.FechaNacimiento = Convert.ToDateTime(tbFechaNacimiento.Text);
                obj_CE_Usuarios.Contrasenia=tbPassword.Password;
                obj_CE_Usuarios.Patron = "Patron pass";
                obj_CE_Usuarios.Telefono = Convert.ToInt32(tbTelefono.Text);

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
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Usuarios inner join Privilegios ON Privilegios.idPrivilegio=Usuarios.privilegio where Usuarios.idUsuario=" + IdUsuario, con);
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            reader.Read();
            this.tbNombres.Text = reader["nombres"].ToString();
            this.tbPassword.Password = reader["contrasenia"].ToString();
            this.tbUsuario.Text = reader["usuario"].ToString();
            this.tbTelefono.Text = reader["telefono"].ToString();
            this.tbApellidos.Text = reader["apellidos"].ToString();
            this.tbDNI.Text = reader["DNI"].ToString();
            this.tbEmail.Text = reader["email"].ToString();
            this.tbFechaNacimiento.Text = reader["fechaNaciemiento"].ToString();
            this.cbPrivilegios.SelectedValue = reader["nombrePrivilegio"];
            
            reader.Close();

            //imagen
            DataSet ds = new DataSet();
            SqlDataAdapter sgda = new SqlDataAdapter("select img from usuarios where idUsuario=" + IdUsuario + "", con);
            sgda.Fill(ds);

            BitmapImage bi = ConvertirCadenaBitAImagen(ds);

            imagen.Source = bi;
            con.Close();
        }
        #endregion


       

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand privi = new SqlCommand("Mostrar_Privi", con) { CommandType = CommandType.StoredProcedure };
            privi.Parameters.Add("@privilegio", SqlDbType.VarChar).Value = cbPrivilegios.SelectedValue;
            Debug.WriteLine("privi selected:" + cbPrivilegios.SelectedValue);
            object valor = privi.ExecuteScalar();

            int privilegio = (int)valor;
            SqlCommand sqlCommand = new SqlCommand("UPD_Usuario", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlCommand.Parameters.Add("@idUsuario", SqlDbType.VarChar).Value = IdUsuario;
            sqlCommand.Parameters.Add("@usuario", SqlDbType.VarChar).Value = tbUsuario.Text;
            sqlCommand.Parameters.Add("@nombres", SqlDbType.VarChar).Value = tbNombres.Text;
            sqlCommand.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = tbApellidos.Text;
            sqlCommand.Parameters.Add("@dni", SqlDbType.VarChar).Value = tbDNI.Text;
            sqlCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = tbEmail.Text;
            sqlCommand.Parameters.Add("@telefono", SqlDbType.BigInt).Value = System.Convert.ToInt64(tbTelefono.Text);
            sqlCommand.Parameters.Add("@fechaNacimiento", SqlDbType.Date).Value = System.Convert.ToDateTime(tbFechaNacimiento.Text);
            sqlCommand.Parameters.Add("@privilegio", SqlDbType.Int).Value = privilegio;
            sqlCommand.Parameters.Add("@contrasenia", SqlDbType.VarChar).Value = "siiiiiiii";
            sqlCommand.Parameters.Add("@patron", SqlDbType.VarChar).Value = "hola_siiiii";
            sqlCommand.ExecuteNonQuery();
            if (imagenSubida == true)
            {

                ActualizaImagenEnDB("UPD_Imagen", data, IdUsuario);
            }


            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            Content = new Usuarios();


        }

        private void ActualizaImagenEnDB(string storePro, byte[] ima, int id)
        {
            SqlCommand com = new SqlCommand(storePro, con)
            {
                CommandType = CommandType.StoredProcedure
            };
            com.Parameters.Add("@imagen", SqlDbType.VarBinary).Value = ima;
            com.Parameters.Add("idUsuario", SqlDbType.Int).Value = id;
            com.ExecuteNonQuery();
           
            
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
