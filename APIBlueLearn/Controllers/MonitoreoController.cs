using APIBlueLearn.Model;
using APIBlueLearn.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIBlueLearn.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MonitoreoController : ControllerBase
    {

        private readonly IMonitoreoService _monitoreoService;

        public MonitoreoController(IMonitoreoService monitoreoService)
        {
            _monitoreoService = monitoreoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Monitoreo>>> GetAll()
        {
            var monitoreo = await _monitoreoService.GetAll();
            var monitoreoNoEliminados = monitoreo.Where(a => !a.Eliminado).ToList();
            return Ok(monitoreoNoEliminados);
        }

        [HttpGet("{IdMonitoreo}")]
        public async Task<ActionResult<Monitoreo>> GetMonitoreo(int IdMonitoreo)
        {
            var Monitoreo = await _monitoreoService.GetMonitoreo(IdMonitoreo);
            if (Monitoreo == null)
            {
                return BadRequest("user not found");
            }
            if (Monitoreo.Eliminado == true)
            {
                return BadRequest("registro no valido");

            }
            return Ok(Monitoreo);
        }

        /*[HttpPost]
        public async Task<ActionResult<Monitoreo>> CreateMonitoreo([FromBody] Monitoreo monitoreo)
        {
            if (monitoreo == null)
            {
                return BadRequest("El objeto es nulo");
            }
            var newMonitoreo = await _monitoreoService.CreateMonitoreo(monitoreo.FechaMonitoreo, monitoreo.Valor, monitoreo.IdDescripcionMonitoreo, monitoreo.IdCultivo);
            return Ok(newMonitoreo);
        }*/


        [HttpPost]
        public async Task<ActionResult<Monitoreo>> CreateMonitoreo(int Valor, int IdDescripcionMonitoreo, int IdCultivo)
        {
            var newMonitoreo = await _monitoreoService.CreateMonitoreo(Valor, IdDescripcionMonitoreo, IdCultivo);
            if (newMonitoreo != null)
            {
                return Ok(newMonitoreo);
            }
            else
            {
                return BadRequest("Error");
            }
        }

        [HttpPut("{IdMonitoreo}")]
        public async Task<ActionResult<Monitoreo>> UpdateMonitoreo(int IdMonitoreo, int Valor, int IdDescripcionMonitoreo, int IdCultivo)
        {
            var MonitoreoToPut = await _monitoreoService.UpdateMonitoreo(IdMonitoreo, Valor, IdDescripcionMonitoreo, IdCultivo);
            if (MonitoreoToPut != null)
            {
                return Ok(MonitoreoToPut);
            }
            else
            {
                return BadRequest("Error");
            }
        }

        /*[HttpDelete("{IdMonitoreo}")]
        public async Task<ActionResult<Monitoreo>> DeleteMonitoreo(int IdMonitoreo)
        {
            if (IdMonitoreo <= 0)
            {
                return BadRequest("Id invalido para eliminar");
            }
            var DeletedMonitoreo = await _monitoreoService.DeleteMonitoreo(IdMonitoreo);
            return Ok(DeletedMonitoreo);
        }*/

        [HttpDelete("Delete/{IdMonitoreo}")]
        public async Task<ActionResult<Agricultores>> DeleteMonitoreo(int IdMonitoreo)
        {

            var monitoreoToDelete = await _monitoreoService.DeleteMonitoreo(IdMonitoreo);

            if (monitoreoToDelete != null)
            {
                return Ok(monitoreoToDelete);
            }
            else
            {
                return BadRequest("Error updating the database :(");
            }
        }
    }
}
