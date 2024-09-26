using FinalAPI2024_HTTPS.Models;
//using FinalAPI2024_HTTPS.Models.Request;
using FinalAPI2024_HTTPS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalAPI2024_HTTPS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusetaController : ControllerBase
    {
        private readonly AplicacionRecorridos2024Context _context;
        private readonly BusetaService _busetaService;

        public BusetaController(AplicacionRecorridos2024Context context, BusetaService busetaService)
        {
            _context = context;
            _busetaService = busetaService;
        }

        
        [Authorize]
        [HttpPost("registrar")]
        public async Task<IActionResult> GuardarBuseta([FromBody] Busetum buseta)
        {
            if (buseta == null)
            {
                return BadRequest(new { Message = "Ingrese una buseta" });
            }
            else
            {
                await _context.Buseta.AddAsync(buseta);//guarda el nuevo objeto en la base
                await _context.SaveChangesAsync();//salva los cambios hechos
                return Ok(new { Message = "Buseta Guardada!" });
            }
            
        }

        //obtener la lista de usuarios pero solo con usuario autenticado o sea con token
        [Authorize]//esto sirve solo para usuarios autorizados
        [HttpGet("{id}")]
        public IActionResult listarBusetaUsuario(int id)
        {
            return Ok(_busetaService.listarBusetasUsuario(id));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult eliminarBuseta(int id)
        {
            bool resul = _busetaService.eliminarBuseta(id);
            if (resul)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }



        /*
        [HttpPost("registrar")]
        public async Task<IActionResult> AnadirBuseta([FromBody] BusetaRequest buseta)
        {
            try
            {
                var user = new Usuario();
                user.IdUser=buseta.IdUser;

                foreach (var busetas in buseta.Busetas )
                {
                    var buses = new Models.Busetum();
                    buses.Placa = busetas.Placa;
                    buses.Anio = busetas.Anio;
                    buses.Capacidad = busetas.Capacidad;
                    buses.Modelo = busetas.Modelo;
                    buses.IdUsu = user.IdUser;
                    await _context.Buseta.AddAsync(buses);
                    await _context.SaveChangesAsync();
                }

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { Message ="Se agregó la buseta" });
        }*/

    }
}
