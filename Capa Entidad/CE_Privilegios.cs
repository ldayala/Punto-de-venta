using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class CE_Privilegios
    {
        private int _idPrivilegio;
        private string _nombrePrivilegio;

        public int IdPrivilegio { get => _idPrivilegio; set => _idPrivilegio = value; }
        public string NombrePrivilegio { get => _nombrePrivilegio; set => _nombrePrivilegio = value; }
    }
}
