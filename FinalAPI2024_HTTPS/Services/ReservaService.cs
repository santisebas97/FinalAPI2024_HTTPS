using FinalAPI2024_HTTPS.Models;

namespace FinalAPI2024_HTTPS.Services
{
    public class ReservaService
    {
        private readonly AplicacionRecorridos2024Context _context;

        public ReservaService(AplicacionRecorridos2024Context context)
        {
            _context = context;
        }

        public List<Reserva> listarReservasConductor(int id)
        {
            return _context.Reservas.Where(x=>x.IdConductor==id).ToList();
        }

        public bool eliminarReserva(int id)
        {
            try
            {
                var reserva= _context.Reservas.Where(x=>x.IdRes==id).FirstOrDefault();
                _context.Remove(reserva);
                _context.SaveChanges();
                return true;
            }catch (Exception)
            {
                return false;
            }
        }



    }
}
