using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFuncionarios.Infra.Data.Contexts
{
    public class SqlServerMigration : IDesignTimeDbContextFactory<SqlServerContext>
    {
        public SqlServerContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            var connectionstring = root.GetSection("ConnectionStrings")
                .GetSection("BDApiFuncionarios").Value;

            var builder = new DbContextOptionsBuilder<SqlServerContext>();
            builder.UseSqlServer(connectionstring);

            return new SqlServerContext(builder.Options);
        }
    }
}



