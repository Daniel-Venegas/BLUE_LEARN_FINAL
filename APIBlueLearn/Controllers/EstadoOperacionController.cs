using Microsoft.AspNetCore.Mvc;
using APIBlueLearn.Model;
using APIBlueLearn.Services;
using Microsoft.AspNetCore.Cors;

namespace APIBlueLearn.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EstadoOperacionController : ControllerBase
    {
        private readonly IEstadoOperacionService _estadoOperacionService;

        public EstadoOperacionController(IEstadoOperacionService estadoOperacionService)
        {
            _estadoOperacionService = estadoOperacionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EstadoOperacion>>> GetAll()
        {
            var estadoOperacion = await _estadoOperacionService.GetAll();
            var estadoOperacionNoEliminados = estadoOperacion.Where(a => !a.Eliminado).ToList();
            return Ok(estadoOperacionNoEliminados);
        }

        [HttpGet("{IdEstadoOperacion}")]
        public async Task<ActionResult<EstadoOperacion>> GetEstadoOperacion(int IdEstadoOperacion)
        {
            var EstadoOperacion = await _estadoOperacionService.GetEstadoOperacion(IdEstadoOperacion);
            if (EstadoOperacion == null)
            {
                return BadRequest("User not found");
            }
            if (EstadoOperacion.Eliminado == true)
            {
                return BadRequest("registro no valido");

            }
            return Ok(EstadoOperacion);
        }

        /*[HttpPost]
        public async Task<ActionResult<EstadoOperacion>> CreateEstadoOperacion([FromBody] EstadoOperacion estadoOperacion)
        {
            if (estadoOperacion == null)
            {
                return BadRequest("El objeto es nulo");

            }

            var newEstadoOperacion = await _estadoOperacionService.CreateEstadoOperacion(estadoOperacion.Descripcion);
            return Ok(newEstadoOperacion);
        }*/


        [HttpPost]
        public async Task<ActionResult<EstadoOperacion>> CreateEstadoOperacion(string Descripcion)
        {
            var newEstadoOperacion = await _estadoOperacionService.CreateEstadoOperacion(Descripcion);
            if (newEstadoOperacion != null)
            {
                return Ok(newEstadoOperacion);
            }
            else
            {
                return BadRequest("Error");
            }
        }

        [HttpPut("{IdEstadoOperacion}")]
        public async Task<ActionResult<EstadoOperacion>> UpdateEstadoOperacion(int IdEstadoOperacion, string Descripcion)
        {
            var EstadoOperacionToPut = await _estadoOperacionService.UpdateEstadoOperacion(IdEstadoOperacion, Descripcion);
            if (EstadoOperacionToPut != null)
            {
                return Ok(EstadoOperacionToPut);
            }
            else
            {
                return BadRequest("Error");
            }
        }

        /*[HttpDelete("{IdEstadoOperacion}")]
        public async Task<ActionResult<EstadoOperacion>> DeleteEatadoOperacion(int IdEstadoOperacion)
        {
            if (IdEstadoOperacion <= 0)
            {
                return BadRequest("Id invalido para eliminar");
            }
            var DeletedEstadoOperacion = await _estadoOperacionService.DeleteEstadoOperacion(IdEstadoOperacion);
            return Ok(DeletedEstadoOperacion);
        }*/
        [HttpDelete("Delete/{IdEstadoOperacion}")]
        public async Task<ActionResult<Agricultores>> DeleteEstadoOperacion(int IdEstadoOperacion)
        {

            var estadoOperacionToDelete = await _estadoOperacionService.DeleteEstadoOperacion(IdEstadoOperacion);

            if (estadoOperacionToDelete != null)
            {
                return Ok(estadoOperacionToDelete);
            }
            else
            {
                return BadRequest("Error updating the database :(");
            }
        }

    }
}
