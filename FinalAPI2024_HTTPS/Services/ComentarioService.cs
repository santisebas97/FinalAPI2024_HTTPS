using FinalAPI2024_HTTPS.Models;

namespace FinalAPI2024_HTTPS.Services
{
    public class ComentarioService
    {
        private readonly AplicacionRecorridos2024Context _context;
        public ComentarioService(AplicacionRecorridos2024Context context)
        {
            _context = context;
        }

        public List<Comentario> ListarComentarios()
        {
            return _context.Comentarios.ToList();
        }
    }
}
