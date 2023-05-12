using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ApiRescuteDog.Data;
using NugetRescuteDog.Models;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ApiRescuteDog.Repositories
{
    #region PROCEDURES VOLUNTARIOS


    //ALTER PROCEDURE SP_NEWVOLUNTARIO(@NOMBRE NVARCHAR(50), @MENSAJE NVARCHAR(50), @IMAGEN NVARCHAR(600), @CORREO NVARCHAR(50), @MUNICIPIO NVARCHAR(50), @FECHANAC DATE, @IDREFUGIO INT)
    //    AS
    //        DECLARE @IDVOLUNTARIO INT
    //        SELECT @IDVOLUNTARIO = ISNULL(MAX(IDVOLUNTARIO), 1) + 1 FROM VOLUNTARIOS;

    //INSERT INTO VOLUNTARIOS VALUES(@IDVOLUNTARIO, @NOMBRE, @MENSAJE, @IMAGEN, @CORREO, @MUNICIPIO, @FECHANAC, @IDREFUGIO)
    //    GO

    //    CREATE PROCEDURE SP_BAJA_VOLUNTARIO(@IDVOLUNTARIO INT)AS
    //        DELETE FROM VOLUNTARIOS WHERE IDVOLUNTARIO = @IDVOLUNTARIO
    //GO

    //ALTER PROCEDURE SP_MODIFICAR_DATOS_VOLUNTARIO(@IDVOLUNTARIO INT, @NOMBRE NVARCHAR(50), @MENSAJE NVARCHAR(600), @IMAGEN NVARCHAR(1000),@CORREO NVARCHAR(50), @MUNICIPIO NVARCHAR(80), @FECHANAC DATE, @IDREFUGIO INT)AS
    //    UPDATE VOLUNTARIOS SET NOMBRE =  @NOMBRE, MENSAJE = @MENSAJE, IMAGEN = @IMAGEN, CORREO = @CORREO, MUNICIPIO = @MUNICIPIO, FECHA_NACIMIENTO = @FECHANAC, IDREFUGIO = @IDREFUGIO WHERE IDVOLUNTARIO = @IDVOLUNTARIO
    //GO

    //    CREATE PROCEDURE SP_FIND_VOLUNTARIO(@IDVOLUNTARIO INT)
    //AS

    //    SELECT* FROM VOLUNTARIOS WHERE IDVOLUNTARIO = @IDVOLUNTARIO
    //GO

    #endregion
    public class RepositoryVoluntarios : IRepoVoluntarios
    {
        private MascotaContext context;
        public RepositoryVoluntarios(MascotaContext context)
        {
            this.context = context;
        }

        public async Task BajaVoluntario(int idvoluntario)
        {
            string sql = "SP_BAJA_VOLUNTARIO @IDVOLUNTARIO";
            SqlParameter pamidvoluntario = new SqlParameter("@IDVOLUNTARIO", idvoluntario);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamidvoluntario);
        }

        public Voluntario FindVoluntario(int idvoluntario)
        {
            string sql = "SP_FIND_VOLUNTARIO @IDVOLUNTARIO";
            SqlParameter pamidanimal = new SqlParameter("@IDVOLUNTARIO", idvoluntario);
            var consulta = this.context.Voluntarios.FromSqlRaw(sql, pamidanimal);
            Voluntario voluntario = consulta.AsEnumerable().FirstOrDefault();
            return voluntario;
        }

        public List<Voluntario> Getvoluntarios()
        {
            return this.context.Voluntarios.ToList();
        }

        public async Task ModificarDatosVoluntario(Voluntario voluntario)
        {
            string sql = "SP_MODIFICAR_DATOS_VOLUNTARIO @IDVOLUNTARIO, @NOMBRE, @MENSAJE, @IMAGEN, @CORREO, @MUNICIPIO, @FECHANAC, @REFUGIO";
            SqlParameter pamidvoluntario = new SqlParameter("@IDVOLUNTARIO", voluntario.IdVoluntario);
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", voluntario.Nombre);
            SqlParameter pammensaje = new SqlParameter("@MENSAJE", voluntario.Mensaje);
            SqlParameter pamimagen = new SqlParameter("@IMAGEN", voluntario.Imagen);
            SqlParameter pamcorreo = new SqlParameter("@CORREO", voluntario.Correo);
            SqlParameter pammunicipio = new SqlParameter("@MUNICIPIO", voluntario.Municipio);
            SqlParameter pamfechanac = new SqlParameter("@FECHANAC", voluntario.Fecha_Nacimiento);
            SqlParameter pamrefugio = new SqlParameter("@REFUGIO", voluntario.IdRefugio);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamidvoluntario, pamnombre, pammensaje, pamimagen, pamcorreo, pammunicipio, pamfechanac, pamrefugio);
        }

        public async Task NewVoluntario(Voluntario voluntario)
        {
            string sql = "SP_NEWVOLUNTARIO @NOMBRE, @MENSAJE, @IMAGEN, @CORREO, @MUNICIPIO, @FECHANAC, @IDREFUGIO";
            SqlParameter pamnombre = new SqlParameter("@NOMBRE", voluntario.Nombre);
            SqlParameter pammensaje = new SqlParameter("@MENSAJE", voluntario.Mensaje);
            SqlParameter pamimagen = new SqlParameter("@IMAGEN", voluntario.Imagen);
            SqlParameter pamcorreo = new SqlParameter("@CORREO", voluntario.Correo);
            SqlParameter pammunicipio = new SqlParameter("@MUNICIPIO", voluntario.Municipio);
            SqlParameter pamfechanac = new SqlParameter("@FECHANAC", voluntario.Fecha_Nacimiento);
            SqlParameter pamrefugio = new SqlParameter("@IDREFUGIO", voluntario.IdRefugio);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamnombre, pammensaje, pamimagen, pamcorreo, pammunicipio, pamfechanac, pamrefugio);
        }

        
    }
}
