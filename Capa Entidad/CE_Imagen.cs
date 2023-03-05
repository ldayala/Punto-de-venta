using Microsoft.Win32;
using System.IO;
using System.Windows.Media;
using System;
using System.Windows.Controls;

namespace Capa_Entidad
{
    public class CE_Imagen
    {
        private byte[] _data;
        private bool _imagenSubida;

        public CE_Imagen()
        {
            ImagenSubida = false;
            
        }

        public byte[] Data { get => _data; set => _data = value; }
        public bool ImagenSubida { get => _imagenSubida; set => _imagenSubida = value; }

        public OpenFileDialog CargarImagen(OpenFileDialog dialog, Image imagen)
        {

            if (dialog.ShowDialog() == true)
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
                Data = new byte[fs.Length];
                fs.Read(Data, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                ImagenSubida = true;
            }
            catch (Exception)
            {

                ImagenSubida = false;
            }

        }

    }
}
