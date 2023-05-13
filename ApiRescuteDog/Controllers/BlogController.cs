using ApiRescuteDog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NugetRescuteDog.Models;

namespace ApiRescuteDog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private IRepoBlog repo;

        public BlogController(IRepoBlog repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public ActionResult<List<BlogModel>> GetBlogs()
        {
            return this.repo.GetPost();
        } 
        [HttpGet("{id}")]
        public ActionResult<BlogModel> FindBlog(int id)
        {
            return this.repo.FindPost(id);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PublicacionNueva(BlogModel blog)
        {
            await this.repo.NewPost(blog);
            return Ok();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePublicacion(int id)
        {
            await this.repo.DeletePost(id);
            return Ok();
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> ModficarPublicacion(BlogModel blog)
        {
            await this.repo.EditPostAsync(blog);
            return Ok();
        }
    }
}
