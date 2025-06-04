using CSharpFunctionalExtensions;

namespace PetPlatform.Domain.Shared.EntityIds;

public class SpeciesId : ValueObject, IComparable<SpeciesId>
{
    private SpeciesId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static SpeciesId NewId() => Guid.NewGuid();
    public static SpeciesId Empty() => Guid.Empty;
    public static SpeciesId Create(Guid id) => id;

    public static implicit operator SpeciesId(Guid id) => new SpeciesId(id);

    public static implicit operator Guid(SpeciesId id)
    {
        ArgumentNullException.ThrowIfNull(id);
        return id.Value;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public int CompareTo(SpeciesId? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }
}