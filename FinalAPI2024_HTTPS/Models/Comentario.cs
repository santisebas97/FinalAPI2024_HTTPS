using System;
using System.Collections.Generic;

namespace FinalAPI2024_HTTPS.Models
{
    public partial class Comentario
    {
        public int IdCom { get; set; }
        public string Comentario1 { get; set; }
        public DateTime? Fecha { get; set; }
        public int IdUsu { get; set; }
    }
}
