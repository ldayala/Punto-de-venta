using Capa_Entidad;
using Microsoft.Win32;
using System;
using System.Windows.Controls;



namespace Capa_Negocio
{
    public class CN_Imagen
    {
        readonly CE_Imagen obj= new CE_Imagen();

        public OpenFileDialog CargaImagen()
        {
            return obj.CargarImagen(Object dialog, Image imagen);
        }
       
    }
}
