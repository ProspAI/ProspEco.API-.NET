using Microsoft.EntityFrameworkCore;
using ProspEco.Model.Entities;

namespace ProspEco.Database
{
    public class ProspEcoDbContext : DbContext
    {
        public ProspEcoDbContext(DbContextOptions<ProspEcoDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Aparelho> Aparelhos { get; set; }
        public DbSet<RegistroConsumo> RegistrosConsumo { get; set; }
        public DbSet<Meta> Metas { get; set; }
        public DbSet<Conquista> Conquistas { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<Recomendacao> Recomendacoes { get; set; }
        public DbSet<BandeiraTarifaria> BandeirasTarifarias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Adicione suas configurações de mapeamento aqui, se necessário
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Configuração de fallback para o tempo de design
                optionsBuilder.UseOracle("Data Source=oracle.fiap.com.br:1521/orcl;user ID=rm551236;Password=171103;");
            }
        }
    }
}