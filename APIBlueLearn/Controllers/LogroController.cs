using Microsoft.AspNetCore.Mvc;
using APIBlueLearn.Model;
using APIBlueLearn.Services;
using Microsoft.AspNetCore.Cors;

namespace APIBlueLearn.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LogroController : ControllerBase
    {
        private readonly ILogroService _logroService;

        public LogroController(ILogroService logroService)
        {
            _logroService = logroService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Logro>>> GetAll()
        {
            var logro = await _logroService.GetAll();
            var logroNoEliminados = logro.Where(a => !a.Eliminado).ToList();
            return Ok(logroNoEliminados);
        }

        [HttpGet("{IdLogro}")]
        public async Task<ActionResult<Logro>> GetLogro(int IdLogro)
        {
            var Logro = await _logroService.GetLogro(IdLogro);
            if (Logro == null)
            {
                return BadRequest("User not found");
            }
            if (Logro.Eliminado == true)
            {
                return BadRequest("registro no valido");

            }
            return Ok(Logro);
        }

        /*[HttpPost]
        public async Task<ActionResult<Logro>> CreateLogro([FromBody] Logro logro)
        {
            if (logro == null)
            {
                return BadRequest("El objeto es nulo");
            }
            var newLogro = await _logroService.CreateLogro(logro.Descripcion, logro.Fecha, logro.Puntos);
            return Ok(newLogro);
        }*/

        [HttpPost]
        public async Task<ActionResult<Logro>> CreateLogro(string Descripcion, int Puntos)
        {
            var newLogro = await _logroService.CreateLogro(Descripcion, Puntos);
            if (newLogro != null)
            {
                return Ok(newLogro);
            }
            else
            {
                return BadRequest("Error");
            }
        }

        [HttpPut("{IdLogro}")]
        public async Task<ActionResult<Logro>> UpdateLogro(int IdLogro, string Descripcion, int Puntos)
        {
            var LogroToPut = await _logroService.UpdateLogro(IdLogro, Descripcion, Puntos);
            if (LogroToPut != null)
            {
                return Ok(LogroToPut);
            }
            else
            {
                return BadRequest("Error");
            }
        }

        /*[HttpDelete("{IdLogro}")]
        public async Task<ActionResult<Logro>> DeleteLogro(int IdLogro)
        {
            if (IdLogro <= 0)
            {
                return BadRequest("Id invalido para eliminar");
            }
            var DeletedLogro = await _logroService.DeleteLogro(IdLogro);
            return Ok(DeletedLogro);
        }*/

        [HttpDelete("Delete/{IdLogro}")]
        public async Task<ActionResult<Agricultores>> DeleteLogro(int IdLogro)
        {

            var logroToDelete = await _logroService.DeleteLogro(IdLogro);

            if (logroToDelete != null)
            {
                return Ok(logroToDelete);
            }
            else
            {
                return BadRequest("Error updating the database :(");
            }
        }
    }
}
