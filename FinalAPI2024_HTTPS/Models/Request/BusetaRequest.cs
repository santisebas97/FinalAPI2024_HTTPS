namespace FinalAPI2024_HTTPS.Models.Request
{
    public class BusetaRequest
    {
        public int IdUser { get; set; }
        public List<Buseta> Busetas { get; set; }

        public BusetaRequest() { 

        this.Busetas = new List<Buseta>();
        }
    }

    public class Buseta
    {
        
        public string Placa { get; set; }
        public int Capacidad { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public int IdUser { get; set; }
    }
}
