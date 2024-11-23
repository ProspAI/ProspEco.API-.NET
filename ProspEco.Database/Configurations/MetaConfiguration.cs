using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProspEco.Model.Entities;

namespace ProspEco.Database.Configurations
{
    public class MetaConfiguration : IEntityTypeConfiguration<Meta>
    {
        public void Configure(EntityTypeBuilder<Meta> builder)
        {
            builder.ToTable("metas", "dbo");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.ConsumoAlvo)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasColumnName("consumo_alvo");

            builder.Property(m => m.Atingida)
                .IsRequired()
                .HasColumnName("atingida");

            builder.Property(m => m.DataInicio)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName("data_inicio");

            builder.Property(m => m.DataFim)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName("data_fim");

            builder.Property(m => m.UsuarioId)
                .IsRequired()
                .HasColumnName("usuario_id");

            builder.HasOne(m => m.Usuario)
                .WithMany(u => u.Metas)
                .HasForeignKey(m => m.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}