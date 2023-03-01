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
    public class CN_Usuarios
    {
        private readonly CD_Usuarios objDatos=new CD_Usuarios();

        #region Consultar
        public CE_Usuarios Consultar(int IdUsuario)
        {
            return objDatos.Consulta(IdUsuario);
        }
        #endregion

        #region Insertar
        public void Insertar(CE_Usuarios Usuario)
        {
             objDatos.CD_Insertar(Usuario);
        }
        #endregion

        #region Eliminar
        public void Eliminar(CE_Usuarios Usuario) {
            objDatos.Eliminar(Usuario);
        }
        #endregion

        #region Actualizar Usuario
         public void ActualizarUsuario(CE_Usuarios Usuario)
        {
            objDatos.ActualizarUsuario(Usuario);
        }
        #endregion

        #region Actualizar Pass
        public void ActualizarPass(CE_Usuarios Usuario) {objDatos.ActualizarPass(Usuario);}
        #endregion

        #region ActualizarImagen
        public void ActualizarImagen(CE_Usuarios Usuario) {objDatos.ActualizarImagen(Usuario);}
        #endregion

       #region Buscar Usuarios
        public DataTable Buscador(string buscador)
        {
            return objDatos.Buscador(buscador);
        }
        #endregion
    }
}
