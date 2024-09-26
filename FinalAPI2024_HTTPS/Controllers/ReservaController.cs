using FinalAPI2024_HTTPS.Models;
using FinalAPI2024_HTTPS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalAPI2024_HTTPS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly ReservaService _reservaService;
        private readonly AplicacionRecorridos2024Context _context;

        public ReservaController(AplicacionRecorridos2024Context context, ReservaService reservaService)
        {
            _context = context;
            _reservaService = reservaService;
        }

        [Authorize]
        [HttpPost("reserva")]
        public async Task<IActionResult> GuardarReserva([FromBody] Reserva reserva)
        {
            if (reserva == null)
            {
                return BadRequest(new { Message = "No existe reserva" });
            }
            else
            {
                await _context.Reservas.AddAsync(reserva);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Se notificó la reserva al conductor" });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult listarReserva(int id) {
            return Ok(_reservaService.listarReservasConductor(id));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult eliminarReserva(int id)
        {
            bool resul=_reservaService.eliminarReserva(id);
            if (resul)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
