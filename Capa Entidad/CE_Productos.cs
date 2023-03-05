using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class CE_Productos
    {
        private int _idProducto;
        private string _codigo;
        private string _nombre;
        private int _cant;
        private decimal _precio;
        private string _descripcion;
        private bool _estado;
        private byte[] _imagen;
        private int _grupo;
        public int IdProducto { get => _idProducto; set => _idProducto = value; }
        public string Codigo { get => _codigo; set => _codigo = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public int Cant { get => _cant; set => _cant = value; }
        public decimal Precio { get => _precio; set => _precio = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public bool Estado { get => _estado; set => _estado = value; }
        public byte[] Imagen { get => _imagen; set => _imagen = value; }
        public int Grupo { get => _grupo; set => _grupo = value; }
       
    }
}
