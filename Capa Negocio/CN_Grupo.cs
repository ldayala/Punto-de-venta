using Capa_de__Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_Grupo
    {
        readonly CD_Grupo cD_Grupo=new CD_Grupo();  

        public int GetIdGrupo(string nombre)
        {
            return cD_Grupo.IdGrupo(nombre);
        }

        public string GetNombreGrupo(int id)
        {
            return cD_Grupo.NombreGrupo(id);
        }

        public List<string> GetGrupos()
        {
            return cD_Grupo.GetGrupos();
        }
    }
}
