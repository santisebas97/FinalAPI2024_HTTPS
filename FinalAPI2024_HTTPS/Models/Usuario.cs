using System;
using System.Collections.Generic;

namespace FinalAPI2024_HTTPS.Models
{
    public partial class Usuario
    {
        public int IdUser { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Passwd { get; set; }
        public string Rol { get; set; }
        public string Token { get; set; }
        public string ResetPasswdTkn { get; set; }
    }
}
