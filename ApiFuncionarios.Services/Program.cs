using ApiFuncionarios.Infra.Data.Contexts;
using ApiFuncionarios.Infra.Data.Interfaces;
using ApiFuncionarios.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Configuração do Swagger

builder.Services.AddSwaggerGen(
        swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API para controle de funcionários - Treinamento em C# WebDeveloper.",
                Description = "Projeto desenvolvido em AspNet 6 API com EntityFramework e Dapper.",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "COTI Informática - Escola de NERDS",
                    Url = new Uri("http://www.cotiinformatica.com.br"),
                    Email = "contato@cotiinformatica.com.br"
                }
            });
        }
);

#endregion

#region Configuração do Repositório

//lendo a connectionstring localizada no arquivo 'appsettings.json'
var connectionString = builder.Configuration.GetConnectionString("BDApiFuncionarios");

//configurando a classe 'SqlServerContext' do projeto Infra.Data
//para que o EntityFramework funcione corretamente
builder.Services.AddDbContext<SqlServerContext>
    (options => options.UseSqlServer(connectionString));

//injeção de dependência das interfaces / classes do repositório
builder.Services.AddTransient<IFuncionarioRepository, FuncionarioRepository>();

#endregion

#region Configuração do CORS

builder.Services.AddCors(s => s.AddPolicy("DefaultPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

#endregion

var app = builder.Build();

#region Configuração do Swagger

app.UseSwagger();
app.UseSwaggerUI(s => { s.SwaggerEndpoint("/swagger/v1/swagger.json", "COTI API"); });

#endregion

app.UseAuthorization();

app.UseCors("DefaultPolicy");

app.MapControllers();

app.Run();

public partial class Program { }


