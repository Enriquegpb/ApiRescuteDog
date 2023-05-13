using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ApiRescuteDog.Data;
using NugetRescuteDog.Models;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;

namespace ApiRescuteDog.Repositories
{
    #region
    //    CREATE PROCEDURE SP_GET_POST
    //AS
    // SELECT * FROM RESCUTEBLOG ORDER BY FECHA DESC
    //GO

    //ALTER PROCEDURE SP_NEW_POST(@TITULO NVARCHAR(50), @CONTENIDO NVARCHAR(50), @IMAGEN NVARCHAR(600), @IDUSER INT, @FEHCA NVARCHAR(75))
    //AS
    //    DECLARE @IDMAXPOST INT

    //    SELECT @IDMAXPOST = ISNULL(MAX(IDPOST), 0) + 1 FROM RESCUTEBLOG
    // INSERT INTO RESCUTEBLOG VALUES(@IDMAXPOST, @TITULO, @CONTENIDO, @IMAGEN, @IDUSER, @FEHCA)
    //GO

    //    ALTER PROCEDURE SP_UPDATE_POST(@IDPOST INT, @TITULO NVARCHAR(50), @CONTENIDO NVARCHAR(50), @IMAGEN NVARCHAR(600), @IDUSER INT, @FECHA NVARCHAR(75))
    //AS
    //    UPDATE RESCUTEBLOG SET @TITULO = @TITULO, CONTENIDO = @CONTENIDO, IMAGEN = @IMAGEN, IDUSER = @IDUSER, FECHA = FECHA
    //    WHERE IDPOST = @IDPOST
    //GO

    //ALTER PROCEDURE SP_DELETE_POST(@IDPOST INT)
    //AS
    //DELETE FROM COMENTARIOS WHERE IDPOST = @IDPOST
    // DELETE FROM RESCUTEBLOG WHERE IDPOST = @IDPOST
    //GO

    //CREATE PROCEDURE BAJA_ALL_PUBLICACIONES(@IDUSER INT)
    //AS
    //    DELETE FROM COMENTARIOS WHERE @IDUSER = @IDUSER
    //GO
    #endregion
    public class RepositoryBlog: IRepoBlog
    {
        private MascotaContext context;
        public RepositoryBlog(MascotaContext context)
        {
            this.context = context;
        }

        public async Task DeletePost(int idpost)
        {
            string sql = "SP_DELETE_POST @IDPOST";
            SqlParameter pamidpost = new SqlParameter("@IDPOST", idpost);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamidpost);

        }

        public async Task EditPostAsync(BlogModel post)
        {
            string sql = "SP_UPDATE_POST @IDPOST,@TITULO, @CONTENIDO, @IMAGEN, @IDUSER, @FECHA";
            SqlParameter pamidpost = new SqlParameter("@IDPOST", post.IdPost);
            SqlParameter pamtitulo = new SqlParameter("@TITULO", post.Titulo);
            SqlParameter pamcontenido = new SqlParameter("@CONTENIDO", post.Contenido);
            SqlParameter pamimagen = new SqlParameter("@IMAGEN", post.Imagen);
            SqlParameter pamiduser = new SqlParameter("@IDUSER", post.IdUser);
            SqlParameter pamfecha = new SqlParameter("@FECHA", post.Fecha);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamidpost,pamtitulo, pamcontenido, pamimagen, pamiduser, pamfecha);
        }

        public BlogModel FindPost(int idpost)
        {
            return this.context.Publicaciones.Where(x => x.IdPost == idpost).FirstOrDefault();
        }

        public List<BlogModel> GetPost()
        {
            string sql = "SP_GET_POST";
            var consulta = this.context.Publicaciones.FromSqlRaw(sql);
            List<BlogModel> publicaciones = consulta.ToList();
            return publicaciones;
        }

        public async Task NewPost(BlogModel post)
        {
            string sql = "SP_NEW_POST @TITULO, @CONTENIDO, @IMAGEN, @IDUSER, @FEHCA";
            SqlParameter pamtitulo = new SqlParameter("@TITULO", post.Titulo);
            SqlParameter pamcontenido = new SqlParameter("@CONTENIDO", post.Contenido);
            SqlParameter pamimagen = new SqlParameter("@IMAGEN", post.Imagen);
            SqlParameter pamiduser = new SqlParameter("@IDUSER", post.IdUser);
            SqlParameter pamfecha = new SqlParameter("@FEHCA", post.Fecha);
            await this.context.Database.ExecuteSqlRawAsync(sql,pamtitulo, pamcontenido, pamimagen, pamiduser, pamfecha);  
        }
    }
}
