using CSharpFunctionalExtensions;
using PetPlatform.Domain.Aggregates.SpeciesManagement.BreedEntity.ValueObjects;
using PetPlatform.Domain.Shared.EntityIds;

namespace PetPlatform.Domain.Aggregates.SpeciesManagement.BreedEntity;

public class Breed : Entity<BreedId>
{
    private Breed(BreedId id) : base(id)
    {
    }

    public Breed(BreedId id, BreedName breedName) : base(id)
    {
        BreedName = breedName;
    }

    public BreedName BreedName { get; private set; }
}