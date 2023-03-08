using Capa_de__Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_Productos
    {
        readonly CD_Productos ObjProductos= new CD_Productos();

        #region Insertar
        public void Insertar(CE_Productos obj)
        {
            ObjProductos.Insertar(obj);
        }
        #endregion

        #region Insertar Imagen
        public void InsertarImagen(CE_Productos obj)
        {
            ObjProductos.InsertarImagen(obj);
        }
        #endregion

        #region Buscar
        public DataTable Buscar(string text)
        {
         return   ObjProductos.Buscar(text);
        }
        #endregion

        #region Update
        public void Update(CE_Productos obj)
        {
            ObjProductos.Update(obj);
        }
        #endregion

        #region Eliminar
        public void Eliminar(int id)
        {
            ObjProductos.Eliminar(id);
        }
        #endregion
    }
}
