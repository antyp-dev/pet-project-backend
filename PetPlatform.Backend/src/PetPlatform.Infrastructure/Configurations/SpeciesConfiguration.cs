using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetPlatform.Domain.Aggregates.SpeciesManagement.AggregateRoot;
using PetPlatform.Domain.Aggregates.SpeciesManagement.AggregateRoot.ValueObjects;
using PetPlatform.Domain.Shared.EntityIds;

namespace PetPlatform.Infrastructure.Configurations;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value));

        builder.ComplexProperty(s => s.Name, nb =>
        {
            nb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(SpeciesName.MaxLength)
                .HasColumnName("name");
        });

        builder.HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey("species_id");
    }
}