using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace InventarioComercio.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<InventarioComercioDbContext>
    {
        public InventarioComercioDbContext CreateDbContext(string[] args)
        {
            // Cargar la configuración desde appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../InventarioComercio.API"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<InventarioComercioDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new InventarioComercioDbContext(optionsBuilder.Options);
        }
    }
}
