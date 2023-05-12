using ApiRescuteDog.Helpers;
using ApiRescuteDog.Models;
using ApiRescuteDog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NugetRescuteDog.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiRescuteDog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IRepoAutentication repo;
        private HelperOAuthToken helperOAuth;
        public AuthController(IRepoAutentication repo, HelperOAuthToken helperOAuth)
        {
            this.repo = repo;
            this.helperOAuth = helperOAuth;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            User usuario =
                await this.repo.ExisteUsuario(model.UserName, model.Password);

            if (usuario == null)
            {
                return Unauthorized();
            }
            else
            {
                SigningCredentials credentials =
                    new SigningCredentials(this.helperOAuth.GetKeyToken(), SecurityAlgorithms.HmacSha256);
                //GENERACION DEL JWT TOKEN CON SUS CORRESPONDIENTES DATOS
                string jsonUsuarioVideojuego = JsonConvert.SerializeObject(usuario);
                Claim[] informacion = new[]
                {
                    new Claim("UserData", jsonUsuarioVideojuego)
                };

                JwtSecurityToken token =
                    new JwtSecurityToken(
                        claims: informacion,
                        issuer: this.helperOAuth.Issuer,
                        audience: this.helperOAuth.Audience,
                        signingCredentials: credentials,
                        expires: DateTime.UtcNow.AddMinutes(15),
                        notBefore: DateTime.UtcNow
                        );
                return Ok(new
                {
                    response =
                    new JwtSecurityTokenHandler().WriteToken(token)
                });


            }
        }
        [HttpPost]
        [Route("[action]/{username}/{password}/{email}/{phone}/{imagen}/{birdthday}")]
        public async Task<ActionResult> Singup(string username, string password, string email, string phone, string imagen, string birdthday)
        {
            await this.repo.NewUser(username, password, email, phone, imagen, birdthday);
            return Ok();
        }
    }
}
