using FinalAPI2024_HTTPS.Models;
using FinalAPI2024_HTTPS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalAPI2024_HTTPS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecorridodetalleController : ControllerBase
    {
        private readonly RecorridoDetalleService _recorridodetalleService;

        public RecorridodetalleController(RecorridoDetalleService recorridoDetalleService)
        {
            _recorridodetalleService = recorridoDetalleService;
        }

        [Authorize]
        [HttpPost]
        [Route("guardardetalle")]
        public IActionResult GuardarDetalle([FromBody] Recorridodetalle rd)
        {
            bool resul = _recorridodetalleService.GuardarRecorridoDetalle(rd);
            if (resul)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult listarCoordenadasRecorrido(int id)
        {
            try
            {
                return Ok(_recorridodetalleService.GetRutaCoordenadas(id));
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "No existen coordenadas para esa ruta" });
            }
        }

        [Authorize]
        [HttpDelete("{rd}")]
        public IActionResult EliminarDetalleRecorrido(int rd)
        {
            bool resul = _recorridodetalleService.EliminarDetalleRecorrido(rd);
            if (resul)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { Message = "No se pudo borrar las coordenadas" });
            }
        }
    }
}
