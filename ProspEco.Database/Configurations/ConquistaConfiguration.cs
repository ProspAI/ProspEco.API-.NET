using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProspEco.Model.Entities;

namespace ProspEco.Database.Configurations
{
    public class ConquistaConfiguration : IEntityTypeConfiguration<Conquista>
    {
        public void Configure(EntityTypeBuilder<Conquista> builder)
        {
            builder.ToTable("Conquistas");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.DataConquista)
                .IsRequired()
                .HasColumnType("datetime");

            builder.HasOne(c => c.Usuario)
                .WithMany(u => u.Conquistas)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}