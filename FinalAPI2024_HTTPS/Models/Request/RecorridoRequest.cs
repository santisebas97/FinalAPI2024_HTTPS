namespace FinalAPI2024_HTTPS.Models.Request
{
    public class RecorridoRequest
    {
        public int numRec { get; set; }
        public string nombreRec { get; set; }
        public string fechaRec { get; set; }
        public string horaRec { get; set; }
        public string precioRec { get; set; }
        public int idBus { get; set; }
        public string descripcion { get; set; }

        public int idUsu { get; set; }

        public List<Recorridodetalle> Recorridodetalles { get; set; }

        public RecorridoRequest()
        {
            this.Recorridodetalles = new List<Recorridodetalle>();
        }

        public class RecorridoDetalle
        {
            public decimal longitud { get; set; }
            public decimal latitud { get; set; }
            public int idRecorrido { get; set;}
        }

    }
}
