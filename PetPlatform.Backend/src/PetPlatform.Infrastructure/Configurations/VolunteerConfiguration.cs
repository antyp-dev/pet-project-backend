using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot;
using PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot.ValueObjects;
using PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;
using PetPlatform.Domain.Shared.EntityIds;
using PetPlatform.Domain.Shared.ValueObjects;

namespace PetPlatform.Infrastructure.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));

        builder.ComplexProperty(v => v.FullName, nb =>
        {
            nb.Property(n => n.LastName)
                .IsRequired()
                .HasMaxLength(FullName.MaxPartLength)
                .HasColumnName("last_name");

            nb.Property(n => n.FirstName)
                .IsRequired()
                .HasMaxLength(FullName.MaxPartLength)
                .HasColumnName("first_name");

            nb.Property(n => n.MiddleName)
                .IsRequired()
                .HasMaxLength(FullName.MaxPartLength)
                .HasColumnName("middle_name");
        });

        builder.ComplexProperty(v => v.Email, eb =>
        {
            eb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(Email.MaxLength)
                .HasColumnName("email");
        });

        builder.ComplexProperty(v => v.Description, db =>
        {
            db.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(Description.MaxLength)
                .HasColumnName("description");
        });

        builder.ComplexProperty(v => v.YearsOfExperience, yb =>
        {
            yb.Property(n => n.Value)
                .IsRequired()
                .HasColumnName("years_of_experience");
        });

        builder.ComplexProperty(v => v.PhoneNumber, pb =>
        {
            pb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(PhoneNumber.MaxLength)
                .HasColumnName("phone_number");
        });

        builder.OwnsOne(v => v.SocialNetworks, snlb =>
        {
            snlb.ToJson();

            snlb.OwnsMany(snl => snl.SocialNetworks, snb =>
            {
                snb.Property(sn => sn.Name).IsRequired().HasMaxLength(SocialNetwork.MaxNameLength);
                snb.Property(sn => sn.Url).IsRequired().HasMaxLength(SocialNetwork.MaxUrlLength);
            });
        });

        builder.OwnsOne(v => v.RequisitesForSupport, rlb =>
        {
            rlb.ToJson();

            rlb.OwnsMany(rl => rl.Requisites, rb =>
            {
                rb.Property(r => r.Title).IsRequired().HasMaxLength(RequisiteForSupport.MaxTitleLength);
                rb.Property(r => r.Description).IsRequired().HasMaxLength(RequisiteForSupport.MaxTitleLength);
            });
        });

        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");
    }
}