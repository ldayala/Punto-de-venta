using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class CE_Grupo
    {
        private int _idGrupo;
        private string _nombre;

        public int IdGrupo { get => _idGrupo; set => _idGrupo = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
    }
}
