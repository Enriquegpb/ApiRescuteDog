using NugetRescuteDog.Models;

namespace ApiRescuteDog.Repositories
{
    public interface IRepoMascotas
    {
        List<Mascota> GetMascotas(int idrefugio);
        //GenerarInformeAdopciones ES EL PRIMER METODO PARA GENERAR EL EXCEL, YA QUE ESTE METODO 
        //RECUPERAR MEDIANTE UN STORED PROCUDRE LAS MASCOTAS QUE HAN SIDO ADOPTADAS EN UNA COLECCION
        List<Mascota> GenerarInformeAdopciones();
        // EL SEGUNDO METODO NOS PERMITE GUARDAR LA COLECCION DE MASCOTAS QUE HAN
        //SIDO ADOPTADAS EN UNA COLECION PASANDOLE EL INFORME GENERADO
        //Task<List<Mascota>> SaveInformeAsync(List<Mascota> adopciones);
        Mascota DetailsMascota(int idmascota);
        Task IngresoAnimal(Mascota mascota);
        //void AdoptarMascota(int idmascota);
        Task UpdateEstadoAdopcion(int idmascota, bool estado);

        Task UpdateMascotas(Mascota mascota);
        Task BajasAllMascotasPorRefugio(int idrefugio);

    }
}
