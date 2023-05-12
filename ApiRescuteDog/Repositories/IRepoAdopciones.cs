namespace ApiRescuteDog.Repositories
{
    public interface IRepoAdopciones
    {
        Task NuevaAdopcion(int idmascota, int iduser);
        Task DevolverAnimalAlRefugio(int idmascota);
    }
}
