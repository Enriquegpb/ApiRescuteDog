using ApiRescuteDog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NugetRescuteDog.Models;

namespace ApiRescuteDog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdopcionesController : ControllerBase
    {
        private IRepoAdopciones repo;
        public AdopcionesController(IRepoAdopciones repo)
        {
            this.repo = repo;
        }
        [HttpPost]
        [Route("[action]/{idmascota}/{iduser}")]
        public async Task<ActionResult> NuevaAdopcion(int idmascota, int iduser)
        {
            await this.repo.NuevaAdopcion(idmascota, iduser);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAdopcion(int id)
        {
            await this.repo.DevolverAnimalAlRefugio(id);
            return Ok();
        }
    }
}
