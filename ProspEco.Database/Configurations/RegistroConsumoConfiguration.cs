using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProspEco.Model.Entities;

namespace ProspEco.Database.Configurations
{
    public class RegistroConsumoConfiguration : IEntityTypeConfiguration<RegistroConsumo>
    {
        public void Configure(EntityTypeBuilder<RegistroConsumo> builder)
        {
            builder.ToTable("registros_consumo", "dbo");

            builder.HasKey(rc => rc.Id);

            builder.Property(rc => rc.Consumo)
                .IsRequired()
                .HasColumnType("float") // Ajuste para "decimal(18,2)" se preferir decimal
                .HasColumnName("consumo");

            builder.Property(rc => rc.DataHora)
                .IsRequired()
                .HasColumnType("datetime")
                .HasColumnName("data_hora");

            builder.Property(rc => rc.AparelhoId)
                .IsRequired()
                .HasColumnName("aparelho_id");

            builder.HasOne(rc => rc.Aparelho)
                .WithMany(a => a.RegistrosConsumo)
                .HasForeignKey(rc => rc.AparelhoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}