using ApiRescuteDog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NugetRescuteDog.Models;

namespace ApiRescuteDog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private IRepoComentarios repo;

        public ComentariosController(IRepoComentarios repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<List<Comentario>> GetComentarios()
        {
            return this.repo.GetComentarios();
        }

        [HttpGet("{id}")]
        public ActionResult<Comentario> SeleccionarComentario(int id)
        {
            return this.repo.FindComentario(id);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComentario(int id)
        {
             await this.repo.DeleteComentario(id);
            return Ok();

        }

        [HttpPut]

        public async Task<ActionResult> EditarComentario(Comentario comentario)
        {
            await this.repo.EditComentario(comentario);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> NewComentario(Comentario comentario)
        {
            await this.repo.NewComentario(comentario.IdPost, comentario.Email, comentario.ComentarioDesc, DateTime.UtcNow, comentario.IdUser);
            return Ok();
        }

    }
}
