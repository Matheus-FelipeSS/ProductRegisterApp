using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using ProductControl.Contexts;

namespace ProductControl.Contexts
{
    public class ProductContextFactory : IDesignTimeDbContextFactory<ProductControlContext>
    {
        public ProductControlContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ProductControlContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new ProductControlContext(optionsBuilder.Options);
        }
    }
}
