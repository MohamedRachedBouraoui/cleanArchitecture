using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uccspu.Domaine.Entites;

namespace Uccspu.AccessDonnees.Configurations
{
    public class ConfigurationEpisode : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            builder.Property(t => t.Libelle)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
