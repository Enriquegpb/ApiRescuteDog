using NugetRescuteDog.Models;

namespace ApiRescuteDog.Repositories
{
    public interface IRepoRefugios
    {
        List<Refugio> GetRefugios();
        Refugio DetailsRefugio(int idrefugio);
        Task ModificarDatosRefugio(Refugio refugio);
        Task AgregarRefugio(Refugio refugio);
        Task BajaRefugio(int idrefugio);
        
    }
}
