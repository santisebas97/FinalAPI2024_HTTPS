using FinalAPI2024_HTTPS.Helpers;
using FinalAPI2024_HTTPS.Models;
using FinalAPI2024_HTTPS.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options=>{ //convertir los string a int y viceversa de los fomularios del front
    //options.JsonSerializerOptions.Converters.Add(new IntToStringConverter());
    //options.JsonSerializerOptions.Converters.Add(new DecimalToStringConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//aqui configuramos el CORS
builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolicy", builder => {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});


//cadena de conexion para mysql (para corregir los errores se agregó builder)
string mySqlConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AplicacionRecorridos2024Context>(options => options.UseMySql(
    mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));

//servicio para el restablecimiento por correo electronico y demás servicios
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<TransportistaService, TransportistaService>();
builder.Services.AddScoped<BusetaService, BusetaService>();
builder.Services.AddScoped<RecorridoService, RecorridoService>();
builder.Services.AddScoped<RecorridoDetalleService, RecorridoDetalleService>();
builder.Services.AddScoped<ComentarioService, ComentarioService>();
builder.Services.AddScoped<ReservaService, ReservaService>();



//crear el servicio para el token
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AplicacionTesisFinal.....")),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew=TimeSpan.Zero
    };
});
//el app builder debe ir despues de modificar la cadena de conexion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyPolicy");//se debe poner la politica antes de authentication siempre para el CORS
app.UseAuthentication();//este es authentication sirve para el token y la cadena de conexion mysql obligatorio
app.UseAuthorization();//token
app.MapControllers();

app.Run();

