using NugetRescuteDog.Models;

namespace ApiRescuteDog.Repositories
{
    public interface IRepoComentarios
    {
        List<Comentario> GetComentarios();
        Comentario FindComentario(int idcomentario);
        Task NewComentario(int idpost, string correo, string comentario, DateTime fechacomentario, int iduser);
        Task EditComentario(Comentario comentario);
        Task DeleteComentario(int idcomentario);
    }
}
