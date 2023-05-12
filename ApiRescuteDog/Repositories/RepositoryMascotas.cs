using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ApiRescuteDog.Data;
using NugetRescuteDog.Models;

namespace ApiRescuteDog.Repositories
{
    #region PROCEDURESMASCOTAS
    //    CREATE PROCEDURE BAJA_ALL_MASCOTAS_REFUIGIO(@IDREFUGIO INT)
    //AS
    //    DELETE FROM MASCOTAS WHERE IDREFUGIO = @IDREFUGIO
    //GO

    //alter PROCEDURE SP_OBTENER_MASCOTAS_REFUGIOS(@IDREFUGIO INT)
    //AS
    //    SELECT* FROM MASCOTAS
    //    WHERE IDREFUGIO = @IDREFUGIO
    //GO

    //EXEC SP_OBTENER_MASCOTAS_REFUGIOS 2

    //CREATE VIEW V_MASCOTAS_REFUGIOS
    //AS

    //    SELECT MASCOTAS.* FROM MASCOTAS

    //    INNER JOIN REFUGIOS
    //    ON REFUGIOS.IDREFUGIO = MASCOTAS.IDREFUGIO
    //GO
    //SELECT * FROM V_MASCOTAS_REFUGIOS

    //    CREATE PROCEDURE SP_SALIDA_MASCOTA_ADOPTADA(@ID INT)
    //AS
    //    DELETE FROM MASCOTAS WHERE ID = @ID
    //GO

    //CREATE PROCEDURE SP_INGRESO_MASCOTA_RESCATADA(@NOMBRE NVARCHAR(50), @RAZA NVARCHAR(50), @EDAD INT, @ANCHO FLOAT, @ALTO FLOAT, @PESO FLOAT, @DESCRIPCION NVARCHAR(200), @PELIGROSIDAD BIT, @IMAGEN NVARCHAR(600), @ADOPTADO BIT, @IDREFUGIO INT)
    //AS

    //    DECLARE @ID INT

    //    SELECT @ID = ISNULL(MAX(IDMASCOTA), 0) + 1 FROM MASCOTAS --AUTOINCREMENTO

    //    INSERT INTO MASCOTAS VALUES(@ID, @NOMBRE, @RAZA, @EDAD, @ANCHO, @ALTO, @PESO, @DESCRIPCION, @PELIGROSIDAD, @IMAGEN, @ADOPTADO, @IDREFUGIO)
    //GO

    //CREATE PROCEDURE SP_DETALLES_MASCOTA(@ID INT)
    //AS

    //    SELECT* FROM MASCOTAS WHERE IDMASCOTA = @ID
    //GO

    /**
     * CREAMOS UNA VISTA PARA VER TODAS LAS MASCOTAS PARA DESPUES RECUPERARLOS EN INFORME MEDIANTE UN PROCEDURE
     */

    //    CREATE VIEW V_VER_MASCOTAS_ADOPTADAS
    //    AS
    //SELECT MASCOTAS.* FROM MASCOTAS
    //INNER JOIN ADOPCIONES
    //ON MASCOTAS.IDMASCOTA = ADOPCIONES.IDMASCOTA
    //INNER JOIN USERS
    //ON ADOPCIONES.IDUSER = USERS.IDUSER
    //GO

    //    CREATE PROCEDURE SP_GENERAR_INFORME_ADOPCIONES
    //AS
    //SELECT* FROM V_VER_MASCOTAS_ADOPTADAS
    //GO

    //    CREATE PROCEDURE SP_UPDATE_STATE_ADOPCION(@IDMASCOTA INT, @ESTADO BIT)
    //AS
    //    UPDATE MASCOTAS SET ADOPTADO = @ESTADO WHERE IDMASCOTA = @IDMASCOTA
    //GO

    //CREATE PROCEDURE SP_ACTUALIZAR_DATOS_MASCOTA(@IDMASCOTA INT, @IDREFUGIO INT, @NOMBRE NVARCHAR(50), @EDAD FLOAT, @ALTO FLOAT , @PESO FLOAT, @DESCRIPCION NVARCHAR(1000), @IMAGEN NVARCHAR(600))
    //AS
    //    UPDATE MASCOTAS SET IDREFUGIO = @IDREFUGIO, NOMBRE = @NOMBRE, EDAD = @EDAD, PESO = @PESO, DESCRIPCION = @DESCRIPCION, IMAGEN = @IMAGEN WHERE IDMASCOTA = @IDMASCOTA
    //GO
    #endregion
    public class RepositoryMascotas: IRepoMascotas
    {
        private MascotaContext context;
        
        public RepositoryMascotas(MascotaContext context)
        {
            this.context = context;
        }
        public List<Mascota> GetMascotas(int idrefugio)
        {
            string sql = "SP_OBTENER_MASCOTAS_REFUGIOS @IDREFUGIO";
            SqlParameter pamidrefugio = new SqlParameter("@IDREFUGIO", idrefugio);
            var consulta = this.context.Mascotas.FromSqlRaw(sql, pamidrefugio);
            List <Mascota> mascotas = consulta.ToList();
            return mascotas;
        }

        public Mascota DetailsMascota(int idmascota)
        {
            string sql = "SP_DETALLES_MASCOTA @ID";
            SqlParameter pamidanimal = new SqlParameter("@ID", idmascota);
            var consulta = this.context.Mascotas.FromSqlRaw(sql, pamidanimal);
            Mascota mascota = consulta.AsEnumerable().FirstOrDefault();
            return mascota;
        }

        public async Task UpdateEstadoAdopcion(int idmascota,bool estado)
        {
            string sql = "SP_UPDATE_STATE_ADOPCION @IDMASCOTA, @ESTADO";
            SqlParameter pamidmascota = new SqlParameter("@IDMASCOTA", idmascota);
            SqlParameter pamestado = new SqlParameter("@ESTADO", estado);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamidmascota, pamestado);
        }


        public async Task IngresoAnimal(Mascota mascota)
        {
            string sql = "SP_INGRESO_MASCOTA_RESCATADA @NOMBRE, @RAZA, @EDAD, @ANCHO, @ALTO, @PESO, @DESCRIPCION, @PELIGROSIDAD, @IMAGEN, @ADOPTADO, @IDREFUGIO";
            //SqlParameter pamidanimal = new SqlParameter("@ID", mascota.Id);
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", mascota.Nombre);
            SqlParameter pamraza = new SqlParameter("@RAZA", mascota.Raza);
            SqlParameter pamedad = new SqlParameter("@EDAD", mascota.Edad);
            SqlParameter pamancho = new SqlParameter("@ANCHO", mascota.Ancho);
            SqlParameter pamalto = new SqlParameter("@ALTO", mascota.Alto);
            SqlParameter pampeso = new SqlParameter("@PESO", mascota.Peso);
            SqlParameter pamdescripcion = new SqlParameter("@DESCRIPCION", mascota.Descripcion);
            SqlParameter pampeligrosidad = new SqlParameter("@PELIGROSIDAD", mascota.Peligrosidad);
            SqlParameter pamimagen = new SqlParameter("@IMAGEN", mascota.Imagen);
            SqlParameter pamadoptado = new SqlParameter("@ADOPTADO", mascota.Adoptado);
            SqlParameter pamidrefugio = new SqlParameter("@IDREFUGIO", mascota.IdRefugio);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamnombre, pamraza, pamedad, pamancho, pamalto, pampeso, pamdescripcion, pampeligrosidad, pamimagen, pamadoptado, pamidrefugio);
        }

        public List<Mascota> GenerarInformeAdopciones()
        {
            string sql = "SP_GENERAR_INFORME_ADOPCIONES";
            var consulta = this.context.Mascotas.FromSqlRaw(sql);
            List<Mascota> mascotasadoptadas = consulta.ToList();
            return mascotasadoptadas;

        }            

        public async Task UpdateMascotas(Mascota mascota)
        {
            string sql = "SP_ACTUALIZAR_DATOS_MASCOTA  @IDMASCOTA, @IDREFUGIO, @NOMBRE, @EDAD, @ANCHO, @ALTO, @PESO, @DESCRIPCION, @IMAGEN, @PELIGROSIDAD";
            SqlParameter pamidmascota = new SqlParameter("@IDMASCOTA", mascota.Id);
            SqlParameter pamidrefugio = new SqlParameter("@IDREFUGIO", mascota.IdRefugio);
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", mascota.Nombre);
            SqlParameter pamedad = new SqlParameter("@EDAD", mascota.Edad);
            SqlParameter pamancho = new SqlParameter("@ANCHO", mascota.Ancho);
            SqlParameter pamaltura = new SqlParameter("@ALTO", mascota.Alto);
            SqlParameter pampeso= new SqlParameter("@PESO", mascota.Alto);
            SqlParameter pamdescripcion = new SqlParameter("@DESCRIPCION", mascota.Descripcion);
            SqlParameter pamImagen = new SqlParameter("@IMAGEN", mascota.Imagen);
            SqlParameter pampeligrosidad = new SqlParameter("@PELIGROSIDAD", mascota.Peligrosidad);
            await this.context.Database.ExecuteSqlRawAsync(sql,pamidmascota, pamidrefugio, pamnombre, pamedad, pamancho, pamaltura, pampeso, pamdescripcion, pamImagen, pampeligrosidad);
        }  
        public async Task BajasAllMascotasPorRefugio(int idrefugio)
        {            
            string sql = "BAJA_ALL_MASCOTAS_REFUIGIO @IDREFUGIO";
            SqlParameter pamidrefugio = new SqlParameter("@IDREFUGIO", idrefugio);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamidrefugio);
        }


        //public async Task <List<Mascota>> SaveInformeAsync(List<Mascota> adopciones)
        //{
        //    await this.context.BulkInsertAsync(adopciones);
        //    return adopciones;
        //}
    }
}
