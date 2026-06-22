using Microsoft.EntityFrameworkCore;
using PetCloudApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Encender el soporte para Controladores
builder.Services.AddControllers();

// 2. Encender la documentación visual (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Configurar la conexión a SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 4. Activar la interfaz de Swagger en modo desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// 5. Mapear las rutas para que encuentre tu MascotasController
app.MapControllers();

app.Run();