using System;
using System.Collections.Generic;

namespace FinalAPI2024_HTTPS.Models
{
    public partial class Reserva
    {
        public int IdRes { get; set; }
        public string NombreEst { get; set; }
        public string FechaRes { get; set; }
        public string NombreRec { get; set; }
        public string HoraRec { get; set; }
        public string TelefonoEst { get; set; }
        public int? IdRec { get; set; }
        public int? IdUsu { get; set; }
        public int? IdConductor { get; set; }
    }
}
