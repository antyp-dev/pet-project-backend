using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetPlatform.Domain.Aggregates.SpeciesManagement.BreedEntity;
using PetPlatform.Domain.Aggregates.SpeciesManagement.BreedEntity.ValueObjects;
using PetPlatform.Domain.Shared.EntityIds;

namespace PetPlatform.Infrastructure.Configurations;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breed");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value));

        builder.ComplexProperty(b => b.BreedName, nb =>
        {
            nb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(BreedName.MaxLength)
                .HasColumnName("name");
        });
    }
}