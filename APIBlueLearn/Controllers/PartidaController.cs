using Microsoft.AspNetCore.Mvc;
using APIBlueLearn.Model;
using APIBlueLearn.Services;
using Microsoft.AspNetCore.Cors;

namespace APIBlueLearn.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PartidaController : ControllerBase
    {
        private readonly IPartidaService _partidaService;

        public PartidaController(IPartidaService partidaService)
        {
            _partidaService = partidaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Partida>>> GetAll()
        {
            var partida = await _partidaService.GetAll();
            var partidaNoEliminados = partida.Where(a => !a.Eliminado).ToList();
            return Ok(partidaNoEliminados);
        }

        [HttpGet("{IdPartida}")]
        public async Task<ActionResult<Partida>> GetPartida(int IdPartida)
        {
            var Partida = await _partidaService.GetPartida(IdPartida);
            if (Partida == null)
            {
                return BadRequest("User not found");
            }
            if (Partida.Eliminado == true)
            {
                return BadRequest("registro no valido");

            }
            return Ok(Partida);
        }

        /*[HttpPost]
        public async Task<ActionResult<Partida>> CreatePartida([FromBody] Partida partida)
        {
            if (partida == null)
            {
                return BadRequest("El objeto es nulo");
            }
            var newPartida = await _partidaService.CreatePartida(partida.NombrePartida, partida.IdJugador, partida.IdLogro, partida.PuntajePartida);
            return Ok(newPartida);
        }*/

        [HttpPost]
        public async Task<ActionResult<Partida>> CreatePartida(string NombrePartida, int IdJugador, int IdLogro, int PuntajePartida)
        {
            var newPartida = await _partidaService.CreatePartida(NombrePartida, IdJugador, IdLogro, PuntajePartida);
            if (newPartida != null)
            {
                return Ok(newPartida);
            }
            else
            {
                return BadRequest("Error");
            }
        }

        [HttpPut("{IdPartida}")]
        public async Task<ActionResult<Partida>> UpdatePartida(int IdPartida, string NombrePartida, int IdJugador, int IdLogro, int PuntajePartida)
        {
            var PartidaToPut = await _partidaService.UpdatePartida(IdPartida, NombrePartida, IdJugador, IdLogro, PuntajePartida);
            if (PartidaToPut != null)
            {
                return Ok(PartidaToPut);
            }
            else
            {
                return BadRequest("Error");
            }
        }

        /*[HttpDelete("{IdPartida}")]
        public async Task<ActionResult<Partida>> DeletePartida(int IdPartida)
        {
            if (IdPartida <= 0)
            {
                return BadRequest("Id invalido para eliminar");
            }
            var DeletedPartida = await _partidaService.DeletePartida(IdPartida);
            return Ok(DeletedPartida);
        }*/

        [HttpDelete("Delete/{IdPartida}")]
        public async Task<ActionResult<Agricultores>> DeletePartida(int IdPartida)
        {

            var partidaToDelete = await _partidaService.DeletePartida(IdPartida);

            if (partidaToDelete != null)
            {
                return Ok(partidaToDelete);
            }
            else
            {
                return BadRequest("Error updating the database :(");
            }
        }


    }
}
