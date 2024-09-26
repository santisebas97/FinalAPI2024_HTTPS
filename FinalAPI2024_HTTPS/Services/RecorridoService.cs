using FinalAPI2024_HTTPS.Models;

namespace FinalAPI2024_HTTPS.Services
{
    public class RecorridoService
    {
        private readonly AplicacionRecorridos2024Context _context;
        public RecorridoService(AplicacionRecorridos2024Context context)
        {
            _context = context;
        }

        public List<Recorrido> ListarRecorridos() { 
            return _context.Recorridos.ToList();
        }

        public List<Recorrido> ListarRecorridosId(int id)
        {
            return _context.Recorridos.Where(x=>x.IdRec==id).ToList();
        }

        public List<Recorrido> ListarRecorridosUsuario(int id)
        {
            return _context.Recorridos.Where(x => x.IdUsu == id).ToList();
        }

        public Recorrido GuardarRecorrido(Recorrido r)
        {
            try
            {
                var recorridoGurdado = _context.Recorridos.Add(r);
                var res = recorridoGurdado.GetDatabaseValues();

                var resp = _context.SaveChanges();
                return r;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool EliminarRecorrido(int id)
        {
            try
            {
                var recorrido=_context.Recorridos.Where(x=>x.IdRec==id).FirstOrDefault();
                _context.Recorridos.Remove(recorrido);
                _context.SaveChanges(false);
                return true;
            }catch (Exception) {
                return false;
            }
        }
    }
}
