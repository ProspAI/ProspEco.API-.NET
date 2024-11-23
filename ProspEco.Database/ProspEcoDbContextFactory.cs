using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ProspEco.Database
{
    public class ProspEcoDbContextFactory : IDesignTimeDbContextFactory<ProspEcoDbContext>
    {
        public ProspEcoDbContext CreateDbContext(string[] args)
        {
            // Carregar configurações do appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Obter string de conexão do Oracle
            var connectionString = configuration.GetConnectionString("OracleFIAP");

            // Configurar DbContext para Oracle
            var optionsBuilder = new DbContextOptionsBuilder<ProspEcoDbContext>();
            optionsBuilder.UseOracle(connectionString);

            return new ProspEcoDbContext(optionsBuilder.Options);
        }
    }
}