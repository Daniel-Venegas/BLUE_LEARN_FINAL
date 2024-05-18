using APIBlueLearn.Model;
using APIBlueLearn.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIBlueLearn.Controllers
{

    [ApiController]

    [Route("api/[controller]")]

    public class AgricultoresController : ControllerBase
    {

        private readonly IAgricultoresService _agricultoresService;

        public AgricultoresController(IAgricultoresService agricultoresService)
        {
            _agricultoresService = agricultoresService;
        }

        [HttpGet]
        /*public async Task<ActionResult<List<Agricultores>>> GetAll()
        {
            return Ok(await _agricultoresService.GetAll());
        }*/

        public async Task<ActionResult<List<Agricultores>>> GetAll()
        {
            var agricultores = await _agricultoresService.GetAll();
            var agricultoresNoEliminados = agricultores.Where(a => !a.Eliminado).ToList();
            return Ok(agricultoresNoEliminados);
        }


        [HttpGet("{IdAgricultor}")]
        public async Task<ActionResult<Agricultores>> GetAgricultores(int IdAgricultor)
        {

            var Agricultores = await _agricultoresService.GetAgricultor(IdAgricultor);

            if (Agricultores == null)
            {
                return BadRequest("user not found");
            }
            if (Agricultores.Eliminado == true)
            {
                return BadRequest("registro no valido");

            }
            return Ok(Agricultores);
        }

        /*[HttpPost]
        public async Task<ActionResult<Agricultores>> CreateAgricultores([FromBody] Agricultores agricultores)
        {
            if (agricultores == null)
            {
                return BadRequest("El objeto agricultores es nulo");
            }
            var newAgricultores = await _agricultoresService.CreateAgricultor(agricultores.IdJugador, agricultores.Nombres, agricultores.Apellidos, agricultores.Direccion, agricultores.Contacto, agricultores.password, agricultores.Jugador);
            return Ok(newAgricultores);


        }*/
        [HttpPost]
        public async Task<ActionResult<Agricultores>> CreateAgricultores(int IdJugador, string Nombres, string Apellidos, string Direccion, string Contacto, string password)
        {
            var newAgricultores = await _agricultoresService.CreateAgricultor(IdJugador, Nombres, Apellidos, Direccion, Contacto, password);
            if (newAgricultores != null)
            {
                return Ok(newAgricultores);
            }
            else
            {
                return BadRequest("Error");
            }
        }


        [HttpPut("{IdAgricultor}")]
        public async Task<ActionResult<Agricultores>> UpdateAgricultores(int IdAgricultor, int IdJugador, string Nombres, string Apellidos, string Direccion, string Contacto, string password)
        {
            var AgricultorToPut = await _agricultoresService.UpdateAgricultor(IdAgricultor, IdJugador, Nombres, Apellidos, Direccion, Contacto, password);
            if (AgricultorToPut != null)
            {
                return Ok(AgricultorToPut);
            }
            else
            {
                return BadRequest("Error updating database");
            }

        }



        /*[HttpDelete("{IdAgricultor}")]
        public async Task<ActionResult<Agricultores>> DeleteAgricultores(int IdAgricultor)
        {
            var Agricultores = await _agricultoresService.GetAgricultor(IdAgricultor);
            if (Agricultores != null)
            {
                Agricultores.Eliminado = true;
            }
            
            return Ok(Agricultores);
        }*/

        [HttpDelete("Delete/{IdAgricultor}")]
        public async Task<ActionResult<Agricultores>> DeleteAgricultores(int IdAgricultor)
        {

            var agricultoresToDelete = await _agricultoresService.DeleteAgricultor(IdAgricultor);

            if (agricultoresToDelete != null)
            {
                return Ok(agricultoresToDelete);
            }
            else
            {
                return BadRequest("Error updating the database :(");
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<bool>> Login(string Contacto, string password)
        {
            if (string.IsNullOrEmpty(Contacto) || string.IsNullOrEmpty(password))
            {
                return BadRequest("El nombre de usuario y la contraseña son obligatorios.");
            }


            var user = await _agricultoresService.Login(Contacto, password);
            if (user != null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}
