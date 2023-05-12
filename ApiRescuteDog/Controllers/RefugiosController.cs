using ApiRescuteDog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NugetRescuteDog.Models;

namespace ApiRescuteDog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefugiosController : ControllerBase
    {
        private IRepoRefugios repo;
        public RefugiosController(IRepoRefugios repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public ActionResult<List<Refugio>> GetRefugios()
        {
            return this.repo.GetRefugios();
        }
        [HttpGet("{id}")]
        public ActionResult<Refugio> GetRefugios(int id)
        {
            return this.repo.DetailsRefugio(id);
        }
        [HttpPost]
        public async Task<ActionResult> AltaRefugio(Refugio refugio)
        {
            await this.repo.AgregarRefugio(refugio);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> ModificarRefugio(Refugio refugio)
        {
            await this.repo.ModificarDatosRefugio(refugio);
            return Ok();
        } 
        [HttpDelete("{id}")]
        public async Task<ActionResult> BajaRefugio(int id)
        {
            await this.repo.BajaRefugio(id);
            return Ok();
        }

    }
}
