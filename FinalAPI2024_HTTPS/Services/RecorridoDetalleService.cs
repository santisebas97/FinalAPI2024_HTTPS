using FinalAPI2024_HTTPS.Models;

namespace FinalAPI2024_HTTPS.Services
{
    public class RecorridoDetalleService
    {
        private readonly AplicacionRecorridos2024Context _context;
        public RecorridoDetalleService(AplicacionRecorridos2024Context context)
        {
            _context = context;
        }

        public bool GuardarRecorridoDetalle(Recorridodetalle rd)
        {
            try
            {
                _context.Recorridodetalles.Add(rd);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Recorridodetalle> GetRutaCoordenadas(int id)
        {
            return _context.Recorridodetalles.Where(x=>x.IdRec==id).ToList();
        }

        public bool EliminarDetalleRecorrido(int rd)
        {
            try
            {
                var rdDB = _context.Recorridodetalles.Where(x => x.IdRec == rd).FirstOrDefault();
                _context.Recorridodetalles.Remove(rdDB);    
                _context.SaveChanges();
                return true;

            }catch(Exception)
            {
                return false;
            }
        }
    }
}
