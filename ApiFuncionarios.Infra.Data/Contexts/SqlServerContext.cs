using ApiFuncionarios.Infra.Data.Entities;
using ApiFuncionarios.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFuncionarios.Infra.Data.Contexts
{
    /// <summary>
    /// Classe para acesso ao banco de dados com o EntityFramework
    /// </summary>
    public class SqlServerContext : DbContext
    {
        //construtor para inicializar a conexão com o banco de dados
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
        {

        }

        //adicionar cada classe de mapeamento do projeto
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FuncionarioMap());
        }

        //criar uma propriedade do tipo DbSet (CRUD) para cada classe de entidade
        public DbSet<Funcionario> Funcionario { get; set; }
    }
}



