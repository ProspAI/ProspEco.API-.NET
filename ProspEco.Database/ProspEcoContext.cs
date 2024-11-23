// ProspEco.Database/Contexts/ProspEcoContext.cs
using Microsoft.EntityFrameworkCore;
using ProspEco.Database.Configurations;
using ProspEco.Model.Entities;

namespace ProspEco.Database.Contexts
{
    public class ProspEcoContext : DbContext
    {
        public ProspEcoContext(DbContextOptions<ProspEcoContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Aparelho> Aparelhos { get; set; }
        public DbSet<BandeiraTarifaria> BandeirasTarifarias { get; set; }
        public DbSet<Conquista> Conquistas { get; set; }
        public DbSet<Meta> Metas { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<Recomendacao> Recomendacoes { get; set; }
        public DbSet<RegistroConsumo> RegistrosConsumo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definir o schema padrão para Oracle (usando letras maiúsculas)
            modelBuilder.HasDefaultSchema("RM551236");

            // Aplicar as configurações específicas de cada entidade
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new AparelhoConfiguration());
            modelBuilder.ApplyConfiguration(new BandeiraTarifariaConfiguration());
            modelBuilder.ApplyConfiguration(new ConquistaConfiguration());
            modelBuilder.ApplyConfiguration(new MetaConfiguration());
            modelBuilder.ApplyConfiguration(new NotificacaoConfiguration());
            modelBuilder.ApplyConfiguration(new RecomendacaoConfiguration());
            modelBuilder.ApplyConfiguration(new RegistroConsumoConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}