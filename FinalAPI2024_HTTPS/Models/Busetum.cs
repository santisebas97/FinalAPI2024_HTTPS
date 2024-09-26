using System;
using System.Collections.Generic;

namespace FinalAPI2024_HTTPS.Models
{
    public partial class Busetum
    {
        public int IdBus { get; set; }
        public string Placa { get; set; }
        public int? Capacidad { get; set; }
        public string Modelo { get; set; }
        public int? Anio { get; set; }
        public byte[] Imagen { get; set; }
        public int IdUsu { get; set; }
    }
}
