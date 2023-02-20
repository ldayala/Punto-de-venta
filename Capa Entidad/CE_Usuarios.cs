using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class CE_Usuarios
    {
        private int _idUsuario;
        private string _nombres;
        private string _apellidos;
        private string _dni;
        private string _email;
        private int telefono;
        private DateTime _fechaNacimiento;
        private int privilegio;
        private byte[] _imagen;
        private string _usuario;
        private string _contrasenia;
        private string _patron;

        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public string Nombres { get => _nombres; set => _nombres = value; }
        public string Apellidos { get => _apellidos; set => _apellidos = value; }
        public string Dni { get => _dni; set => _dni = value; }
        public string Email { get => _email; set => _email = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public DateTime FechaNacimiento { get => _fechaNacimiento; set => _fechaNacimiento = value; }
        public int Privilegio { get => privilegio; set => privilegio = value; }
        public byte[] Imagen { get => _imagen; set => _imagen = value; }
        public string Usuario { get => _usuario; set => _usuario = value; }
        public string Contrasenia { get => _contrasenia; set => _contrasenia = value; }
        public string Patron { get => _patron; set => _patron = value; }
    }
}
