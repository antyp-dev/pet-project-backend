using CSharpFunctionalExtensions;
using PetPlatform.Domain.Aggregates.SpeciesManagement.AggregateRoot.ValueObjects;
using PetPlatform.Domain.Aggregates.SpeciesManagement.BreedEntity;
using PetPlatform.Domain.Shared.EntityIds;

namespace PetPlatform.Domain.Aggregates.SpeciesManagement.AggregateRoot;

public class Species : Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];

    private Species(SpeciesId id) : base(id)
    {
    }

    public Species(SpeciesId id, SpeciesName name) : base(id)
    {
        Name = name;
    }

    public SpeciesName Name { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;
}