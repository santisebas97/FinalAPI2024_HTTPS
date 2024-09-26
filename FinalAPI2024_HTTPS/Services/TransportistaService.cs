using FinalAPI2024_HTTPS.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalAPI2024_HTTPS.Services
{
    public class TransportistaService
    {
        private readonly AplicacionRecorridos2024Context _context;

        public TransportistaService(AplicacionRecorridos2024Context context)
        {
            _context = context;
        }

        public List<Usuario>RetornarListaTransportistas()
        {
                return _context.Usuarios.Where(rol =>rol.Rol=="transportista").ToList();
            
        }
    }
}
