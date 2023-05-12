using ApiRescuteDog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NugetRescuteDog.Models;

namespace ApiRescuteDog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoluntariosController : ControllerBase
    {
        private IRepoVoluntarios repo;

        public VoluntariosController(IRepoVoluntarios repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public ActionResult<List<Voluntario>> GetVoluntarios()
        {
            return this.repo.Getvoluntarios();
        }
        [HttpGet("{id}")]
        public ActionResult<Voluntario> FindVoluntario(int id)
        {
            return this.repo.FindVoluntario(id);
        }

        [HttpPost]
        public async Task<ActionResult> AltaVoluntario(Voluntario voluntario)
        {
            await this.repo.NewVoluntario(voluntario);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> ModificarDatosVoluntario(Voluntario voluntario)
        {
            await this.repo.ModificarDatosVoluntario(voluntario);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> BajaVoluntario(int id)
        {
            await this.repo.BajaVoluntario(id);
            return Ok();
        }
    }
}
