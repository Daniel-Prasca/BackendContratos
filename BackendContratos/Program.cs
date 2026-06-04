using BackendContratos.Data;
using BackendContratos.Middleware;
using BackendContratos.Services;
using BackendContratos.Services.Implementations;
using BackendContratos.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexion a la base de datos
if (builder.Environment.IsEnvironment("Testing"))
{
    var testDbName = "TestDb_" + Guid.NewGuid().ToString();
    builder.Services.AddDbContext<BackendContratoDbContext>(options =>
        options.UseInMemoryDatabase(testDbName));
}
else
    builder.Services.AddDbContext<BackendContratoDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar autenticaci�n JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Registrar servicios
builder.Services.AddScoped<IProveedoresService, ProveedoresService>();
builder.Services.AddScoped<ContratosService>();
builder.Services.AddScoped<ServiciosService>();
builder.Services.AddScoped<LiquidacionesService>();
builder.Services.AddScoped<PolizasService>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<AlertasService>();
builder.Services.AddScoped<AuthServices>();

var app = builder.Build();

// Aplicar migraciones autom�ticamente al arrancar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BackendContratoDbContext>();
    if (db.Database.IsRelational())
        db.Database.Migrate();
    else
        db.Database.EnsureCreated();
}

using (var scope = app.Services.CreateScope())
{
    var authService = scope.ServiceProvider.GetRequiredService<AuthServices>();
    await authService.SeedUsersAsync();
}


app.UseMiddleware<GlobalExceptionMiddleware>();

// CORS
app.UseCors(MyAllowSpecificOrigins);

// Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }