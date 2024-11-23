using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProspEco.Model.Entities;

namespace ProspEco.Database.Configurations
{
    public class RecomendacaoConfiguration : IEntityTypeConfiguration<Recomendacao>
    {
        public void Configure(EntityTypeBuilder<Recomendacao> builder)
        {
            builder.ToTable("recomendacoes", "dbo");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.DataHora)
                .IsRequired()
                .HasColumnType("datetime")
                .HasColumnName("data_hora");

            builder.Property(r => r.Mensagem)
                .HasMaxLength(300)
                .HasColumnName("mensagem");

            builder.Property(r => r.UsuarioId)
                .IsRequired()
                .HasColumnName("usuario_id");

            builder.HasOne(r => r.Usuario)
                .WithMany(u => u.Recomendacoes)
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}