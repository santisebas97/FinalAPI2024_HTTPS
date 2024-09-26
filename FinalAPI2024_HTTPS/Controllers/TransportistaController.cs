using FinalAPI2024_HTTPS.Models;
using FinalAPI2024_HTTPS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalAPI2024_HTTPS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportistaController : ControllerBase
    {
        private readonly TransportistaService _transportistaService;

        public TransportistaController(TransportistaService transportistaService)
        {
            _transportistaService = transportistaService;
        }

        //obtener la lista de usuarios pero solo con usuario autenticado o sea con token
        //[Authorize]//esto sirve solo para usuarios autorizados
        [Authorize]
        [HttpGet]
        public IActionResult ObtenerTransportistas()
        {
            return Ok( _transportistaService.RetornarListaTransportistas());
        }
    }
}
