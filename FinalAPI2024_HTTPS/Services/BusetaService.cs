using FinalAPI2024_HTTPS.Models;
//using FinalAPI2024_HTTPS.Models.Request;

namespace FinalAPI2024_HTTPS.Services
{
    public class BusetaService
    {
        private readonly AplicacionRecorridos2024Context _context;
        public BusetaService(AplicacionRecorridos2024Context context)
        {
            _context = context;
        }
        
        public List<Busetum> listarBusetasUsuario (int id)
        {
            return _context.Buseta.Where(x => x.IdUsu == id).ToList();
        }

        public bool eliminarBuseta(int id)
        {
            try
            {
                var buseta = _context.Buseta.Where(x=>x.IdBus==id).FirstOrDefault();
                _context.Remove(buseta);
                _context.SaveChanges();
                return true;
            }catch (Exception)
            {
                return false;
            }
        }
        
    }
}
