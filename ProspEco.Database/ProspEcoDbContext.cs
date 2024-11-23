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

            // Configurações específicas para cada entidade
            ConfigureUsuario(modelBuilder);
            ConfigureAparelho(modelBuilder);
            ConfigureRegistroConsumo(modelBuilder);
            ConfigureMeta(modelBuilder);
            ConfigureConquista(modelBuilder);
            ConfigureNotificacao(modelBuilder);
            ConfigureRecomendacao(modelBuilder);
            ConfigureBandeiraTarifaria(modelBuilder);
        }

        private void ConfigureUsuario(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("prospecco_usuarios");
                entity.HasKey(u => u.IdUsuario);

                entity.Property(u => u.DsEmail).HasColumnName("ds_email").HasMaxLength(255).IsRequired();
                entity.Property(u => u.DsNome).HasColumnName("ds_nome").HasMaxLength(255);
                entity.Property(u => u.DsSenha).HasColumnName("ds_senha").HasMaxLength(255).IsRequired();
                entity.Property(u => u.VlPontuacaoEconomia).HasColumnName("vl_pontuacao_economia");
                entity.Property(u => u.DsRole).HasColumnName("ds_role").HasMaxLength(255);
                entity.Property(u => u.DtCriacao).HasColumnName("dt_criacao");
                entity.Property(u => u.DtModificacao).HasColumnName("dt_modificacao");
            });
        }

        private void ConfigureAparelho(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aparelho>(entity =>
            {
                entity.ToTable("prospecco_aparelhos");
                entity.HasKey(a => a.IdAparelho);

                entity.Property(a => a.DsDescricao).HasColumnName("ds_descricao").HasMaxLength(255);
                entity.Property(a => a.DsNome).HasColumnName("ds_nome").HasMaxLength(100).IsRequired();
                entity.Property(a => a.VlPotencia).HasColumnName("vl_potencia").IsRequired();
                entity.Property(a => a.DsTipo).HasColumnName("ds_tipo").HasMaxLength(50).IsRequired();
                entity.Property(a => a.IdUsuario).HasColumnName("id_usuario").IsRequired();
                entity.Property(a => a.DtCriacao).HasColumnName("dt_criacao");
                entity.Property(a => a.DtModificacao).HasColumnName("dt_modificacao");

                entity.HasOne(a => a.Usuario)
                      .WithMany(u => u.Aparelhos)
                      .HasForeignKey(a => a.IdUsuario)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureRegistroConsumo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegistroConsumo>(entity =>
            {
                entity.ToTable("prospecco_registros_consumo");
                entity.HasKey(r => r.IdRegistro);

                entity.Property(r => r.VlConsumo).HasColumnName("vl_consumo").IsRequired();
                entity.Property(r => r.DtHora).HasColumnName("dt_hora").IsRequired();
                entity.Property(r => r.IdAparelho).HasColumnName("id_aparelho").IsRequired();
                entity.Property(r => r.DtCriacao).HasColumnName("dt_criacao");
                entity.Property(r => r.DtModificacao).HasColumnName("dt_modificacao");

                entity.HasOne(r => r.Aparelho)
                      .WithMany(a => a.RegistrosConsumo)
                      .HasForeignKey(r => r.IdAparelho)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureMeta(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meta>(entity =>
            {
                entity.ToTable("prospecco_metas");
                entity.HasKey(m => m.IdMeta);

                entity.Property(m => m.FlAtingida).HasColumnName("fl_atingida").IsRequired();
                entity.Property(m => m.VlConsumoAlvo).HasColumnName("vl_consumo_alvo").IsRequired();
                entity.Property(m => m.DtFim).HasColumnName("dt_fim").IsRequired();
                entity.Property(m => m.DtInicio).HasColumnName("dt_inicio").IsRequired();
                entity.Property(m => m.IdUsuario).HasColumnName("id_usuario").IsRequired();
                entity.Property(m => m.DtCriacao).HasColumnName("dt_criacao");
                entity.Property(m => m.DtModificacao).HasColumnName("dt_modificacao");

                entity.HasOne(m => m.Usuario)
                      .WithMany(u => u.Metas)
                      .HasForeignKey(m => m.IdUsuario)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureConquista(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conquista>(entity =>
            {
                entity.ToTable("prospecco_conquistas");
                entity.HasKey(c => c.IdConquista);

                entity.Property(c => c.DtConquista).HasColumnName("dt_conquista").IsRequired();
                entity.Property(c => c.DsDescricao).HasColumnName("ds_descricao").HasMaxLength(255);
                entity.Property(c => c.DsTitulo).HasColumnName("ds_titulo").HasMaxLength(100).IsRequired();
                entity.Property(c => c.IdUsuario).HasColumnName("id_usuario").IsRequired();
                entity.Property(c => c.DtCriacao).HasColumnName("dt_criacao");
                entity.Property(c => c.DtModificacao).HasColumnName("dt_modificacao");

                entity.HasOne(c => c.Usuario)
                      .WithMany(u => u.Conquistas)
                      .HasForeignKey(c => c.IdUsuario)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureNotificacao(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notificacao>(entity =>
            {
                entity.ToTable("prospecco_notificacoes");
                entity.HasKey(n => n.IdNotificacao);

                entity.Property(n => n.DtHora).HasColumnName("dt_hora").IsRequired();
                entity.Property(n => n.FlLida).HasColumnName("fl_lida").IsRequired();
                entity.Property(n => n.DsMensagem).HasColumnName("ds_mensagem").HasMaxLength(255);
                entity.Property(n => n.IdUsuario).HasColumnName("id_usuario").IsRequired();
                entity.Property(n => n.DtCriacao).HasColumnName("dt_criacao");
                entity.Property(n => n.DtModificacao).HasColumnName("dt_modificacao");

                entity.HasOne(n => n.Usuario)
                      .WithMany(u => u.Notificacoes)
                      .HasForeignKey(n => n.IdUsuario)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureRecomendacao(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recomendacao>(entity =>
            {
                entity.ToTable("prospecco_recomendacoes");
                entity.HasKey(r => r.IdRecomendacao);

                entity.Property(r => r.DtHora).HasColumnName("dt_hora").IsRequired();
                entity.Property(r => r.DsMensagem).HasColumnName("ds_mensagem").HasMaxLength(255);
                entity.Property(r => r.IdUsuario).HasColumnName("id_usuario").IsRequired();
                entity.Property(r => r.DtCriacao).HasColumnName("dt_criacao");
                entity.Property(r => r.DtModificacao).HasColumnName("dt_modificacao");

                entity.HasOne(r => r.Usuario)
                      .WithMany(u => u.Recomendacoes)
                      .HasForeignKey(r => r.IdUsuario)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureBandeiraTarifaria(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BandeiraTarifaria>(entity =>
            {
                entity.ToTable("prospecco_bandeiras_tarifarias");
                entity.HasKey(b => b.IdBandeira);

                entity.Property(b => b.DtVigencia).HasColumnName("dt_vigencia").IsRequired();
                entity.Property(b => b.DsTipoBandeira).HasColumnName("ds_tipo_bandeira").HasMaxLength(20).IsRequired();
                entity.Property(b => b.DtCriacao).HasColumnName("dt_criacao");
                entity.Property(b => b.DtModificacao).HasColumnName("dt_modificacao");
            });
        }
    }
}
