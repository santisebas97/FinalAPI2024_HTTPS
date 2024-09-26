using System;
using System.Collections.Generic;

namespace FinalAPI2024_HTTPS.Models
{
    public partial class Recorrido
    {
        public Recorrido()
        {
            Recorridodetalles = new HashSet<Recorridodetalle>();
        }

        public int IdRec { get; set; }
        public int NumRec { get; set; }
        public string NombreRec { get; set; }
        public string FechaRec { get; set; }
        public string HoraRec { get; set; }
        public string PrecioRec { get; set; }
        public int IdBus { get; set; }
        public string Descripcion { get; set; }
        public int IdUsu { get; set; }

        public virtual ICollection<Recorridodetalle> Recorridodetalles { get; set; }
    }
}
