using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ApiRescuteDog.Data;
using NugetRescuteDog.Models;
using System.Collections.Generic;

namespace ApiRescuteDog.Repositories
{
    public class RepositoryComentarios : IRepoComentarios
    {
        #region PROCEDURES
        //        CREATE PROCEDURE SP_DELTE_COMENTARIO(@IDCOMENTARIO INT)
        //AS
        //    DELETE FROM COMENTARIOS WHERE IDCOMENTARIO = @IDCOMENTARIO
        //GO
        //ALTER PROCEDURE SP_UPDATE_COMENTARIO(@IDCOMENTARIO INT, @COMENTARIO NVARCHAR(600))
        //AS
        //    UPDATE COMENTARIOS SET COMENTARIOTEXT = @COMENTARIO WHERE IDCOMENTARIO = @IDCOMENTARIO
        //GO


        //ALTER PROCEDURE SP_NEW_COMENTARIO_POST(@IDPOST INT, @CORREO NVARCHAR(50), @COMENTARIO NVARCHAR(600), @FECHA DATE, @IDUSER INT)
        //AS
        //    DECLARE @IDCOMENTARIO INT

        //    SELECT @IDCOMENTARIO = ISNULL(MAX(IDCOMENTARIO), 0) + 1 FROM COMENTARIOS

        //    INSERT INTO COMENTARIOS VALUES(@IDCOMENTARIO, @IDPOST, @CORREO, @COMENTARIO, @FECHA, @IDUSER)
        //GO



        //        CREATE PROCEDURE BAJA_ALL_COMENTARIOS(@IDPOST INT)
        //AS
        //    DELETE FROM COMENTARIOS WHERE IDPOST = @IDPOST
        //GO


        #endregion
        private MascotaContext context;
        public RepositoryComentarios(MascotaContext context)
        {
            this.context = context;
        }
        public async Task DeleteComentario(int idcomentario)
        {
            string sql = "SP_DELTE_COMENTARIO @IDCOMENTARIO";
            SqlParameter pamidpost = new SqlParameter("@IDCOMENTARIO", idcomentario);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamidpost);
        }

        public async Task EditComentario(Comentario comentario)
        {
            string sql = "SP_UPDATE_COMENTARIO @IDCOMENTARIO, @COMENTARIO";
            SqlParameter pamidcomentario = new SqlParameter("@IDCOMENTARIO", comentario.IdComentario);
            SqlParameter pamcomentario = new SqlParameter("@COMENTARIO", comentario.ComentarioDesc);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamidcomentario, pamcomentario);
        }

        public Comentario FindComentario(int idcomentario)
        {
            return this.context.Comentarios.Where(x => x.IdComentario == idcomentario).FirstOrDefault();
        }

        public List<Comentario> GetComentarios()
        {
            return  this.context.Comentarios.OrderBy(x => x.Fecha).ToList();
        }

        public async Task NewComentario(int idpost, string correo, string comentario, DateTime fechacomentario, int iduser)
        {
            string sql = "SP_NEW_COMENTARIO_POST @IDPOST,@CORREO,@COMENTARIO,@FECHA,@IDUSER";
            SqlParameter pamidpost = new SqlParameter("@IDPOST", idpost);
            SqlParameter pamemail = new SqlParameter("@CORREO", correo);
            SqlParameter pamcomentario = new SqlParameter("@COMENTARIO", comentario);
            SqlParameter pamfecha = new SqlParameter("@FECHA", fechacomentario);
            SqlParameter pamiduser = new SqlParameter("@IDUSER", iduser);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamidpost, pamemail, pamcomentario, pamfecha, pamiduser);
        }
    }
}
