using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProspEco.Model.Entities;

namespace ProspEco.Database.Configurations
{
    public class NotificacaoConfiguration : IEntityTypeConfiguration<Notificacao>
    {
        public void Configure(EntityTypeBuilder<Notificacao> builder)
        {
            builder.ToTable("notificacoes", "dbo");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.DataHora)
                .IsRequired()
                .HasColumnType("datetime")
                .HasColumnName("data_hora");

            builder.Property(n => n.Lida)
                .IsRequired()
                .HasColumnName("lida");

            builder.Property(n => n.Mensagem)
                .HasMaxLength(500)
                .HasColumnName("mensagem");

            builder.Property(n => n.UsuarioId)
                .IsRequired()
                .HasColumnName("usuario_id");

            builder.HasOne(n => n.Usuario)
                .WithMany(u => u.Notificacoes)
                .HasForeignKey(n => n.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}