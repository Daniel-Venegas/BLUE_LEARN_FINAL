using Microsoft.AspNetCore.Mvc;
using APIBlueLearn.Model;
using APIBlueLearn.Services;
using Microsoft.AspNetCore.Cors;

namespace APIBlueLearn.Controllers
{



    [ApiController]
    [Route("api/[controller]")]
    public class DescripcionMonitoreoController : ControllerBase
    {
        private readonly IDescripcionMonitoreoService _descripcionMonitoreoService;

        public DescripcionMonitoreoController(IDescripcionMonitoreoService descripcionMonitoreoService)
        {
            _descripcionMonitoreoService = descripcionMonitoreoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DescripcionMonitoreo>>> GetAll()
        {
            var descripcionMonitoreo = await _descripcionMonitoreoService.GetAll();
            var descripcionMonitoreoNoEliminados = descripcionMonitoreo.Where(a => !a.Eliminado).ToList();
            return Ok(descripcionMonitoreoNoEliminados);
        }

        [HttpGet("{IdDescripcionMonitoreo}")]
        public async Task<ActionResult<DescripcionMonitoreo>> GetDescripcionMonitoreo(int IdDescripcionMonitoreo)
        {
            var DescripcionMonitoreo = await _descripcionMonitoreoService.GetDMonitoreo(IdDescripcionMonitoreo);
            if (DescripcionMonitoreo == null)
            {
                return BadRequest("User not found");
            }
            if (DescripcionMonitoreo.Eliminado == true)
            {
                return BadRequest("registro no valido");

            }
            return Ok(DescripcionMonitoreo);
        }

        /*[HttpPost]
        public async Task<ActionResult<DescripcionMonitoreo>> CreateDescripcionMonitoreo([FromBody] DescripcionMonitoreo descripcionMonitoreo)
        {
            if (descripcionMonitoreo == null)
            {
                return BadRequest("El objeto es nulo");
            }
            var newDescripcionMonitoreo = await _descripcionMonitoreoService.CreateDMonitoreo(descripcionMonitoreo.Variable, descripcionMonitoreo.UnidadMedida);
            return Ok(newDescripcionMonitoreo);
        }*/

        [HttpPost]
        public async Task<ActionResult<DescripcionMonitoreo>> CreateDescripcionMonitoreo(string Variable, string UnidadMedida)
        {
            var newDescripcionMonitoreo = await _descripcionMonitoreoService.CreateDMonitoreo(Variable, UnidadMedida);
            if (newDescripcionMonitoreo != null)
            {
                return Ok(newDescripcionMonitoreo);
            }
            else
            {
                return BadRequest("Error");
            }
        }

        [HttpPut("{IdDescripcionMonitoreo}")]
        public async Task<ActionResult<DescripcionMonitoreo>> UpdateDescripcionMonitoreo(int IdDescripcionMonitoreo, string Variable, string UnidadMedida)
        {
            var DescripcionMonitoreoToPut = await _descripcionMonitoreoService.UpdateDMonitoreo(IdDescripcionMonitoreo, Variable, UnidadMedida);
            if (DescripcionMonitoreoToPut != null)
            {
                return Ok(DescripcionMonitoreoToPut);
            }
            else
            {
                return BadRequest("Error");
            }
        }

        /*[HttpDelete("{IdDescripcionMonitoreo}")]
        public async Task<ActionResult<DescripcionMonitoreo>> DeleteDescripcionMonitoreo(int IdDescripcionMonitoreo)
        {
            if (IdDescripcionMonitoreo <= 0)
            {
                return BadRequest("Id invalido para eliminar");
            }
            var DeletedDescripcionMonitoreo = await _descripcionMonitoreoService.DeleteDMonitoreo(IdDescripcionMonitoreo);
            return Ok(DeletedDescripcionMonitoreo);
        }*/

        [HttpDelete("Delete/{IdDescripcionMonitoreo}")]
        public async Task<ActionResult<Agricultores>> DeleteDescripcionMonitoreo(int IdDescripcionMonitoreo)
        {

            var descripcionMonitoreoToDelete = await _descripcionMonitoreoService.DeleteDMonitoreo(IdDescripcionMonitoreo);

            if (descripcionMonitoreoToDelete != null)
            {
                return Ok(descripcionMonitoreoToDelete);
            }
            else
            {
                return BadRequest("Error updating the database :(");
            }
        }
    }


}
