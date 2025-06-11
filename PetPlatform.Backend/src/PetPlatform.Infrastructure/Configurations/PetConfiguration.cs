using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity;
using PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity.ValueObjects;
using PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;
using PetPlatform.Domain.Shared.EntityIds;
using PetPlatform.Domain.Shared.ValueObjects;

namespace PetPlatform.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value));

        builder.ComplexProperty(p => p.PetName, nb =>
        {
            nb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(PetName.MaxLength)
                .HasColumnName("pet_name");
        });

        builder.Property(p => p.SpeciesId)
            .HasConversion(id => id.Value,
                value => SpeciesId.Create(value));

        builder.ComplexProperty(p => p.Description, db =>
        {
            db.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Description.MaxLength)
                .HasColumnName("description");
        });

        builder.Property(p => p.BreedId)
            .HasConversion(id => id.Value,
                value => BreedId.Create(value));

        builder.ComplexProperty(p => p.FurColor, fcb =>
        {
            fcb.Property(fc => fc.Value)
                .IsRequired()
                .HasMaxLength(FurColor.MaxLength)
                .HasColumnName("fur_color");
        });

        builder.ComplexProperty(p => p.HealthInfo, hib =>
        {
            hib.Property(hi => hi.Summary)
                .IsRequired()
                .HasMaxLength(HealthInfo.MaxSummaryLength)
                .HasColumnName("health_info_summary");

            hib.Property(hi => hi.IsVaccinated)
                .IsRequired()
                .HasColumnName("is_vaccinated");

            hib.Property(hi => hi.HasChronicDiseases)
                .IsRequired()
                .HasColumnName("has_chronic_diseases");

            hib.Property(hi => hi.LastVetVisit)
                .IsRequired(false)
                .HasColumnName("last_vet_visit");
        });

        builder.OwnsOne(p => p.Address, ab =>
        {
            ab.ToJson();

            ab.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(Address.MaxCityLength);

            ab.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Address.MaxStreetLength);

            ab.Property(a => a.HouseNumber)
                .IsRequired()
                .HasMaxLength(Address.MaxHouseLength);

            ab.Property(a => a.AdditionalInfo)
                .IsRequired(false)
                .HasMaxLength(Address.MaxAdditionalLength);
        });

        builder.ComplexProperty(p => p.Weight, wb =>
        {
            wb.Property(w => w.Value)
                .IsRequired()
                .HasColumnName("weight");
        });

        builder.ComplexProperty(p => p.Height, hb =>
        {
            hb.Property(h => h.Value)
                .IsRequired()
                .HasColumnName("height");
        });

        builder.ComplexProperty(p => p.PhoneNumber, pb =>
        {
            pb.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(PhoneNumber.MaxLength)
                .HasColumnName("phone_number");
        });

        builder.Property(p => p.IsNeutered).IsRequired();

        builder.ComplexProperty(p => p.BirthDate, bdb =>
        {
            bdb.Property(bd => bd.Value)
                .IsRequired()
                .HasColumnName("birth_date");
        });

        builder.ComplexProperty(p => p.HelpStatus, hsb =>
        {
            hsb.Property(hs => hs.Value)
                .IsRequired()
                .HasMaxLength(HelpStatus.MaxLength)
                .HasColumnName("help_status");
        });

        builder.OwnsOne(p => p.RequisitesForSupport, rlb =>
        {
            rlb.ToJson();

            rlb.OwnsMany(rl => rl.Requisites, rb =>
            {
                rb.Property(r => r.Title).IsRequired().HasMaxLength(RequisiteForSupport.MaxTitleLength);
                rb.Property(r => r.Description).IsRequired().HasMaxLength(RequisiteForSupport.MaxTitleLength);
            });
        });

        builder.Property(p => p.CreatedAt).IsRequired();
        
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}