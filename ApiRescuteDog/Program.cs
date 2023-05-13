using ApiRescuteDog.Data;
using ApiRescuteDog.Helpers;
using ApiRescuteDog.Repositories;
using Microsoft.EntityFrameworkCore;
using NSwag.Generation.Processors.Security;
using NSwag;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//string connectionString = builder.Configuration.GetConnectionString("SqlPanimales");
builder.Services.AddSingleton<HelperOAuthToken>();
HelperOAuthToken helper = new HelperOAuthToken(builder.Configuration);
//AÑADIR LAS OPCIONES DE AUTENTICACION
builder.Services.AddAuthentication(helper.GetAuthenticationOptions()).AddJwtBearer(helper.GetJwtOptions());
string connectionStringAzure = builder.Configuration.GetConnectionString("SqlAzureDatabase");
builder.Services.AddTransient<IRepoBlog, RepositoryBlog>();
builder.Services.AddTransient<IRepoComentarios, RepositoryComentarios>();
builder.Services.AddTransient<IRepoMascotas, RepositoryMascotas>();
builder.Services.AddTransient<IRepoAutentication, RepositoryAutentication>();
builder.Services.AddTransient<IRepoVoluntarios, RepositoryVoluntarios>();
builder.Services.AddTransient<IRepoRefugios, RepositoryRefugios>();
builder.Services.AddTransient<IRepoAdopciones, RepositoryAdopciones>();
builder.Services.AddDbContext<MascotaContext>
    (options => options.UseSqlServer(connectionStringAzure));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "Apii OAuth Videojuegos 2023",
//        Version = "v1",
//        Description = "Api Videojuegos con seguridad token"
//    });
//});
builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "Api Rescute Dog";
    document.Description = "Api RescuteDog 2023. Seguridad Api OAuth";

    document.AddSecurity("JWT", Enumerable.Empty<string>(),
        new NSwag.OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Copia y pega el Token en el campo 'Value:' así: Bearer {Token JWT}."
        }
    );

    document.OperationProcessors.Add(
        new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseSwagger();
app.UseOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json", name: "Api v1");
    options.RoutePrefix = "";
    options.DocExpansion(DocExpansion.None);
});
if (app.Environment.IsDevelopment())
{
   
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
