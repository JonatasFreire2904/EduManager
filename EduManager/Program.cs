using EduManager.Domain.Interfaces;
using EduManager.Repository.Contexts;
using EduManager.Repository.Repositories;
using EduManager.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Obtém a connection string do arquivo de configuração
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada no arquivo de configuração.");
}

// Configuração do DbContext com resiliência a falhas transitórias
builder.Services.AddDbContext<EduManagerDbContext>(options =>
    options.UseSqlServer(connectionString, b =>
        b.MigrationsAssembly("EduManager.Repository")));

// Registra os serviços e repositórios na injeção de dependência
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();

// Configuração dos controladores e do Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplica as migrações automaticamente ao iniciar o aplicativo
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EduManagerDbContext>();
    dbContext.Database.Migrate(); // Aplica migrações pendentes
}

// Configuração do pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();