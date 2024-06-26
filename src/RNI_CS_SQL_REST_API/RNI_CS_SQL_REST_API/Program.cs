using Microsoft.OpenApi.Models;
using RNI_CS_SQL_REST_API.DBContexts;
using RNI_CS_SQL_REST_API.Interfaces;
using RNI_CS_SQL_REST_API.Repositories;
using RNI_CS_SQL_REST_API.Services;

var builder = WebApplication.CreateBuilder(args);

//Aqui agregamos los servicios requeridos

//El DBContext a utilizar
builder.Services.AddSingleton<PgsqlDbContext>();

//Los repositorios
builder.Services.AddScoped<IResumenRepository, ResumenRepository>();
builder.Services.AddScoped<IUbicacionRepository, UbicacionRepository>();
builder.Services.AddScoped<ITipoReactorRepository, TipoReactorRepository>();
builder.Services.AddScoped<IReactorRepository, ReactorRepository>();

//Aqui agregamos los servicios asociados para cada ruta
builder.Services.AddScoped<ResumenService>();
builder.Services.AddScoped<UbicacionService>();
builder.Services.AddScoped<TipoReactorService>();
builder.Services.AddScoped<ReactorService>();


// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "RNI - Reactores Nucleares de Investigación - PostgreSQL Version",
        Description = "API para la gestión de Reactores Nucleares de Investigación"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Modificamos el encabezado de las peticiones para ocultar el web server utilizado
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Server", "RNIServer");
    await next();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
