﻿using APIBlueLearn.Model;
using APIBlueLearn.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace APIBlueLearn.Controllers
{

    [ApiController]

    [Route("api/[controller]")]
    public class CosechasController : ControllerBase
    {

        private readonly ICosechasService _cosechasService;

        public CosechasController(ICosechasService cosechasService)
        {
            _cosechasService = cosechasService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Cosechas>>> GetAll()
        {
            var cosechas = await _cosechasService.GetAll();
            var cosechasNoEliminados = cosechas.Where(a => !a.Eliminado).ToList();
            return Ok(cosechasNoEliminados);
        }

        [HttpGet("{IdCosecha}")]
        public async Task<ActionResult<Cosechas>> GetCosechas(int IdCosecha)
        {
            var Cosechas = await _cosechasService.GetCosechas(IdCosecha);
            if (Cosechas == null)
            {
                return BadRequest("User not found");

            }
            if (Cosechas.Eliminado == true)
            {
                return BadRequest("registro no valido");

            }
            return Ok(Cosechas);
        }

        /*[HttpPost]
        public async Task<ActionResult<Cosechas>> CreateCosechas([FromBody] Cosechas cosechas)
        {
            if (cosechas == null)
            {
                return BadRequest("El objeto cosechas es nulo");
            }
            var newCosechas = await _cosechasService.CreateCosechas(cosechas.FechaCosecha, cosechas.CantidadRecogida, cosechas.IdCultivo, cosechas.IdTemporada);
            return Ok(newCosechas);
        }*/

        [HttpPost]
        public async Task<ActionResult<Cosechas>> CreateCosechas(int CantidadRecogida, int IdCultivo, int IdTemporada)
        {
            var newCosechas = await _cosechasService.CreateCosechas(CantidadRecogida, IdCultivo, IdTemporada);
            if (newCosechas != null)
            {
                return Ok(newCosechas);
            }
            else
            {
                return BadRequest("Error");
            }
        }

        [HttpPut("{IdCosecha}")]
        public async Task<ActionResult<Cosechas>> UpdateCosechas(int IdCosechas, int CantidadRecogida, int IdCultivo, int IdTemporada)
        {
            var CosechasToPut = await _cosechasService.UpdateCosechas(IdCosechas, CantidadRecogida, IdCultivo, IdTemporada);
            if (CosechasToPut != null)
            {
                return Ok(CosechasToPut);
            }
            else
            {
                return BadRequest("Error");
            }
        }

        /*[HttpDelete("{IdCosechas}")]
        public async Task<ActionResult<Cosechas>> DeleteCosechas(int IdCosechas)
        {
            if (IdCosechas <= 0)
            {
                return BadRequest("Id invalido para eliminar");

            }
            var DeletedCosechas = await _cosechasService.DeleteCosechas(IdCosechas);
            return Ok(DeletedCosechas);
        }*/

        [HttpDelete("Delete/{IdCosecha}")]
        public async Task<ActionResult<Agricultores>> DeleteCosechas(int IdCosecha)
        {

            var cosechasToDelete = await _cosechasService.DeleteCosechas(IdCosecha);

            if (cosechasToDelete != null)
            {
                return Ok(cosechasToDelete);
            }
            else
            {
                return BadRequest("Error updating the database :(");
            }
        }

    }
}
