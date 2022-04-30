using ApiFuncionarios.Infra.Data.Contexts;
using ApiFuncionarios.Infra.Data.Interfaces;
using ApiFuncionarios.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Configura��o do Swagger

builder.Services.AddSwaggerGen(
        swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API para controle de funcion�rios - Treinamento em C# WebDeveloper.",
                Description = "Projeto desenvolvido em AspNet 6 API com EntityFramework e Dapper.",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "COTI Inform�tica - Escola de NERDS",
                    Url = new Uri("http://www.cotiinformatica.com.br"),
                    Email = "contato@cotiinformatica.com.br"
                }
            });
        }
);

#endregion

#region Configura��o do Reposit�rio

//lendo a connectionstring localizada no arquivo 'appsettings.json'
var connectionString = builder.Configuration.GetConnectionString("BDApiFuncionarios");

//configurando a classe 'SqlServerContext' do projeto Infra.Data
//para que o EntityFramework funcione corretamente
builder.Services.AddDbContext<SqlServerContext>
    (options => options.UseSqlServer(connectionString));

//inje��o de depend�ncia das interfaces / classes do reposit�rio
builder.Services.AddTransient<IFuncionarioRepository, FuncionarioRepository>();

#endregion

#region Configura��o do CORS

builder.Services.AddCors(s => s.AddPolicy("DefaultPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

#endregion

var app = builder.Build();

#region Configura��o do Swagger

app.UseSwagger();
app.UseSwaggerUI(s => { s.SwaggerEndpoint("/swagger/v1/swagger.json", "COTI API"); });

#endregion

app.UseAuthorization();

app.UseCors("DefaultPolicy");

app.MapControllers();

app.Run();

public partial class Program { }


