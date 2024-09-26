using FinalAPI2024_HTTPS.Models;
using FinalAPI2024_HTTPS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FinalAPI2024_HTTPS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly AplicacionRecorridos2024Context _context;
        private readonly ComentarioService _comentarioService;
        public ComentarioController(AplicacionRecorridos2024Context context,ComentarioService comentarioService)
        {
        
            _context = context;
            _comentarioService = comentarioService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult ObtenerListaComentarios()
        {
            return Ok(_comentarioService.ListarComentarios());
        }

        [Authorize]
        [HttpPost("registrarcomentario")]
        public async Task<IActionResult> GuardarCOMENTARIO([FromBody] Comentario com)
        {
            if (com == null)
            {
                return BadRequest(new { Message = "Ingrese un comentario" });
            }
            else
            {
                var comen = new Comentario();
                comen.Comentario1 = com.Comentario1;
                comen.Fecha = DateTime.Now;
                comen.IdUsu = com.IdUsu;
                await _context.Comentarios.AddAsync(comen);//guarda el nuevo objeto en la base
                await _context.SaveChangesAsync();//salva los cambios hechos
                return Ok(new { Message = "Comentario Guardado!" });
            }

        }
    }
}
