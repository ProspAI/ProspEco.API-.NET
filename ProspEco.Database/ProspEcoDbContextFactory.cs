using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProspEco.Database
{
    public class ProspEcoDbContextFactory : IDesignTimeDbContextFactory<ProspEcoDbContext>
    {
        public ProspEcoDbContext CreateDbContext(string[] args)
        {
            // Configurar as opções do DbContext
            var optionsBuilder = new DbContextOptionsBuilder<ProspEcoDbContext>();
            
            // Adicionar o provedor de banco de dados e a connection string
            optionsBuilder.UseOracle("Data Source=oracle.fiap.com.br:1521/orcl;user ID=rm551236;Password=171103;");
            
            return new ProspEcoDbContext(optionsBuilder.Options);
        }
    }
}