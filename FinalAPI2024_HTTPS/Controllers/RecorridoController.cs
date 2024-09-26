using FinalAPI2024_HTTPS.Models;
using FinalAPI2024_HTTPS.Services;

using FinalAPI2024_HTTPS.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FinalAPI2024_HTTPS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecorridoController : ControllerBase
    {
        private readonly RecorridoService _recorridoService;
        private readonly AplicacionRecorridos2024Context _context;

        public RecorridoController(RecorridoService recorridoService, AplicacionRecorridos2024Context context)
        {
            _recorridoService = recorridoService;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult ObtenerListaRecorridos()
        {
            return Ok(_recorridoService.ListarRecorridos());
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult ObtenerListaRecorridosId(int id)
        {
            return Ok(_recorridoService.ListarRecorridosId(id));
        }

        [Authorize]
        [HttpGet]
        [Route("obtenerrecorridousuario/{id}")]
        public IActionResult ObtenerListaRecorridosUsuario(int id)
        {
            return Ok(_recorridoService.ListarRecorridosUsuario(id));
        }

        [Authorize]
        [HttpPost]
        [Route("guardarrecorrido")]
        public IActionResult GuardarRecorrido(Recorrido r)
        {
            r = _recorridoService.GuardarRecorrido(r);
            if(r!= null)
            {
                return Ok(r);
            }
            else
            {
                return BadRequest(new { Message = "No se pudo guardar el recorrido" });
            }
        }

        [Authorize]
        [HttpDelete("{idRecorrido}")]
        public IActionResult EliminarRecorrido(int idRecorrido)
        {
            bool result = _recorridoService.EliminarRecorrido(idRecorrido);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new { Message = "No se pudo borrar el recorrido" });
            }
        }
        
        /*
        //metodo del hdeleon

        [HttpPost]
        [Route("guardarrecorrido")]
        public IActionResult AgregarRecorrido(RecorridoRequest recorridoRequest)
        {
            try
            {
                var recorrido= new Recorrido();

                recorrido.NumRec = recorridoRequest.numRec;
                recorrido.NombreRec = recorridoRequest.nombreRec;
                recorrido.FechaRec = recorridoRequest.fechaRec;
                recorrido.HoraRec = recorridoRequest.horaRec;
                recorrido.PrecioRec = recorridoRequest.precioRec;
                recorrido.IdBus = recorridoRequest.idBus;
                recorrido.Descripcion =recorridoRequest.descripcion;
                recorrido.IdUsu = recorridoRequest.idUsu;

                _context.Recorridos.Add(recorrido);
                _context.SaveChanges();

                foreach (var recorridoDetalle in recorridoRequest.Recorridodetalles)
                {
                    var rec= new Models.Recorridodetalle();
                    rec.Longitud=recorridoDetalle.Longitud;
                    rec.Latitud = recorridoDetalle.Latitud;
                    rec.IdRec = recorrido.IdRec;

                    _context.Recorridodetalles.Add(rec);
                    

                }
                _context.SaveChanges();
                
            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message);
            }
            return Ok(new { Message = "Se agregó el recorrido!" });
        }*/
    }
}
