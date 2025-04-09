using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using TWTodos.Contexts;

namespace TWTodos.Contexts
{
    public class TWTodosContextFactory : IDesignTimeDbContextFactory<TWTodosContext>
    {
        public TWTodosContext CreateDbContext(string[] args)
        {
            // Lê as configurações do appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Obtém a connection string do arquivo de configuração
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configura o DbContext para usar PostgreSQL
            var optionsBuilder = new DbContextOptionsBuilder<TWTodosContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new TWTodosContext(optionsBuilder.Options);
        }
    }
}

