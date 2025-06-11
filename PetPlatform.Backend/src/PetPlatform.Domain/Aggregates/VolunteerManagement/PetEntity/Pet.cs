using CSharpFunctionalExtensions;
using PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity.ValueObjects;
using PetPlatform.Domain.Aggregates.VolunteerManagement.Shared.ValueObjects;
using PetPlatform.Domain.Shared.EntityIds;
using PetPlatform.Domain.Shared.ValueObjects;

namespace PetPlatform.Domain.Aggregates.VolunteerManagement.PetEntity;

public class Pet : Entity<PetId>
{
    private bool _isDeleted;
    private Pet(PetId id) : base(id)
    {
    }

    public Pet(PetId id,
        PetName petName,
        SpeciesId speciesId,
        Description description,
        BreedId breedId,
        FurColor furColor,
        HealthInfo healthInfo,
        Address address,
        Weight weight,
        Height height,
        PhoneNumber phoneNumber,
        bool isNeutered,
        BirthDate birthDate,
        HelpStatus helpStatus,
        DateTime createdAt) : base(id)
    {
        PetName = petName;
        SpeciesId = speciesId;
        Description = description;
        BreedId = breedId;
        FurColor = furColor;
        HealthInfo = healthInfo;
        Address = address;
        Weight = weight;
        Height = height;
        PhoneNumber = phoneNumber;
        IsNeutered = isNeutered;
        BirthDate = birthDate;
        HelpStatus = helpStatus;
        CreatedAt = createdAt;
    }

    public PetName PetName { get; private set; }
    public SpeciesId SpeciesId { get; private set; }
    public Description Description { get; private set; }
    public BreedId BreedId { get; private set; }
    public FurColor FurColor { get; private set; }
    public HealthInfo HealthInfo { get; private set; }
    public Address Address { get; private set; }
    public Weight Weight { get; private set; }
    public Height Height { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public bool IsNeutered { get; private set; }
    public BirthDate BirthDate { get; private set; }
    public HelpStatus HelpStatus { get; private set; }
    public RequisiteForSupportList? RequisitesForSupport { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public void SoftDelete()
    {
        _isDeleted = true;
    }

    public void Restore()
    {
        _isDeleted = false;
    }
}