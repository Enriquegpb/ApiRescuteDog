using ApiRescuteDog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NugetRescuteDog.Models;

namespace ApiRescuteDog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotasController : ControllerBase
    {
        private IRepoMascotas repo;

        public MascotasController(IRepoMascotas repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public ActionResult<List<Mascota>> GenerarInformeAdopciones()
        {
            return  this.repo.GenerarInformeAdopciones();
        }

        [HttpGet]
        [Route("[action]/{idrefugio}")]
        public ActionResult<List<Mascota>> GetMascotasRefugio(int idrefugio)
        {
            return this.repo.GetMascotas(idrefugio);
        }
        [HttpGet("{id}")]
        public ActionResult<Mascota> DetallesMascota(int id)
        {
            return this.repo.DetailsMascota(id);
        }

        [HttpPost]
        public async Task<ActionResult> AltaMascota (Mascota mascota)
        {
            await this.repo.IngresoAnimal(mascota);
            return Ok();
        }
        [HttpDelete("{idrefugio}")]
        public async Task<ActionResult> FullBajaMascotasRufugio(int idrefugio)
        {
            await this.repo.BajasAllMascotasPorRefugio(idrefugio);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> ModificarMascota (Mascota mascota)
        {
            await this.repo.UpdateMascotas(mascota);
            return Ok();
        }
    }
}
