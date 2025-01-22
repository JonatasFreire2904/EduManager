using EduManager.Domain.Interfaces;
using EduManager.Repository.Contexts;
using EduManager.Repository.Repositories;
using EduManager.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Obt�m a connection string do arquivo de configura��o
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conex�o 'DefaultConnection' n�o foi encontrada no arquivo de configura��o.");
}

// Configura��o do DbContext com resili�ncia a falhas transit�rias
builder.Services.AddDbContext<EduManagerDbContext>(options =>
    options.UseSqlServer(connectionString, b =>
        b.MigrationsAssembly("EduManager.Repository")));

// Registra os servi�os e reposit�rios na inje��o de depend�ncia
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();

// Configura��o dos controladores e do Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplica as migra��es automaticamente ao iniciar o aplicativo
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EduManagerDbContext>();
    dbContext.Database.Migrate(); // Aplica migra��es pendentes
}

// Configura��o do pipeline de requisi��es HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();