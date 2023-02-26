using Capa_de__Datos;
using Capa_Entidad;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capa_Negocio
{
    public class CN_Privilegio
    {
        private readonly CD_Privilegios obj_D_Privilegio =new CD_Privilegios();

        #region IdPrivilegio
        public int IdPrivilegio(string nombre)
        {
            return obj_D_Privilegio.IdPrivilegio(nombre);
        }
        #endregion

        #region Mostrar Nombre de Privilegio
        public CE_Privilegios MostrarNombrePrivi(int id) { return obj_D_Privilegio.NombrePrivilegio(id); }
        #endregion

        #region Mostrar Privilegios
        public List<string> MostrarPrivilegios() {return obj_D_Privilegio.MostrarPrivilegios();}   
        #endregion

    }
}
