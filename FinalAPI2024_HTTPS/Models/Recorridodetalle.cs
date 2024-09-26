using System;
using System.Collections.Generic;

namespace FinalAPI2024_HTTPS.Models
{
    public partial class Recorridodetalle
    {
        public int IdRecDetalle { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public int? IdRec { get; set; }

        public virtual Recorrido IdRecNavigation { get; set; }
    }
}
