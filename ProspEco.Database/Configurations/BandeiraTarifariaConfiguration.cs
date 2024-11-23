using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProspEco.Model.Entities;

namespace ProspEco.Database.Configurations
{
    public class BandeiraTarifariaConfiguration : IEntityTypeConfiguration<BandeiraTarifaria>
    {
        public void Configure(EntityTypeBuilder<BandeiraTarifaria> builder)
        {
            builder.ToTable("bandeiras_tarifarias", "dbo");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.TipoBandeira)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("tipo_bandeira");

            builder.Property(b => b.DataVigencia)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName("data_vigencia");

            // Índices
            builder.HasIndex(b => b.DataVigencia)
                .HasDatabaseName("IX_BandeiraTarifaria_DataVigencia");
        }
    }
}