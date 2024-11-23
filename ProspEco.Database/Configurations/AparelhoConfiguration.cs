using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProspEco.Model.Entities;

namespace ProspEco.Database.Configurations
{
    public class AparelhoConfiguration : IEntityTypeConfiguration<Aparelho>
    {
        public void Configure(EntityTypeBuilder<Aparelho> builder)
        {
            builder.ToTable("Aparelhos");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Tipo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Potencia)
                .IsRequired();

            builder.HasOne(a => a.Usuario)
                .WithMany(u => u.Aparelhos)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}