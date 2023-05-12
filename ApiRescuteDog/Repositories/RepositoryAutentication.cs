using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ApiRescuteDog.Data;
using ApiRescuteDog.Helpers;
using NugetRescuteDog.Models;

namespace ApiRescuteDog.Repositories
{
    public class RepositoryAutentication: IRepoAutentication
    {
        #region PROCEDURES
        //        CREATE PROCEDURE SP_UPDATE_PERFIL_USUARIO(@USERNAME NVARCHAR(60), @TELEFONO NVARCHAR(50), @EMAIL NVARCHAR(50), @IMAGEN NVARCHAR(600), @IDUSER INT)
        //AS
        //    UPDATE USERS_VOLUNTARIOS SET USERNAME = @USERNAME, PHONE = @TELEFONO, EMAIL = @EMAIL, IMAGEN = @IMAGEN WHERE IDUSER = @IDUSER
        //GO

        //        CREATE PROCEDURE BajaUsuario(@IDUSER INT)
        //AS
        //    DELETE FROM USERS_VOLUNTARIOS WHERE IDUSER = @IDUSER
        //GO
        #endregion

        private MascotaContext context;
        public RepositoryAutentication(MascotaContext context)
        {
            this.context = context;
        }

        public User LogIn(string email, string password)
        {
            User user =
                this.context.Users.FirstOrDefault(z => z.Email == email);
            if (user == null)
            {
                return null;
            }
            else
            {
                byte[] passUsuario = user.Password;
                string salt = user.Salt;
                byte[] temp = HelperCryptography.EncryptPassword(password, salt);
                bool respuesta = HelperCryptography.CompareArrays(passUsuario, temp);
                if(respuesta == true)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }
        public int GetMaximoUser()
        {
            int maximo = 1 + (from datos in this.context.Users
                              select datos).Max(x => x.Id);
            if (maximo == 0)
            {
                maximo = 1;
            }
            return maximo;
        }
        public async Task NewUser(string username, string password, string email, string phone, string imagen,string birdthday)
        {
            User user = new User();
            user.Id = this.GetMaximoUser();
            user.Username = username;
            user.Email = email;
            user.Phone = phone;
            user.Imagen = imagen;
            user.Contrasena = password;
            user.Birdthday = birdthday;
            user.Salt = HelperCryptography.GenerateSalt();
            user.Password = HelperCryptography.EncryptPassword(password, user.Salt);
            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();
        }

        //public User FindUser(int iduser)
        //{
        //    return this.context.Users.FirstOrDefault(x => x.Id == iduser);
        //}

        public async Task<User> ExisteUsuario
    (string email, string password)
        {
            User user = this.FindUser(email);

            if (user == null)
            {
                return null;
            }
            else
            {
                //RECUPERAMOS EL PASSWORD CIFRADO DE LA BBDD
                byte[] passUsuario = user.Password;
                //DEBEMOS CIFRAR DE NUEVO EL PASSWORD DE USUARIO
                //JUNTO A SU SALT UTILIZANDO LA MISMA TECNICA
                string salt = user.Salt;
                byte[] temp =
                    HelperCryptography.EncryptPassword(password, salt);
                //COMPARAMOS LOS DOS ARRAYS
                bool respuesta =
                    HelperCryptography.CompareArrays(passUsuario, temp);
                if (respuesta == true)
                {
                    //SON IGUALES
                    return user;
                }
                else
                {
                    return null;
                }
            }

        }

        public User FindUser(string email)
        {
            return this.context.Users.FirstOrDefault(x => x.Email == email);
        }

        public async Task UpdatePerfilusuario(string username, string telefono, string email, string imagen, int iduser)
        {
            string sql = "SP_UPDATE_PERFIL_USUARIO @USERNAME, @TELEFONO, @EMAIL, @IMAGEN, @IDUSER";
            SqlParameter pamiduser = new SqlParameter("@IDUSER", iduser);
            SqlParameter pamusername = new SqlParameter("@USERNAME", username);
            SqlParameter pamtelefono = new SqlParameter("@TELEFONO", telefono);
            SqlParameter pamemail = new SqlParameter("@EMAIL", email);
            SqlParameter pamimagen = new SqlParameter("@IMAGEN", imagen);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamusername, pamtelefono, pamemail, pamimagen, pamiduser);

        }

        public async Task BajaUsuario(int iduser)
        {
            string sql = "SP_BAJA_USER @IDUSER";
            SqlParameter pamiduser = new SqlParameter("@IDUSER", iduser);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamiduser);
        }
    }
}
